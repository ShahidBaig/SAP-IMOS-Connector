namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class ProductionOrderLineItem
    {
        public string DocEntry { get; set; }

        public string LineNum { get; set; }

        public string TargetType { get; set; }

        public string TrgetEntry { get; set; }

        public string BaseRef { get; set; }

        public string BaseType { get; set; }

        public string BaseEntry { get; set; }

        public string TargetEntry { get; set; }

        public string BaseLine { get; set; }

        public string LineStatus { get; set; }

        public string ItemCode { get; set; }

        public string Dscription { get; set; }

        public double Quantity { get; set; }
    }
}

