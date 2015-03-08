namespace QuizFactory.Services.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using QuizFactory.Data.Models;

    public interface IQuizzesService : IService
    {
        IQueryable<QuizDefinition> GetAllPublic();

        IQueryable<QuizDefinition> GetAllPublic<TOrderBy>(Expression<Func<QuizDefinition, TOrderBy>> predicate, bool asc);
    }
}