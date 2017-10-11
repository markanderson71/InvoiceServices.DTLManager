using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceServices.DTLManager.ViewModels
{
    public class InvoiceLineItemPost
    {
       
        public string Description { get; set; }
        public int Qauntity { get; set; }
        public float perQauntityCost { get; set; }
        public float LineItemTotal { get; set; }
    }
}
