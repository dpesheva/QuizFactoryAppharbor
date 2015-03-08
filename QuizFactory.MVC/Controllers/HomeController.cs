namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Web.Mvc;
    using HelperExtentions;
    using MvcPaging;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Services.Interfaces;

    [HandleError]
    public class HomeController : BaseController
    {
        private const int PageSize = 6;
        private readonly IQuizzesService quizzesData;

        public HomeController(IQuizFactoryData data, IQuizzesService quizzesData)
            : base(data)
        {
            this.quizzesData = quizzesData;
        }
        
        public ActionResult Index(int? catId)
        {
            this.ViewBag.CategoryId = catId;
            var category = this.Db.Categories.Find(catId);

            if (category != null)
            {
                this.ViewBag.Category = category.Name;
            }

            return this.View();
        }

        [OutputCache(Duration = 10 * 60)]
        public ActionResult Categories()
        {
            var categories = this.Db.Categories.All().ToList();
            return this.PartialView("_CategoriesPartial", categories);
        }

        public ActionResult GetRandomQuizzes()
        {
            if (this.Db.QuizzesDefinitions.All().Any())
            {
                var ramdomQuizzes = this.quizzesData
                                        .GetAllPublic(e => Guid.NewGuid(), true) // TODO add user's quizzes
                                        .Take(3)
                                        .Project<QuizDefinition, QuizMainInfoViewModel>();

                return this.PartialView("_RandomQuizBoxesPartial", ramdomQuizzes);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No available quzzes");
        }

        public ActionResult Search(string search, int? page)
        {
            search = search ?? string.Empty;
            var quizzes = this.quizzesData
                              .GetAllPublic()// TODO add user's quizzes
                              .Where(q => q.Title.ToLower().Contains(search.ToLower())) 
                              .Project<QuizDefinition, QuizMainInfoViewModel>();

            this.ViewBag.Pages = Math.Ceiling((double)quizzes.Count() / PageSize);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.View(quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult RecentContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = this.GetData(catId, q => q.CreatedOn, false);
            return this.FormatOutput(page, quizzes);
        }

        public ActionResult PopularContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = this.GetData(catId, q => q.TakenQuizzes.Count, false);
            return this.FormatOutput(page, quizzes);
        }

        public ActionResult ByNameContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = this.GetData(catId, q => q.Title, true);
            return this.FormatOutput(page, quizzes);
        }

        public ActionResult ByRatingContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = this.GetData(catId, q => q.Rating, false);
            return this.FormatOutput(page, quizzes);
        }

        public ActionResult Error()
        {
            return this.View();
        }

        private IEnumerable<QuizMainInfoViewModel> GetData<TOrderBy>(int? catId, Expression<Func<QuizDefinition, TOrderBy>> predicate, bool asc)
        {
            IQueryable<QuizDefinition> quizzesQuery = this.quizzesData.GetAllPublic(predicate, asc);

            if (catId != null)
            {
                quizzesQuery = quizzesQuery.Where(q => q.CategoryId == catId);
            }

            return quizzesQuery.Project<QuizDefinition, QuizMainInfoViewModel>();
        }
        
        private ActionResult FormatOutput(int? page, IEnumerable<QuizMainInfoViewModel> quizzes)
        {
            this.ViewBag.Pages = Math.Ceiling((double)quizzes.Count() / PageSize);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }
    }
}