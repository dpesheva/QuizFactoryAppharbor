﻿namespace QuizFactory.Data.Common.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);

        void Detach(T entity);

        void SaveChanges();

        void UpdateValues(Expression<Func<T, object>> entity);
    }
}
