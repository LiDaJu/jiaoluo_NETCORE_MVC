using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jiaoluo.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        /// <summary>
        /// 通过依赖注入服务注入ILogger服务
        /// 将制定类型的控制器作为泛型参数
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "访问页面不存在";
                    _logger.LogWarning($"发生404错误，路径：{statusCodeResult.OriginalPath}");
                    ViewBag.Path = statusCodeResult.OriginalPath;//获取原始访问路径
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    break;
                default:
                    break;
            }
            return View("Index");
        }

        [AllowAnonymous]//允许匿名访问
        [Route("Error")]
        public IActionResult Error()
        {


            var iExceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"路径{iExceptionHandlerPathFeature.Path}，产生了错误信息：{iExceptionHandlerPathFeature.Error.Message}");
            //ViewBag.ExceptionHandlerPath = iExceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionHandlerErrorMsg = iExceptionHandlerPathFeature.Error.Message;//错误信息
            ViewBag.ExceptionHandlerErrorStackTrace = iExceptionHandlerPathFeature.Error.StackTrace;//堆栈信息
            return View("Error");
        }
    }
}
