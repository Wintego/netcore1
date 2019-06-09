using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Details()
        {
            return View(cartService.TransFromCart());
        } 
        public IActionResult DecrementFromCart(int id)
        {
            cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }
        public IActionResult RemoveFromCart(int id)
        {
            cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }
        public IActionResult RemoveAll()
        {
            cartService.RemoveAll();
            return RedirectToAction("Details");
        }
        public IActionResult AddToCart(int id)
        {
            cartService.AddToCart(id);
            return RedirectToAction("Details");
        }
    }
}