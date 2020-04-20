using System;
using System.Collections.Generic;
using System.Linq;

namespace JQ.Common.Model
{
    public class PropertyMappingContainer: IPropertyMappingContainer
    {
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public void Register<T>() where T: IPropertyMapping, new()
        {
            _propertyMappings.Add(new T());
        }

        public IPropertyMapping Resolve<TSource, TDestination>() where TDestination: IEntity
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>().ToList();
            if (matchingMapping.Count > 0)
            {
                return matchingMapping.First();
            }

            throw new Exception($"无法找到映射对象<{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity
        {
            var propertyMapping = Resolve<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);
                if (!propertyMapping.MappingDictionary.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
