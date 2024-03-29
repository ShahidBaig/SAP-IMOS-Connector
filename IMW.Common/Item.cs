﻿namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class Item
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string FrgnName { get; set; }

        public string SalUnitMsr { get; set; }

        public int U_InCharges { get; set; }

        public double Price { get; set; }
        public double AvgCost { get; set; }

        public string DfltWH { get; set; }

        public string VatGourpSa { get; set; }

        public string Currency { get; set; }
        public string Active { get; set; }
        public string Inactive { get; set; }
    }
}

