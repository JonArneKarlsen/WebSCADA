﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Net;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Common;
using System.Web.Helpers;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Controllers
{
    public class FacilityController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();
       
        int LandId1;
        int LokasjonsID;
        int BrukerID;
        // GET: Facility
        public ActionResult FacilityDetails(int? id)
        {
            foreach (var item in db.facilities.Where(c=>c.Id==id))
            {
                LandId1 = item.locations_countries_Id;
                LokasjonsID = item.locations_Id;
             
            }


            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    Alarmer = db.alarms.Where(x=>x.equipments_facilities_Id==id).Where(o=>o.Status=="Active").ToList(),
                    Anlegg = db.facilities.Where(c => c.Id == id).ToList(),

                    //Anlegg2=db.facilities.Where(x=>x.locations_countries_Id=landid),

                    Kontinenter = db.continents.ToList(),

                    Land = db.countries.Where(x => x.Id == LandId1).ToList(), /*Se her mer 167 som lokal variabel fra koden før*/
                    //Land = db.countries.ToList(),
                    Utstyr = db.equipments.ToList(),
                    Lokasjoner = db.locations.Where(x=>x.Id==LokasjonsID).ToList(),
                    Vedlikehold = db.maintenance.ToList(),
                    Roller = db.roles.ToList(),
                    Brukere = db.users.Where(x=>x.locations_Id==LokasjonsID).ToList(),
                    Sesjoner = db.sessions.ToList(),

                };

                return View(model);
            }
        }
    }
}