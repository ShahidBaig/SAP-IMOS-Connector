namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class IMOSVariable
    {
        public override string ToString() => 
            $"{this.Name}";

        public string ArticleID { get; set; }

        public string OrderId { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public IMOSItemType Typ { get; set; }

        public int Quantity { get; set; }

        public int CNT { get; set; }

        public double Price { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }
    }
}

