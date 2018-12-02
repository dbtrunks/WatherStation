using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Business;
using Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        readonly WeatherStationLogic _WeatherStationLogic;
        public HomeController(IWeatherStationRepository WeatherStationRepository)
        {
            _WeatherStationLogic = new WeatherStationLogic(WeatherStationRepository);
        }

        public IActionResult Index(string station)
        {
            var WeatherStations = _WeatherStationLogic.GetWeatherStations();
            var chosenWeatherStation = ChoseWeatherStation(WeatherStations, station);
            ViewBag.WeatherStationList = new SelectList(WeatherStations.Select(w => w.Name), chosenWeatherStation.Name);
            var measurement = _WeatherStationLogic.GetLastTemperatureMeasurement(chosenWeatherStation.ExternalKey);
            ViewData["Temperature"] = measurement?.Temperature.ToString("##.##") ?? "_";
            return View();
        }

        public IActionResult Temperature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Temperature(string externalKey, decimal temperature)
        {
            if (String.IsNullOrEmpty(externalKey))
            {
                ViewData["Message"] = "Nie podano ExternalKey.";
                return View();
            }
            _WeatherStationLogic.SaveTemperatureMeasurement(externalKey, temperature);
            return View();
        }

        [HttpGet]
        public IActionResult Measurement(string station, string date)
        {
            DateTime? chosenDateTemp = null;
            if (!String.IsNullOrEmpty(date))
                chosenDateTemp = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

            var WeatherStations = _WeatherStationLogic.GetWeatherStations();
            var chosenWeatherStation = ChoseWeatherStation(WeatherStations, station);
            ViewBag.WeatherStationList = new SelectList(WeatherStations.Select(w => w.Name), chosenWeatherStation.Name);

            var dateMeasure = _WeatherStationLogic.GetTemperatureMeasurementsDates(chosenWeatherStation.ExternalKey);
            var selectDate = dateMeasure.OrderByDescending(d => d.Date).Select(s => s.Date.ToString("yyyy.MM.dd")).ToList();
            ViewBag.DateList = new SelectList(selectDate, chosenDateTemp.HasValue ? chosenDateTemp.Value.ToString("yyyy.MM.dd") : selectDate.FirstOrDefault());

            var model = _WeatherStationLogic.GetTemperatureMeasurements(chosenWeatherStation.ExternalKey, chosenDateTemp);
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        private WeatherStation ChoseWeatherStation(List<WeatherStation> WeatherStationList, string station)
        {
            if (String.IsNullOrEmpty(station))
                return WeatherStationList.FirstOrDefault();
            else
                return WeatherStationList.Where(w => w.Name == station).FirstOrDefault();
        }
    }
}
