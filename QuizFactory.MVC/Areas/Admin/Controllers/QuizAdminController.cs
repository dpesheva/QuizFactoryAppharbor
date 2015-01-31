﻿namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    //using Kendo.Mvc.UI;
    using QuizFactory.Data;
    using QuizFactory.Data.Common;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using QuizFactory.Mvc.Areas.Common.Controllers;
    using Model = QuizFactory.Data.Models.QuizDefinition;
    using ViewModel = QuizFactory.Mvc.Areas.Admin.ViewModels.QuizAdminViewModel;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class QuizAdminController : KendoGridAdministrationController
    {
        public QuizAdminController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            this.TempData["categories"] = this.Db.Categories.All().Project().To<CategoryViewModel>().ToList();
            return this.View();
        }

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest]
        //                           DataSourceRequest request, ViewModel model)
        //{
        //    var dbModel = base.Create<Model>(model);
        //    if (dbModel != null)
        //    {
        //        model.Id = dbModel.Id;
        //    }

        //    return this.GridOperation(model, request);
        //}

        //[HttpPost]
        //public ActionResult Update([DataSourceRequest]
        //                           DataSourceRequest request, ViewModel model)
        //{
        //    base.Update<Model, ViewModel>(model, model.Id);
        //    return this.GridOperation(model, request);
        //}

        //[HttpPost]
        //public ActionResult Destroy([DataSourceRequest]
        //                            DataSourceRequest request, ViewModel model)
        //{
        //    if (model != null && this.ModelState.IsValid)
        //    {
        //        this.Db.QuizzesDefinitions.Delete(model.Id);
        //        this.Db.SaveChanges();
        //    }

        //    return this.GridOperation(model, request);
        //}

        protected override IEnumerable GetData()
        {
            try
            {
                return this.Db.QuizzesDefinitions
                           .All()
                           .Project()
                           .To<QuizAdminViewModel>()
                           .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override T GetById<T>(object id)
        {
            return this.Db.QuizzesDefinitions.Find(id) as T;
        }
    }
}