using System.Web.Mvc;

namespace MyAbp.Web.Controllers
{
    public class HomeController : MyAbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}