using EFCodeFirstApproach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFCodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if(ModelState.IsValid==true)
            {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.InsertMessage = "<script>alert('Data inserted')</script>";
                    TempData["InsertMessage"]= "<script>alert('Data inserted')</script>";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Data not inserted')</script>";
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if(ModelState.IsValid==true)
            {
                db.Entry(s).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Data updated')</script>";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert('Data updated')</script>";
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if(id>0)
            {
                var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
                db.Entry(row).State = EntityState.Deleted;
                int a = db.SaveChanges();
                if(a>0)
                {
                    TempData["DeleteMessage"]= "<script>alert('Data Deleted')</script>";
                }
                else
                {
                    TempData["DeleteMessage"] = "<script>alert('Data Not Deleted')</script>";
                }
            }
            return RedirectToAction("Index");
        }
    }
}