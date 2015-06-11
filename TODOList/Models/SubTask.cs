//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TODOList.Models
{
    public class SubTask
    {
        public int SubTaskId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public bool Completion { get; set; }

        public virtual int TaskId { get; set; }

        public virtual int CategoryId { get; set; }
    }
}