namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class MatFolder
    {
        public int Dir_Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Parent_Id { get; set; }

        public int Generation_Number { get; set; }
    }
}

