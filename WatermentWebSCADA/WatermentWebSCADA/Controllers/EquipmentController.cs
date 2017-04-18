﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class EquipmentController : Controller
    {
        // GET: Equipment with their measurred values.
        public ActionResult Index(int? id)
        {
            watermentdbEntities db = new watermentdbEntities(); //dbcontect class
            List<FacilityEquipmentVM> facilityEquipmentVM = new List<FacilityEquipmentVM>(); // to hold list of Customer and order details
            var equipmentlist = (from Eq in db.equipments.Where(x => x.facilities_Id == id)
                                select new { Eq.Tag, Eq.SIUnits, Eq.Description, Eq.LastCalibrated}).ToList();
            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in equipmentlist)
            {
                FacilityEquipmentVM objevm = new FacilityEquipmentVM(); // ViewModel
                objevm.Tag = item.Tag;
                objevm.SIUnits = item.SIUnits;
                objevm.Description = item.Description;
                objevm.LastCalibrated = item.LastCalibrated;
                facilityEquipmentVM.Add(objevm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(facilityEquipmentVM); //List of CustomerVM (ViewModel)
        }
        // GET: Equipment
        public ActionResult IndexBACKUP()
        {
            watermentdbEntities db = new watermentdbEntities(); //dbcontect class
            List<FacilityEquipmentVM> facilityEquipmentVM = new List<FacilityEquipmentVM>(); // to hold list of Customer and order details
            var customerlist = (from Eq in db.equipments
                                join Me in db.measurements on Eq.Id equals Me.equipments_Id
                                select new { Eq.Tag, Eq.SIUnits, Eq.Description, Me.ProcessValue, Me.Recorded }).ToList();
            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in customerlist)
            {
                FacilityEquipmentVM objevm = new FacilityEquipmentVM(); // ViewModel
                objevm.Tag = item.Tag;
                objevm.SIUnits = item.SIUnits;
                objevm.Description = item.Description;
                //objevm.ProcessValue = item.ProcessValue;
                //objevm.Recorded = item.Recorded;
                facilityEquipmentVM.Add(objevm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(facilityEquipmentVM); //List of CustomerVM (ViewModel)
        }
    }
}