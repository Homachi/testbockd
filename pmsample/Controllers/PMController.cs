using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using pmsample.Models;

namespace pmsample.Controllers
{
    public class PMController : Controller
    {
        public IActionResult Index()
        {
            var viewModeld = new PM();
            return View(viewModeld);
        }


        [HttpPost]
        public IActionResult Index(string station)
        {
            var viewModel = new PM(station);
            
            return View(viewModel);
        }

    }


    [Route("api/[controller]")]
    public class StationDataController : Controller
    {
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(SampleData.Stations, loadOptions);
        }
    }
}
