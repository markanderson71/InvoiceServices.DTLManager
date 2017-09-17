using InvoiceServices.DTLManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceServices.DTLManager.ViewModels
{
    public class LineItemViewModel : ILineItems
    {
        public string Id { get; set; }
        public string InvoiceId { get; set; }
        public string Description { get; set; }
        public int Qauntity { get ; set ; }
        public float perQauntityCost { get ; set ; }
        public float LineItemTotal { get ; set ; }
        
    }
}
