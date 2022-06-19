using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class PartController : Controller
    {
        private IPartRepository repo;
        public int pageSize = 4;    // Количество товаров на странице
        public PartController(IPartRepository repo)
        {
            this.repo = repo;
        }


        public ViewResult List(string category, int page = 1)
        {
            PartsListViewModel model = new PartsListViewModel
            {
                Parts = repo.Parts
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(game => game.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                Paging = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repo.Parts.Count() :
                        repo.Parts.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}