using AutoMapper;
using JQ.Common.Model;
using JQ.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JQ.Base.Infrastructure.AutoMappers
{
    public class FeeItemMappers : Profile
    {
        public FeeItemMappers()
        {
            CreateMap<Base_Fee_Item_Class, BaseFeeItemClassViewModel>();
            CreateMap<BaseFeeItemClassViewModel, Base_Fee_Item_Class>();
            CreateMap<PaginatedList<Base_Fee_Item_Class>, PaginatedList<BaseFeeItemClassViewModel>>();

        }

    }
}


