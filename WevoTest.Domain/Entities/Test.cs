using System;
using System.Collections.Generic;
using System.Text;
using WevoTest.Domain.Interfaces;

namespace WevoTest.Domain.Entities
{
    public class Test : IEntity
    {
        public int Number { get; set; }

        public int Id { get; set; }

    }
}
