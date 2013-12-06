using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johnny.Models;
using Johnny.Logic;
using Johnny.Data;

namespace Johnny.Controllers
{
    public class ArticleController : DefaultController<Article, ArticleLogic, ArticleData>
    {
        public ArticleController()
            :base(new ArticleLogic())
        {
        }
    }
}
