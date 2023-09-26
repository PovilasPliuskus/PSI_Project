using Microsoft.AspNetCore.Mvc;

namespace SoftwareEngineeringProject.Controllers
{
    public class ProjectPagesController : Controller
    {
        public IActionResult NotePage()
        {
            return View();
        }
    }
}
