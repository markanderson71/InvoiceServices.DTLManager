using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceServices.DTLManager.Core.Model
{
    public class LineItem:ILineItems
    {

        public string Id { get; set; }
        public string InvoiceId { get; set;}
        public string Description { get; set; }
        public int Qauntity{ get; set; }
        public float perQauntityCost { get; set; }

        public float LineItemTotal
        {
            get
            {
                return Qauntity * perQauntityCost;
            }

            set { }
        }
     
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Status { get; set ; }
      
    }
}
