using System.Drawing;
using System.Web.Mvc;
using Web.Annotations;

namespace Web.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public void Stuff()
    {
      var a = Color.FromArgb(255, 0, 0);
      var b = Color.FromName("red");
      var c = Color.Red;
    }

    void RenderInput([HtmlElementAttributes] object attr)
    {
      RenderInput(new { style = "color:red"});
      RenderInput(new { style = "background:#f00"});
      RenderInput(new { style = "background:rgb(255,0,0)"});
      RenderInput(new { style = "background: rgb(100%,0%,0%)"});
    }
  }
}
