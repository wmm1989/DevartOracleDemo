using System;
using System.Collections.Generic;
using JQ.Common.Model;
using JQ.Common.ViewModel;

namespace JQ.HIS2.Base.Model
{
    public class BaseImportClassStatisticsPropertyMapping : PropertyMapping<BaseImportClassStatisticsViewModel, Base_Import_Class_Statistics>
    {
	        public BaseImportClassStatisticsPropertyMapping() :
            base(new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(BaseImportClassStatisticsViewModel.Create_Time)] = new List<MappedProperty>()
                {
                    new MappedProperty{ Name=nameof(Base_Import_Class_Statistics.Create_Time),Revert=false}
                }
            })
        {
        }
    }
}






