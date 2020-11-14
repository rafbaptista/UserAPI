using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WevoTest.Tests.Helpers;

namespace WevoTest.Tests.Fixtures
{
    //recurso que irá ser compartilhado por todos os testes da mesma classe (como um setup)
    //se for necessário compartilhar com diversas classes, utilizamos um collectionFixture que implementa esta Fixture
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public TestFixture()
        {
            Driver = new ChromeDriver(TestHelpers.TestDirectory);            
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
