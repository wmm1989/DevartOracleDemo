using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.IRepository;
using JQ.Common.IServices;
using JQ.Common.Model;
using JQ.Common.ViewModel;
using JQ.Base.IServices;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JQ.Base.Api.Controllers
{

    [Route("api/feeItem")]
    [ApiController]
    public class FeeItemController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseFeeItemClassServices baseFeeItemClassServices;

        public FeeItemController(IBaseFeeItemClassServices baseFeeItemClassServices,
           IMapper mapper)
        {
            this.baseFeeItemClassServices = baseFeeItemClassServices;
            this.mapper = mapper;
        }

        [HttpGet("class")]
        public async Task<IActionResult> GetFeeItemClass([FromQuery]BaseFeeItemClassParameters param)
        {
           
            var whereExpression = PredicateBuilder.New<Base_Fee_Item_Class>(true);

            whereExpression = whereExpression.GetWhereExpression<Base_Fee_Item_Class, BaseFeeItemClassParameters>(param);

            var data = await baseFeeItemClassServices.GetPageAsync((PaginationBase)param, whereExpression);

            PaginatedList<BaseFeeItemClassViewModel> result = mapper.Map<PaginatedList<BaseFeeItemClassViewModel>>(data);

            return Ok(ApiResult.GetResult20x(result));
        }

     
    }

}




