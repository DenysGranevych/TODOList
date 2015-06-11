//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TODOList.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Category")]
        public string Text { get; set; }
    }
}