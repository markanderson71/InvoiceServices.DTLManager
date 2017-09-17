using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceServices.DTLManager.Core.Model
{
    public interface ILineItems
    {
        string Id { get; set; }
        string InvoiceId { get; set; }
        string Description { get; set; }
        int Qauntity { get; set; }
        float perQauntityCost { get; set; }
        float LineItemTotal { get; set; }
    }
}
