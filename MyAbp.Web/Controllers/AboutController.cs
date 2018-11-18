using System.Web.Mvc;

namespace MyAbp.Web.Controllers
{
    public class AboutController : MyAbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}