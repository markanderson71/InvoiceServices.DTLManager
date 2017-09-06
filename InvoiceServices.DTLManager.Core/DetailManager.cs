using System;
using InvoiceServices.DTLManager.Core.Model;
using System.Collections.Generic;

namespace InvoiceServices.DTLManager.Core
{
    public class DetailManager
    {
        private IRepository repo;

        public DetailManager(IRepository repo)
        {
            this.repo = repo;
        }


        public LineItem GetItem(string itemId)
        {
            return repo.GetItem(itemId);
        }


        public string Add(LineItem newLineItem)
        {
            return repo.Add(newLineItem);
        }


        public IEnumerable<LineItem> GetAll()
        {
            return repo.GetAll();
        }

    }
}
