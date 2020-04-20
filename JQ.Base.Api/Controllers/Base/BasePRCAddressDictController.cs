using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JQ.Base.IServices;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.IServices;
using JQ.Common.Model;
using JQ.Common.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JQ.EHR3.Api.Controllers
{
    /// <summary>
    /// 行政区划
    /// </summary>
	[Route("api/base/prcaddressdict")]
    [ApiController]
    public class BasePRCAddressDictController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBasePRCAddressDictServices basePRCAddressDictServices;

        public BasePRCAddressDictController(IBasePRCAddressDictServices basePRCAddressDictServices, IMapper mapper)
        {
            this.basePRCAddressDictServices = basePRCAddressDictServices;
            this.mapper = mapper;
        }

        /// <summary>
        /// 查询行政区划
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPRCAddressDict([FromQuery]BasePRCAddressDictParameters param)
        {

            var whereExpression = PredicateBuilder.New<Base_PRCAddress_Dict>(true);

            whereExpression = whereExpression.GetWhereExpression<Base_PRCAddress_Dict, BasePRCAddressDictParameters>(param);

            var data = await basePRCAddressDictServices.GetPageAsync((PaginationBase)param, whereExpression);
            PaginatedList<BasePRCAddressDictViewModel> result = mapper.Map<PaginatedList<BasePRCAddressDictViewModel>>(data);
            return Ok(ApiResult.GetResult20x(result));

        }



    }


}




