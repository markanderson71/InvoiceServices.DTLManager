using System;
using System.Collections.Generic;
using System.Text;
using InvoiceServices.DTLManager.Core.Model;

namespace InvoiceServices.DTLManager.Core
{
    public interface IRepository
    {

        LineItem GetItem(string itemId);

        string Add(LineItem lineItem);
        IEnumerable<LineItem> GetAll();
        bool IsAvailable();
    }
}
