﻿namespace QuizFactory.Mvc.Areas.Common.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;
    using AutoMapper;
    //using Kendo.Mvc.Extensions;
    // using Kendo.Mvc.UI;
    using QuizFactory.Data;
    using QuizFactory.Data.Common;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using QuizFactory.Mvc.Controllers;

    public abstract class KendoGridAdministrationController : BaseController
    {
        public KendoGridAdministrationController(IQuizFactoryData data)
            : base(data)
        {
        }

        //[HttpPost]
        //public ActionResult Read([DataSourceRequest]
        //                         DataSourceRequest request)
        //{
        //    var ads = this.GetData().ToDataSourceResult(request);

        //    return this.Json(ads);
        //}

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : QuizAdminViewModel
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);

                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
            }
        }

        //protected JsonResult GridOperation<T>(T model, [DataSourceRequest] DataSourceRequest request)
        //{
        //    return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        //}

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Db.Context.Entry(dbModel);
            entry.State = state;
            this.Db.SaveChanges();
        }
    }
}