using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace jiaoluo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "访问页面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;//获取原始访问路径
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    break;
                default:
                    break;
            }
            return View("Index");
        }
    }
}
