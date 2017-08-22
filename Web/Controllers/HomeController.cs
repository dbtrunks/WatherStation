using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business;
using Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Temperature()
        {
            var ws = new WeatherStationLogic(new WatherStationRepository());
            ViewData["Temperature"] = ws.GetLastTemperatureMeasurement("2106E356-4F23-4167-AC8F-D45290A20F9A").Temperature.ToString("##.##");
            return View();
        }

        [HttpPost]
        public IActionResult Temperature([Bind("ExternalKey, Temperature")] ModelPost modelPost)
        {
            if(String.IsNullOrEmpty(modelPost.ExternalKey))
            {
              ViewData["Message"] = "Nie podano ExternalKey.";
              return View();
            }
            
            var ws = new WeatherStationLogic(new WatherStationRepository());
            ws.SaveTemperatureMeasurement(modelPost.ExternalKey,modelPost.Temperature);
            return  View();
        }

        [HttpGet]
        public IActionResult Contact(string  id)
        {
            DateTime? dateTemp = null;
            if (!String.IsNullOrEmpty(id))
            dateTemp = DateTime.ParseExact(id,"yyyyMMdd", CultureInfo.InvariantCulture);

            var ws = new WeatherStationLogic(new WatherStationRepository());
            var model = ws.GetTemperatureMeasurements("2106E356-4F23-4167-AC8F-D45290A20F9A", dateTemp);

            var dateMeasure = ws.GetTemperatureMeasurementsDates("2106E356-4F23-4167-AC8F-D45290A20F9A");
            var selectList = dateMeasure.OrderByDescending(d => d.Date).Select(s => s.Date.ToString("yyyy.MM.dd")).ToList();
            ViewBag.DateList = new SelectList(selectList, dateTemp.HasValue ? dateTemp.Value.ToString("yyyy.MM.dd") : selectList.FirstOrDefault());
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }

    public class ModelPost
    {
        public string ExternalKey {get; set;}
        public decimal Temperature {get; set;}
    }

}
