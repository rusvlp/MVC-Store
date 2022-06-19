using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPartRepository repository;
        private IShippingDetailRepository orderProcessor;

        public ViewResult Checkout()
        {
            return View(new ShippingDetail());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetail shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                List<CartLine> cls = cart.GetCartLines();

                foreach (CartLine cl in cls)
                {
                    cl.Part.Quantity -= cl.Quantity;
                    repository.SavePart(cl.Part);
                }

                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }


        public CartController(IPartRepository repo, IShippingDetailRepository processor)
        {
            orderProcessor = processor;
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            if (TempData["ErrorMsg"] != null)
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
            }
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(p => p.Id == Id);

            if (part != null)
            {
                try
                {
                    cart.AddItem(part, 1);
                }
                catch (Exception e)
                {

                    TempData["ErrorMsg"] = "Ошибка! Невозможно добавить '" + part.Name +"', так как недостаточно товара на складе!";
                    return RedirectToAction("Index", new { returnUrl });
                }
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(p => p.Id == Id);

            if (part != null)
            {
                cart.RemoveLine(part);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}