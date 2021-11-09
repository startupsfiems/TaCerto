using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteLogado]
    public class HomeController : ControladoraBase {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}