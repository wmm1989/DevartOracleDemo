using System;
using System.Collections.Generic;
using JQ.Common.Model;
using JQ.Common.ViewModel;

namespace JQ.HIS2.Base.Model
{
    public class BaseExportClassStatisticsPropertyMapping : PropertyMapping<BaseExportClassStatisticsViewModel, Base_Export_Class_Statistics>
    {
	        public BaseExportClassStatisticsPropertyMapping() :
            base(new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(BaseExportClassStatisticsViewModel.Create_Time)] = new List<MappedProperty>()
                {
                    new MappedProperty{ Name=nameof(Base_Export_Class_Statistics.Create_Time),Revert=false}
                }
            })
        {
        }
    }
}






