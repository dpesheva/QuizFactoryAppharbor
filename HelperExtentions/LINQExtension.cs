namespace HelperExtentions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    public static class LINQExtension
    {
        public static IEnumerable<ModelClass> Project<T, ModelClass>(this IQueryable<T> query)
        { 
            return query.Project()
                        .To<ModelClass>()
                        .ToList();
        }
    }
}