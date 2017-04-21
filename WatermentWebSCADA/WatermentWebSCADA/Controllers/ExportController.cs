﻿
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class ExportController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();


        public ActionResult ExportTempMeasurementsToExcel(int? id)
        {
            //Code to export Temperature measurements to Excel file
            using (var db = new Models.watermentdbEntities())
            {
           
                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.measurements.Where(x => x.equipments_facilities_Id == id).Where(x => x.equipments.SIUnits == "Degrees").ToList()
                                  select new
                                  {
                                      //Getting information from database, and tell the excel file what to get.
                                      ID = d.Id,
                                      ProcessValue = d.ProcessValue,
                                      Recorded = d.Recorded,
                                      Equipment = d.equipments_Id,
                                      EqupmentName = d.equipments.Tag


                                  };

                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=TempMeasurementList.xls");
                Response.ContentType = "application/excel";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);

                grid.RenderControl(hwriter);

                Response.Write(swriter.ToString());

                Response.End();

                return null;
            }
        }
        public ActionResult ExportBarMeasurementsToExcel(int? id)
        {
            //Code to export Bar measurements to Excel file
            using (var db = new Models.watermentdbEntities())
            {

                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.measurements.Where(x => x.equipments_facilities_Id == id).Where(x => x.equipments.SIUnits == "Bar").ToList()
                                  select new
                                  {
                                      //Getting information from database, and tell the excel file what to get.
                                      ID = d.Id,
                                      ProcessValue = d.ProcessValue,
                                      Recorded = d.Recorded,
                                      Equipment = d.equipments_Id,
                                      EqupmentName = d.equipments.Tag


                                  };

                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=BarMeasurementList.xls");
                Response.ContentType = "application/excel";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);

                grid.RenderControl(hwriter);

                Response.Write(swriter.ToString());

                Response.End();

                return null;
            }
        }

        public ActionResult ExportAlarmsToExcel(int? id)
        {
            //Code to export Alarm list to Excel file
            using (var db = new Models.watermentdbEntities())
            {

                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.alarms.Where(x => x.equipments_facilities_Id == id).ToList()
                                  select new
                                  {
                                      //Getting information from database, and tell the excel file what to get.
                                      ID = d.Id,
                                      ProcessValue = d.ProcessValue,
                                      Recorded = d.AlarmOccured,
                                      Status = d.Status,
                                      EqupmentName = d.equipments.Tag,
                                      Description = d.Description


                                  };

                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=AlarmList.xls");
                Response.ContentType = "application/excel";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);

                grid.RenderControl(hwriter);

                Response.Write(swriter.ToString());

                Response.End();

                return null;
            }
        }

    }
}
