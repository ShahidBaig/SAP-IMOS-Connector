namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class SaleQuotationLineItem
    {
        public override bool Equals(object obj) => 
            this.ItemCode == (obj as SaleQuotationLineItem).ItemCode;

        public double GetLineTotal() => 
            this.Quantity * this.Price;

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

        public double Price { get; set; }

        public string WhsCode { get; set; }

        public string TaxCode { get; set; }

		public double TaxRate { get; set; }

		public string UOM { get; set; }

		public int UomEntry { get; set; }

		public int UgpEntry { get; set; }

        public string UgpCode { get; set; }

        public int Updated { get; set; }

        public double SequenceNo { get; set; }
    }
}

