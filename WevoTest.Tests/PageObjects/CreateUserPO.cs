using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace WevoTest.Tests.PageObjects
{
    //All the html references should stay inside Page Objects class
    public class CreateUserPO
    {
        private readonly IWebDriver _driver;
        public readonly By ByInputName;
        public readonly By ByInputCpf;
        public readonly By ByInputEmail;
        public readonly By ByInputCellphone;
        public readonly By ByInputGender;
        public readonly By ByInputSubmit;
        public string MessageSuccess
        {
            get
            {
                return _driver.FindElement(By.CssSelector("div[role=alert].alert.alert-primary")).Text;
            }
        }        

        public IWebElement ErrorMessageElement(string messageError)
        {
            try
            {
                return _driver.FindElement(By.XPath($"//span[text() = '{messageError}']"));
            }
            catch (Exception)
            {
                return null;
            }
        }      


        public CreateUserPO(IWebDriver driver)
        {
            _driver = driver;
            ByInputName = By.Id("Name");
            ByInputCpf = By.Id("CPF");
            ByInputEmail = By.Id("Email");
            ByInputCellphone = By.Id("Cellphone");
            ByInputGender = By.Id("Gender");
            ByInputSubmit = By.CssSelector("input[type=submit]");
        }

        public void GoToCreateUserPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:44345/User/Create");
        }

        public void FillForm(string name, string cpf, string email, string cellphone, string gender)
        {
            string[] values = new[] { name, cpf, email, cellphone, gender };
            By[] fields = new[] { ByInputName, ByInputCpf, ByInputEmail, ByInputCellphone, ByInputGender };

            for (int i = 0; i < values.Length; i++)
            {
                IWebElement element = _driver.FindElement(fields[i]);
                element.SendKeys(values[i]);
            }
        }

        public void FillForm(string[] values)
        {
            FillForm(values[0], values[1], values[2], values[3], values[4]);
        }

        public void SubmitForm()
        {
            _driver.FindElement(ByInputSubmit).Click();
        }

    }
}
