﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace MVCDemoApp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(
           ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
