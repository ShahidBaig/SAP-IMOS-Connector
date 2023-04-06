namespace IMW.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Customer
    {
        public Customer()
        {
            this.ContactCode = new List<string>();
            this.ContactPerson = new List<string>();
            this.Phone = new List<string>();
        }

        public Customer(string customerCode, string customerName)
        {
            this.CustomerCode = customerCode;
            this.CustomerName = this.CustomerName;
        }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public int DocEntry { get; set; }

        public List<string> ContactCode { get; set; }

        public List<string> ContactPerson { get; set; }

        public List<string> Phone { get; set; }
    }
}

