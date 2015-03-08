namespace QuizFactory.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Models;
    using QuizFactory.Services.Interfaces;

    public class QuizzesService : IQuizzesService
    {
        private IDeletableEntityRepository<QuizDefinition> db;

        public QuizzesService(IDeletableEntityRepository<QuizDefinition> data)
        {
            this.db = data;
        }

        public IDeletableEntityRepository<QuizDefinition> Db
        {
            get
            {
                return this.db;
            }
        }

        public IQueryable<QuizDefinition> GetAllPublic()
        {
            return this.db.All().Where(q => q.IsPublic);
        }

        public IQueryable<QuizDefinition> GetAllPublic<TOrderBy>(Expression<Func<QuizDefinition, TOrderBy>> predicate, bool asc)
        {
            if (asc)
            {
                return this.GetAllPublic().OrderBy(predicate);
            }
            else
            {
                return this.GetAllPublic().OrderByDescending(predicate);
            }
        }
    }
}