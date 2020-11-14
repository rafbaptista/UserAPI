using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WevoTest.Tests.Fixtures
{
    //Usado quando queremos usar nossa Fixture em diversas classes de teste
    [CollectionDefinition("ChromeDriver")]
    public class CollectionFixture : ICollectionFixture<TestFixture>
    {
    }
}
