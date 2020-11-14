using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using WevoTest.Tests.Helpers;
using Xunit;

namespace WevoTest.Tests
{
    public class TestSeleniumScreenshot
    {
        [Fact]
        public void SeleniumScreenshot()
        {
            //Arrange

            //selenium web driver + chrome driver
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //project directory to save screenshot                        
            string directoryToSaveScreens = $"{TestHelpers.TestDirectory}" +
                                            $"\\screenshots\\" +
                                            $"{DateTime.Now.Day.ToString()}-" +
                                            $"{DateTime.Now.Month.ToString()}" +
                                            $"-{DateTime.Now.Year.ToString()}";

            //Act

            //navigate to url
            driver.Navigate().GoToUrl("https://www.uol.com.br/");
           
            //take screenshot
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();                                

            //creates directory if not exists
            Directory.CreateDirectory(directoryToSaveScreens);

            //and save screenshot inside it
            ss.SaveAsFile($"{directoryToSaveScreens}\\screenshotTeste.png");

            //Assert
            Assert.Contains("UOL - O melhor conteúdo", driver.Title);
            Assert.True(File.Exists($"{directoryToSaveScreens}\\screenshotTeste.png"));            

            //closes chrome 
            driver.Close();
        }
    }
}
