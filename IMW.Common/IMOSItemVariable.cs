namespace IMW.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class IMOSItemVariable
    {
        public override string ToString() => 
            this.VariableValue;

        public string VariableName { get; set; }

        public string VariableValue { get; set; }
    }
}

