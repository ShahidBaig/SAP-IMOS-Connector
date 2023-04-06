namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class BOM
    {
        public string Father { get; set; }

        public string ChildNum { get; set; }

        public string VisOrder { get; set; }

        public string Code { get; set; }

        public double Quantity { get; set; }

        public string Warehouse { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string PriceList { get; set; }

        public double OrigPrice { get; set; }

        public string OrigCurr { get; set; }
    }
}

