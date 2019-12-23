using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.MvcFramework.Mapping
{
    public static class MappingExtensions
    {
        public static ICollection<TDestination> To<TDestination>(this ICollection<object> collection)
        {
            return collection
                .Select(elem => ModelMapper.ProjectTo<TDestination>(elem))
                .ToList();
        }
    }
}
