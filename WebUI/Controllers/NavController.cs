using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {

        private IPartRepository repo;

        public NavController(IPartRepository repo)
        {
            this.repo = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repo.Parts
                  .Select(part => part.Category)
                   .Distinct()
                   .OrderBy(x => x);
            return PartialView(categories);

        }
    }
}