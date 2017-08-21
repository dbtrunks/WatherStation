using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business;
using Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Contact()
        {
            var ws = new WeatherStationLogic(new WatherStationRepository());
            var model = ws.GetTemperatureMeasurements("2106E356-4F23-4167-AC8F-D45290A20F9A");
            //return View(model);
            var model2 = ws.GetTemperatureMeasurements("2106E356-4F23-4167-AC8F-D45290A20F9A").GroupBy(t => t.DateTime.Date).Select(grp => grp.ToList()).ToList();
            var selectList = model2.Select(m => m[0].DateTime.Date).OrderByDescending(d => d.Date).Select(s => s.Date.ToString("yyyy.MM.dd")).ToList();
            ViewBag.DateList = new SelectList(selectList, selectList.FirstOrDefault());
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
