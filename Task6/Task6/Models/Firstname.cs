using System;
using System.Collections.Generic;

namespace Task6
{
    public abstract partial class Firstname
    {
        public string Name { get; set; } = null!;
        public bool IsMale { get; set; }
        public int Id { get; set; }
    }
}
