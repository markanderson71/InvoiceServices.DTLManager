using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceServices.DTLManager.Core.Model;
using InvoiceServices.DTLManager.ViewModels;

namespace InvoiceServices.DTLManager
{
    public class AutoMapperConfigurationProfile:Profile
    {

        public AutoMapperConfigurationProfile()
        {
            CreateMap<string, string>().ConvertUsing(new NullStringConverter());
            CreateMap<LineItem, LineItemViewModel>().ReverseMap();
            CreateMap<InvoiceLineItemPost, LineItem > ().ReverseMap();
        }
    }

    public class NullStringConverter : ITypeConverter<string, string>
    {

        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (String.IsNullOrEmpty(source))
            {
                return String.Empty;
            }
            else
            {
                return source;
            }


        }
    }
}
