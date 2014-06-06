using FakeAsyncLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncMVC.Controllers
{
    public class DemoNewController : AsyncController
    {
        // GET: DemoNew
        public async Task<JsonResult> AsyncMethod()
        {
            var ops = new AsyncOperations();
            var result = await ops.GetRandomNumberAsync(10);
            return Json(new { number = result }, JsonRequestBehavior.AllowGet);
        }



        public  JsonResult SyncMethod()
        {
            var ops = new AsyncOperations();
            var result = ops.GetRandomNumber(10);
            return Json(new { number = result }, JsonRequestBehavior.AllowGet);
        }
    }
}