﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TODOList.Models
{
    public class SubTask
    {
        [Key]
        public int SubTaskId { get; set; }

        [Display(Name = "SubTask")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public bool Completion { get; set; }

        //public virtual int TaskId { get; set; }

        public virtual Task Task { get; set; }

        [Display(Name = "Category")]
        [DataType(DataType.Text)]
        public virtual Category Category { get; set; }
    }
}