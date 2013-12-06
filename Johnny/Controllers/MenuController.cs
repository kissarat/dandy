using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using Johnny.Logic;
using Johnny.Data;

namespace Johnny.Controllers
{
    public class MenuController : DefaultController<Dish, MenuLogic, MenuData>
    {
        public MenuController() : base(new MenuLogic())
        {
        }
    }
}