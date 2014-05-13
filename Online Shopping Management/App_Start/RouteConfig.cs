using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Online_Shopping_Management
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CategorysList",
                "Model/Categories/List/",
                new { controller = "Model", action = "CategorysList"}
            );

            routes.MapRoute(
                 "SubCategoriesList",
                 "Model/SubCategories/List/{CategoryId}",
                 new { controller = "Model", action = "SubCategoriesList", CategoryId = 0}
            );

            routes.MapRoute(
                  "CategoryList",
                  "Product/Categories/List/",
                  new { controller = "Product", action = "CategoryList" }
              );

            routes.MapRoute(
                 "SubCategoryList",
                 "Product/SubCategories/List/{CategoryId}",
                 new { controller = "Product", action = "SubCategoryList", CategoryId = 0 }
            );

            routes.MapRoute(
                    "ModelList",
                    "Product/Models/List/{SubCategoryId}",
                    new { controller = "Product", action = "ModelList", SubCategoryId = 0 }
            );

            routes.MapRoute(
                      "CategoryListForImage",
                      "Image/Categories/List/",
                      new { controller = "Image", action = "CategoryListForImage" }
                  );

            routes.MapRoute(
                     "SubCategoryListForImage",
                     "Image/SubCategories/List/{CategoryId}",
                     new { controller = "Image", action = "SubCategoryListForImage", CategoryId = 0 }
                );

            routes.MapRoute(
                    "ModelListForImage",
                    "Image/Models/List/{SubCategoryId}",
                    new { controller = "Image", action = "ModelListForImage", SubCategoryId = 0 }
               );
            routes.MapRoute(
                "ProductListForImage",
                "Image/Products/List/{ModelId}",
                new { controller = "Image", action = "ProductListForImage", ModelId = 0 }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}