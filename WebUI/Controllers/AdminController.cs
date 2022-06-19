using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;


namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IPartRepository partRepository;
        IShippingDetailRepository shipRepository;
        // GET: Admin

        public AdminController(IPartRepository pR, IShippingDetailRepository sR)
        {
            partRepository = pR;
            shipRepository = sR;
        }

        public ViewResult Index()
        {
            PartsAndOrdersModel model = new PartsAndOrdersModel
            {
                Parts = partRepository.Parts,
                Orders = shipRepository.Orders

            };
            return View(model);
        }

        public ViewResult Edit(int id)
        {
            Part part = partRepository.Parts
                .FirstOrDefault(p => p.Id == id);
            return View(part);
        }

        [HttpPost]
        public ActionResult Edit(Part part)
        {
            if (ModelState.IsValid)
            {
                partRepository.SavePart(part);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", part.Name);
                return RedirectToAction("Index");
            }
            else
            {
               
                return View(part);
            }

        }
        public ViewResult Create()
        {
            return View("Edit", new Part());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Part deletedPart = partRepository.DeletePart(id);
            if (deletedPart != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedPart.Name);
            }
            return RedirectToAction("Index");
        }
    }
}