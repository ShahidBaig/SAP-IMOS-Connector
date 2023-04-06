namespace IMW.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SaleQuotation
    {
        public SaleQuotation()
        {
            this.Lines = new List<SaleQuotationLineItem>();
        }

        public override string ToString() => 
            this.DocNum;

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

        public List<SaleQuotationLineItem> Lines { get; set; }

        public string IMOS_PO_ID { get; set; }

        public bool Posted_IMOS { get; set; }

        public bool Posted_SAP { get; set; }

        public string U_Type1 { get; set; }
    }
}

