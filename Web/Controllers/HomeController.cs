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
        public IActionResult Index(string station)
        {
            var ws = new WeatherStationLogic(new WatherStationRepository());
            var watherStations = ws.GetWatherStations();
            var chosenWatherStation = ChoseWatherStation(watherStations, station);
            ViewBag.WatherStationList = new SelectList(watherStations.Select(w => w.Name), chosenWatherStation.Name);
            ViewData["Temperature"] = ws.GetLastTemperatureMeasurement(chosenWatherStation.ExternalKey).Temperature.ToString("##.##");
            return View();
        }

        public IActionResult Temperature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Temperature([Bind("ExternalKey, Temperature")] ModelPost modelPost)
        {
            if (String.IsNullOrEmpty(modelPost.ExternalKey))
            {
                ViewData["Message"] = "Nie podano ExternalKey.";
                return View();
            }

            var ws = new WeatherStationLogic(new WatherStationRepository());
            ws.SaveTemperatureMeasurement(modelPost.ExternalKey, modelPost.Temperature);
            return View();
        }

        [HttpGet]
        public IActionResult Measurement(string station, string date)
        {
            var ws = new WeatherStationLogic(new WatherStationRepository());
            DateTime? chosenDateTemp = null;
            if (!String.IsNullOrEmpty(date))
                chosenDateTemp = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

            var watherStations = ws.GetWatherStations();
            var chosenWatherStation = ChoseWatherStation(watherStations, station);
            ViewBag.WatherStationList = new SelectList(watherStations.Select(w => w.Name), chosenWatherStation.Name);

            var dateMeasure = ws.GetTemperatureMeasurementsDates(chosenWatherStation.ExternalKey);
            var selectDate = dateMeasure.OrderByDescending(d => d.Date).Select(s => s.Date.ToString("yyyy.MM.dd")).ToList();
            ViewBag.DateList = new SelectList(selectDate, chosenDateTemp.HasValue ? chosenDateTemp.Value.ToString("yyyy.MM.dd") : selectDate.FirstOrDefault());

            var model = ws.GetTemperatureMeasurements(chosenWatherStation.ExternalKey, chosenDateTemp);
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        private WatherStation ChoseWatherStation(List<WatherStation> watherStationList, string station)
        {
            if (String.IsNullOrEmpty(station))
                return watherStationList.FirstOrDefault();
            else
                return watherStationList.Where(w => w.Name == station).FirstOrDefault();
        }
    }

    public class ModelPost
    {
        public string ExternalKey { get; set; }
        public decimal Temperature { get; set; }
    }

}
