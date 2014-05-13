using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Shopping_Management.Models;

namespace Online_Shopping_Management.Controllers
{ 
    [Authorize]
    public class ImageController : Controller
    {
        private ShoppingDbContext db = new ShoppingDbContext();

        //
        // GET: /Image/
    [AllowAnonymous]
        public ActionResult Index()
        {
            var images = db.Images.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Model).Include(i => i.Product);
            return View(images.ToList());
        }

        [HttpPost]
        public ActionResult Upload()
        {
            foreach (string file in Request.Files)
            {
                var postedFile = Request.Files[file];
                postedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + Path.GetFileName(postedFile.FileName));
                

            }
            return RedirectToAction("Create");
        }
        public ActionResult List()
        {
            var uploadedFiles = new List<Image>();

            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var picture = new Image() { Name = Path.GetFileName(file) };
                picture.Size = fileInfo.Length;

                picture.Path = ("~/UploadedFiles/") + Path.GetFileName(file);
                uploadedFiles.Add(picture);
            }

            return View(uploadedFiles);
        }

        public ActionResult CategoryListForImage()
        {
            var categories = db.Categorys.ToList();

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(
                        new SelectList(
                            categories,
                            "CategoryId",
                            "Name"
                            ), JsonRequestBehavior.AllowGet
                    );
            }

            return View(categories);
        }

        public ActionResult SubCategoryListForImage(int CategoryId)
        {
            var subcategories = from sc in db.SubCategories
                                where sc.CategoryId == CategoryId
                                select sc;

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(
                    new SelectList(
                        subcategories,
                        "SubCategoryId",
                        "SubCategoryName"
                        ), JsonRequestBehavior.AllowGet
                    );
            }
            return View(subcategories);
        }

        public ActionResult ModelListForImage(int SubCategoryId)
        {
            var models = from m in db.Models
                         where m.SubCategoryId == SubCategoryId
                         select m;

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(
                    new SelectList(
                         models,
                        "ModelId",
                        "ModelName"
                        ), JsonRequestBehavior.AllowGet
                    );
            }
            return View(models);
        }

        public ActionResult ProductListForImage(int ModelId)
        {
            var products = from p in db.Products
                           where p.ModelId == ModelId
                           select p;

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(
                    new SelectList(
                         products,
                        "ProductId",
                        "ProductName"
                        ), JsonRequestBehavior.AllowGet
                    );
            }
            return View(products);
        }
    [AllowAnonymous]

        //
        // GET: /Image/Details/5

        public ActionResult Details(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // GET: /Image/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName");
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        //
        // POST: /Image/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", image.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", image.SubCategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", image.ModelId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", image.ProductId);
            return View(image);
        }

        //
        // GET: /Image/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", image.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", image.SubCategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", image.ModelId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", image.ProductId);
            return View(image);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", image.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", image.SubCategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", image.ModelId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", image.ProductId);
            return View(image);
        }

        //
        // GET: /Image/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}