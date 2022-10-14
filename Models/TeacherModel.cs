using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWebAPIMVC.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_Email { get; set; }
        public string Teacher_ContactNo { get; set; }
        public string Teacher_Department { get; set; }
        public string Teacher_Address { get; set; }
    }
}