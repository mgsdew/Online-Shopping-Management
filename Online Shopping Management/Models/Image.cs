using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shopping_Management.Models
{
    public class Image
    {
        [Key]
        public int ImageId { set; get; }
        public int? CategoryId { set; get; }
        public virtual Category Category { set; get; }
        public int? SubCategoryId { set; get; }
        public virtual SubCategory SubCategory { get; set; }
        public int? ModelId { set; get; }
        public virtual Model Model { set; get; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public IEnumerable<HttpPostedFile> ImageFile { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
    }
}