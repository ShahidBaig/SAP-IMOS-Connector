namespace IMW.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductionOrder
    {
        public ProductionOrder()
        {
            this.Lines = new List<ProductionOrderLineItem>();
        }

        public string DocEntry { get; set; }

        public string DocNum { get; set; }

        public string DocType { get; set; }

        public string Canceled { get; set; }

        public string DocStatus { get; set; }

        public string InvntSttus { get; set; }

        public string Transfered { get; set; }

        public string ObjType { get; set; }

        public DateTime DocDueDate { get; set; }

        public DateTime DocDate { get; set; }

        public string CardCode { get; set; }

        public string CardName { get; set; }

        public List<ProductionOrderLineItem> Lines { get; set; }
    }
}

