using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johnny.Logic;
using Johnny.Models;
using Johnny.Data;

namespace Johnny.Controllers
{
    public abstract class DefaultController<T, TLogic, TRepository> : Controller
        where T : Model
        where TLogic : DefaultLogic<T, TRepository>
        where TRepository : Repository<T>
    {
        protected readonly TLogic logic;

        protected DefaultController(TLogic logic)
        {
            this.logic = logic;
        }

        public virtual ActionResult Index()
        {
            return Render(logic.Read());
        }

        public virtual ActionResult Create()
        {
            return Render("Edit", logic.Create());
        }

        public virtual ActionResult Details(int id)
        {
            return Render(logic.Read(id));
        }

        public virtual ActionResult Edit(int id)
        {
            return Render(logic.Read(id));
        }

        [HttpPost]
        public virtual ActionResult Save(T entity)
        {
            if (ModelState.IsValid)
            {
                logic.Save(entity);
                return RedirectToAction("Index");
            }
            return Render("Edit", entity);
        }

        public virtual ActionResult Delete(int id)
        {
            logic.Delete(id);
            return RedirectToAction("Index");
        }

        public virtual ActionResult ChangeVisibility(int id, bool isVisible)
        {
            //try
            //{
                logic.ChangeVisibility(id, isVisible);
                return RenderVoidJson(id);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }

        protected JsonResult RenderVoidJson(int Id, Exception ex = null)
        {
            return Json(new {success = true, id = Id});
        }

        [NonAction]
        protected ActionResult Render(object model)
        {
            if (Helper.IsAjax())
            {
                return View(model);
            }
            return PartialView(model);
        }

        [NonAction]
        protected ActionResult Render(string viewName, object model)
        {
            if (Helper.IsAjax())
            {
                return View(viewName, model);
            }
            return PartialView(viewName, model);
        }
    }
}