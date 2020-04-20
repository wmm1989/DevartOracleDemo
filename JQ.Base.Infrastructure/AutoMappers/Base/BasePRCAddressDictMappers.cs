using AutoMapper;
using JQ.Common.Model;
using JQ.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JQ.EHR3.Infrastructure.AutoMappers
{
    public class BasePRCAddressDictMappers : Profile
    {
        public BasePRCAddressDictMappers()
        {
				               
			     /// <summary>
				 /// Base_PRCAddress_Dict
				 /// </summary>
				 CreateMap<Base_PRCAddress_Dict, BasePRCAddressDictViewModel>();
				 CreateMap<BasePRCAddressDictViewModel, Base_PRCAddress_Dict>();
				 CreateMap<PaginatedList<Base_PRCAddress_Dict>, PaginatedList<BasePRCAddressDictViewModel>>();

        }

    }
}


