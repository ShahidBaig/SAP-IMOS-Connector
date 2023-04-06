namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class MapItem
    {
        public override bool Equals(object obj)
        {
            MapItem objA = obj as MapItem;
            return (!ReferenceEquals(objA, null) ? (((this.Length == objA.Length) && ((this.Width == objA.Width) && ((this.Thickness == objA.Thickness) && ((objA.IMOSItem == this.IMOSItem) && ((objA.IMOSItemVariable == this.IMOSItemVariable) && (objA.SAPItem == this.SAPItem)))))) && (objA.IMOSItemVariableValue == this.IMOSItemVariableValue)) : false);
        }

        public override string ToString() => 
            $"{this.IMOSItemVariable} - {this.IMOSItemVariableValue}";

        public string IMOSItem { get; set; }

        public string ItemName =>
            $"{this.IMOSItem} - {this.Length} X {this.Width} X {this.Thickness}";

        public string IMOSItemVariable { get; set; }

        public string IMOSItemVariableValue { get; set; }

        public string SAPItem { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public string ArticleNo { get; set; }

        public double Quantity { get; set; }
    }
}

