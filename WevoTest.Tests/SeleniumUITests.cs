using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using WevoTest.Domain.Entities;
using WevoTest.Tests.Fixtures;
using WevoTest.Tests.Helpers;
using WevoTest.Tests.PageObjects;
using Xunit;
using Xunit.Extensions;

namespace WevoTest.Tests
{    
    //UI Tests rely on deply, database, so they're low and expensive
    //run test without debug to run these tests            

    //fixtures on xUnit allow us to inject a class like IoC and share the same chrome driver
    //this will allow us to run all tests inside just one chrome tab, saving resources and gaining speed
    [Collection("ChromeDriver")]
    public class SeleniumUITests : IClassFixture<TestFixture>
    {
        private readonly IWebDriver _driver;

        public SeleniumUITests(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void MustDisplayPageTitle()
        {
            //arrange                                

            //act
            _driver.Navigate().GoToUrl("https://localhost:44345");

            //assert
            Assert.Contains("WevoTest.UI.Web", _driver.Title);
        }

        [Fact]
        public void MustDisplayListOfUsersOnInitialPage()
        {            
            //act
            _driver.Navigate().GoToUrl("https://localhost:44345");

            //assert
            Assert.Contains("Usuários cadastrados", _driver.PageSource);
        }

        [Fact]
        public void CreateValidUser()
        {
            //Arrange
            var createUserPO = new CreateUserPO(_driver);
                                   
            //Act
            createUserPO.GoToCreateUserPage();
            createUserPO.FillForm("Selenium Web Driver", "1111111", "mail@mail.com", "9954465564", "Male");
            createUserPO.SubmitForm();

            //Assert            
            Assert.Equal("Usuário criado com sucesso", createUserPO.MessageSuccess);                        
        }
        
        public static IEnumerable<object[]> InvalidCustomers
        {
            get
            {
                return new[]
                {
                    new object[] {"O campo Name é obrigatório", "", "1111111111", "mail@mail.com", "112121", "Male" },
                    new object[] {"O campo Name é obrigatório", " ", "1111111111", "mail@mail.com", "112121", "Male" },

                    new object[] {"O campo CPF é obrigatório", "Rafael", "", "mail@mail.com", "112121", "Male" },
                    new object[] {"O campo CPF é obrigatório", "Rafael", " ", "mail@mail.com", "112121", "Male" },

                    new object[] { "O campo Email é obrigatório", "Rafael", "1111111111", "", "112121", "Male" },
                    new object[] { "O campo Email é obrigatório", "Rafael", "1111111111", " ", "112121", "Male" },

                    new object[] { "O campo Cellphone é obrigatório", "Rafael", "1111111111", "mail@mail.com", "", "Male" },
                    new object[] { "O campo Cellphone é obrigatório", "Rafael", "1111111111", "mail@mail.com", " ", "Male" },

                    new object[] { "O campo Gender é obrigatório", "Rafael", "1111111111", "mail@mail.com", "112121", "" },
                    new object[] { "O campo Gender é obrigatório", "Rafael", "1111111111", "mail@mail.com", "112121", " " },
                };
            }
        }

        [Theory(DisplayName = nameof(ShouldNotCreateInvalidUser))]
        [MemberData(nameof(InvalidCustomers))]
        [Trait("", "Selenium UI Tests")]
        public void ShouldNotCreateInvalidUser(string messageError, params string[] customerValues)
        {
            //Arrange
            var createUserPO = new CreateUserPO(_driver);
            createUserPO.GoToCreateUserPage();

            //Act
            createUserPO.FillForm(customerValues);
            createUserPO.SubmitForm();
            var errorElement = createUserPO.ErrorMessageElement(messageError);

            //Assert
            Assert.NotNull(errorElement);
            Assert.True(errorElement.Displayed);                                                          
        }

    }
}
