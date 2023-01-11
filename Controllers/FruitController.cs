using FruitMarketData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FruitMarketData.Controllers
{
    public class FruitController : Controller
    {
        // GET: Fruit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FruitList()
        {
            FruitDB fruitDB= new FruitDB();
            List<Fruit> fruits= fruitDB.allFruits();
            ViewBag.Fruits = fruits;
            return View();
        }

        public ActionResult editFruit(int id)
        {
            FruitDB fruitDB = new FruitDB();
            Fruit fruit = fruitDB.getFruitById(id);
            ViewBag.Fruit = fruit;
            return View(fruit);
        }

        public ActionResult editFruitSave(Fruit fruit, HttpPostedFileBase photo)
        {
            FruitDB fruitDB = new FruitDB();

            if(photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Server.MapPath("/Content/Image/Fruit"), fileName);
                photo.SaveAs(path);
                fruit.FruitImage = fileName;
            }
            fruitDB.saveEditFruit(fruit);
            List<Fruit> fruits = fruitDB.allFruits();
            return RedirectToAction("FruitList");
        }

        public ActionResult DeleteFruit(int id)
        {
            FruitDB fruitDB = new FruitDB();
            fruitDB.removeFruit(id);
            List<Fruit> fruits = fruitDB.allFruits();
            return RedirectToAction("FruitList");
        }

        public ActionResult CreateNew()
        {
            return View();
        }

        public ActionResult saveNewFruit(Fruit fruit, HttpPostedFileBase photo)
        {
            FruitDB fruitDB = new FruitDB();
            if (photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Server.MapPath("/Content/Image/Fruit"), fileName);
                photo.SaveAs(path);
                fruit.FruitImage = fileName;
            }
            fruitDB.newFruit(fruit);
            List<Fruit> fruits = fruitDB.allFruits();
            return RedirectToAction("FruitList");
        }
    }
}