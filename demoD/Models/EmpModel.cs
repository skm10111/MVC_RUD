using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoD.Models
{
    public class EmpModel
    {
        public int Id { get; set; }
        public string EmpemployeName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public string Status { get; set; } = "Pending";
        public string Description { get; set; }
    }
    public enum LeaveType
    {
      CL,
      LWP,
      HL,
    }
}