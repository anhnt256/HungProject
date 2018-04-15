using ImagerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelancer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ResizeImage(HttpPostedFileBase file) {
            string imgPath = GetImgPath("~/Upload/");
            int width = 180;
            int height = 180;

            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(imgPath, filename);
            file.SaveAs(path);

            string txtOutputFileName = DateTime.Now.Ticks.ToString() + ".jpg";

            Imager.PerformImageResizeAndPutOnCanvas(imgPath, filename, width, height, txtOutputFileName);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            System.IO.File.Delete(path);
            
            System.Drawing.Image imgBef;
            imgBef = System.Drawing.Image.FromFile(imgPath + txtOutputFileName);

            byte[] imgByteFile = null;
            imgByteFile = Imager.imageToByteArray(imgBef);

            txtOutputFileName = Imager.GetImage(imgByteFile);
            return Json(new { data = txtOutputFileName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(HttpPostedFileBase file, string share_text, string color) {
            string imgPath = GetImgPath("~/Gallery/");
            // Insert FileName to Database and Return Database ID
            return Json(new {
                status = 1,
                share_link = "return"
            }, JsonRequestBehavior.AllowGet);
        }

        #region helper
        private string GetImgPath(string folder) {
            string imgPath = "";
            if (!Directory.Exists(Server.MapPath(folder)))
            {
                Directory.CreateDirectory(Server.MapPath(folder));
            }
            imgPath = Server.MapPath(folder);
            return imgPath;
        }
        #endregion

    }
}