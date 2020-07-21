using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask.Models;

namespace ArmyTechTask.Controllers
{
    public class StudentController : Controller
    {
        readonly ArmyTechTaskEntities _context;

        public StudentController()
        {
            _context = new ArmyTechTaskEntities();
        }

        #region GET All Student

        public ActionResult Index()
        {
            return View(_context.Student.ToList());
        }

        #endregion

        #region Create New Student

        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(_context.Field, "ID", "Name");
            ViewBag.GovernorateId = new SelectList(_context.Governorate, "ID", "Name");
            ViewBag.NeighborhoodId = new SelectList(_context.Neighborhood, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Student.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldId = new SelectList(_context.Field, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(_context.Governorate, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(_context.Neighborhood, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

        #endregion

        #region Edit Student

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _context.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(_context.Field, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(_context.Governorate, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(_context.Neighborhood, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(student).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldId = new SelectList(_context.Field, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(_context.Governorate, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(_context.Neighborhood, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

        #endregion

        #region Delete Student

        public ActionResult Delete(int id)
        {
            Student student = _context.Student.Find(id);
            _context.Student.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}