using System;
using System.Collections.Generic;
using JQ.Common.Model;
using JQ.Common.ViewModel;

namespace JQ.HIS2.Base.Model
{
    public class BaseDrugSourcePropertyMapping : PropertyMapping<BaseDrugSourceViewModel, Base_Drug_Source>
    {
	        public BaseDrugSourcePropertyMapping() :
            base(new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(BaseDrugSourceViewModel.Create_Time)] = new List<MappedProperty>()
                {
                    new MappedProperty{ Name=nameof(Base_Drug_Source.Create_Time),Revert=false}
                }
            })
        {
        }
    }
}






