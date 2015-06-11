using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TODOList.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Task")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }


        public bool Completion { get; set; }

        public virtual List<SubTask> ListSubtask { get; set; }

        [Display(Name = "Category")]
        [DataType(DataType.Text)]
        public virtual Category Category { get; set; }

        //public int CategoryID { get; set; }

        //це для того, щоб можна було сбворитинове завдання з новою назвою категорії.
        //заню це не правильно, але по іншому в мене не вийшло реалізувати
        //test commit
        [Display(Name = "Category")]
        [DataType(DataType.Text)]
        public string TextCategory { get; set; }
    }

    
}