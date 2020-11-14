using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WevoTest.Domain.Entities;
using WevoTest.Domain.Interfaces.Services;
using WevoTest.Domain.ViewModels;
using WevoTest.Infra.Data.Transactions;

namespace WevoTest.Services.WebApi.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unit;
        public UserController(IUserService userService, IUnitOfWork unit)
        {
            _userService = userService;
            _unit = unit;
        }

        [Route("api/v1/user")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }

        [Route("api/v1/user/{id}")]
        [HttpGet]
        public ResultViewModel Get(int id)
        {
            var result = new ResultViewModel();

            if (id <= 0)
            {
                result.Message = "Id inválido";
                return result;
            }

            User user = _userService.GetById(id);

            if (user != null)
            {
                result.Success = true;
                result.Message = "Usuário localizada";                
                result.Data = user;
            }
            else
            {
                result.Success = false;
                result.Message = "Usuário não localizada";
            }
            return result;
        }

        [Route("api/v1/user")]
        [HttpPost]
        public ResultViewModel Post([FromBody] User user)
        {
            var result = new ResultViewModel();

            if (user == null)
            {
                result.Message = "Erro ao cadastrar pessoa, favor confira se os parâmetros informados estão corretos";
                return result;
            }            

            if (ModelState.IsValid)
            {
                _userService.Add(user);
                _unit.Commit();
                result.Success = true;
                result.Message = "Usuário cadastrada com sucesso";
                result.Data = user;
            }
            else
            {
                result.Success = false;
                result.Message = "Erro ao cadastrar pessoa";
                result.Data = ModelStateErrors(ModelState);
            }
            return result;
        }

        [HttpPut]
        [Route("api/v1/user")]
        public ResultViewModel Put([FromBody] User user)
        {
            var result = new ResultViewModel();

            //wrong id, incorrect json format
            if (user == null)
            {
                result.Message = "Erro ao cadastrar usuário, favor confira se os parâmetros informados estão corretos";
                return result;
            }
           

            if (ModelState.IsValid)
            {
                var model = _userService.Find(user.Id);

                if (model != null)
                {
                    _userService.Update(model);
                    _unit.Commit();

                    result.Success = true;
                    result.Message = "Usuário atualizada com sucesso";
                    result.Data = user;
                    return result;
                }
                else
                {
                    result.Message = "Usuário não localizado";
                    return result;
                }
            }
            result.Success = false;
            result.Message = "Erro ao atualizar usuário";            
            return result;
        }

        [HttpDelete]
        [Route("api/v1/user")]
        public ResultViewModel Delete([FromBody] User user)
        {
            var result = new ResultViewModel();

            if (user == null)
            {
                result.Message = "Erro ao deletar usuário, favor confira se os parâmetros informados estão corretos";
                return result;
            }

            User model = _userService.Find(user.Id);

            if (model != null)
            {
                _userService.Delete(user);
                _unit.Commit();
                result.Success = true;
                result.Message = "Usuário deletado com sucesso";
            }
            else
            {
                result.Success = false;
                result.Message = "Usuário não encontrado";
            }
            return result;
        }

        private List<ModelError> ModelStateErrors(ModelStateDictionary modelState)
        {
            return modelState.Where(a => a.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).ToList();
        }

    }
}
