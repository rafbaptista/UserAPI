using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WevoTest.Domain.Entities;
using WevoTest.Domain.Enums;
using WevoTest.Domain.Interfaces.Services;
using WevoTest.Infra.Data.Transactions;

namespace WevoTest.UI.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unit;

        public UserController(IUserService userService, IUnitOfWork unit)
        {
            _userService = userService;
            _unit = unit;
        }

        public IActionResult Index()
        {            
            return View(_userService.GetAll());            
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var model = _userService.Find(id.Value);

            if (model == null) return NotFound();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Add(user);                
                _unit.Commit();
                ViewBag.Message = "Usuário criado com sucesso";
                return View(user);
            }
            else
            {
                return View(user);
            }            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var model = _userService.Find(id.Value);

            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(user);
                _unit.Commit();
                ViewBag.Message = "Usuáro atualizado com sucesso";
                return View(user);
            }
            else
            {
                return View(user);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var model = _userService.Find(id.Value);

            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            _userService.Delete(_userService.Find(id.Value));
            _unit.Commit();
            ViewBag.Message = "Usuáro deletado com sucesso";
            return RedirectToAction(nameof(Index));
        }

    }
}
