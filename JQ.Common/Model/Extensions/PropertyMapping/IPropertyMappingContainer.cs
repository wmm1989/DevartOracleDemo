﻿
namespace JQ.Common.Model
{
    public interface IPropertyMappingContainer
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity;
        bool ValidMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity;
    }
}