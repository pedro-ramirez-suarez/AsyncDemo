using FakeAsyncLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsyncMVC.Controllers
{
    public class DemoOldController : AsyncController
    {
        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var bg = new BackgroundWorker();
            bg.DoWork += bg_DoWork;
            bg.RunWorkerCompleted += (o, e) =>
            {
                AsyncManager.Parameters["result"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            bg.RunWorkerAsync();
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            var ops = new AsyncOperations();
            var result = ops.GetRandomNumber(10);
            e.Result = result;
        }

        public JsonResult IndexCompleted(int result)
        {
            return Json(new { number = result }, JsonRequestBehavior.AllowGet);
        }
    }
}