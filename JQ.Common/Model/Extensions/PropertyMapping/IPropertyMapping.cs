using System.Collections.Generic;

namespace JQ.Common.Model
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}