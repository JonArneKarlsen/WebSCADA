﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Threading.Tasks;
using System.Net;
using WatermentWebSCADA.CustomFilters;

namespace WatermentWebSCADA.Controllers
{
    public class EquipmentController : Controller
    {
        [AuthLog(Roles = "Admin, SuperUser, Maintenacnce, User")]
        // GET: Equipment with their measurred values.
        public ActionResult Index(int? id)
        {
            watermentdbEntities db = new watermentdbEntities(); //dbcontect class
            List<EquipmentVM> facilityEquipmentVM = new List<EquipmentVM>(); // to hold list of Customer and order details
            var equipmentlist = (from Eq in db.equipments.Where(x => x.facilities_Id == id)
                                select new {Eq.Id, Eq.Tag, Eq.SIUnits, Eq.Description, Eq.LastCalibrated, Eq.InstallDate, Eq.Manufacturer, Eq.TypeSpecification}).ToList();
            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in equipmentlist)
            {
                EquipmentVM fevm = new EquipmentVM(); // ViewModel
                fevm.Tag = item.Tag;
                fevm.SIUnits = item.SIUnits;
                fevm.Description = item.Description;
                fevm.LastCalibrated = item.LastCalibrated;
                fevm.InstallDate = item.InstallDate;
                fevm.Manufacturer = item.Manufacturer;
                fevm.TypeSpecification = item.TypeSpecification;
                facilityEquipmentVM.Add(fevm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(facilityEquipmentVM); //List of CustomerVM (ViewModel)
        }
        // GET: Equipment. Not used
        //public ActionResult IndexBACKUP()
        //{
        //    watermentdbEntities db = new watermentdbEntities(); //dbcontect class
        //    List<FacilityEquipmentVM> facilityEquipmentVM = new List<FacilityEquipmentVM>(); // to hold list of Customer and order details
        //    var customerlist = (from Eq in db.equipments
        //                        join Me in db.measurements on Eq.Id equals Me.equipments_Id
        //                        select new { Eq.Tag, Eq.SIUnits, Eq.Description, Me.ProcessValue, Me.Recorded }).ToList();
        //    //query getting data from database from joining two tables and storing data in customerlist
        //    foreach (var item in customerlist)
        //    {
        //        FacilityEquipmentVM objevm = new FacilityEquipmentVM(); // ViewModel
        //        objevm.Tag = item.Tag;
        //        objevm.SIUnits = item.SIUnits;
        //        objevm.Description = item.Description;
        //        //objevm.ProcessValue = item.ProcessValue;
        //        //objevm.Recorded = item.Recorded;
        //        facilityEquipmentVM.Add(objevm);
        //    }
        //    //Using foreach loop fill data from custmerlist to List<CustomerVM>.
        //    return View(facilityEquipmentVM); //List of CustomerVM (ViewModel)
        //}

        //This action result is used to open the partial view input box for adding equipment.

        [AuthLog(Roles = "Admin, SuperUser")]
        public ActionResult ViewCreate()
        {
            // return PartialView("_CreateEquipment", model);
            return View("CreateEquipment");
        }



        //This action result takes the input in the partial view and stores it to the DB before returning to the list of equipments.
        [AuthLog(Roles = "Admin, SuperUser")]
        [HttpPost]
        public ActionResult CreateEquipment(EquipmentAddVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEquipment", model);
            }
            else
            {

                var db = new watermentdbEntities();
                db.equipments.Add(new equipments
                {
                    Tag = model.Tag,
                    SIUnits = model.SIUnits,
                    Description = model.Description,
                    LastCalibrated = model.LastCalibrated,
                    InstallDate = model.InstallDate,
                    TypeSpecification = model.TypeSpecification,
                    Manufacturer = model.Manufacturer,
                    facilities_Id = model.facilities_Id
                    
                    
                    

                });
                //Need to create some error handling here.
                db.SaveChanges();
                ModelState.Clear();
            }

            return RedirectToAction("Index", new { id = model.facilities_Id });
        }

        [AuthLog(Roles = "Admin, SuperUser")]
        // GET: /Equipment/Delete/5
        public ActionResult Delete(int? id)
        {
            watermentdbEntities db = new watermentdbEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipments eq = db.equipments.Find(id);
            if (eq == null)
            {
                return HttpNotFound();
            }
            return View(eq);
        }

        [AuthLog(Roles = "Admin, SuperUser")]
        // POST: /Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            watermentdbEntities wdbe = new watermentdbEntities();
            equipments evm = wdbe.equipments.Find(id);
            wdbe.equipments.Remove(evm);
            wdbe.SaveChanges();
            return RedirectToAction("Details");
        }


    }
}