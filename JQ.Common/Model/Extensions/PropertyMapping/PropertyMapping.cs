using System.Collections.Generic;

namespace JQ.Common.Model
{
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
        where TDestination : IEntity
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IEntity.ID)] = new List<MappedProperty> { new MappedProperty { Name = nameof(IEntity.ID), Revert = false } };
        }
    }
}
