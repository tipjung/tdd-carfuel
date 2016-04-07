﻿using CarFuel.DataAccess;
using CarFuel.Models;
using CarFuel.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarFuel.Controllers
{
    public class CarsController : Controller
    {
        private ICarDb db;
        private CarService carService;

        public CarsController()
        {
            db = new CarDb();
            carService = new CarService(db);
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = new Guid(User.Identity.GetUserId());
            IEnumerable<Car> cars = carService.GetCarsByMember(userId);
            return View(cars);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Car item)
        {
            var userId = new Guid(User.Identity.GetUserId());

            try
            {
                carService.AddCar(item, userId);
            }
            catch (OverQuotaException ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = new Guid(User.Identity.GetUserId());
            var c = carService.GetCarsByMember(userId).SingleOrDefault(x => x.Id == id);

            return View(c);
        }
    }
}