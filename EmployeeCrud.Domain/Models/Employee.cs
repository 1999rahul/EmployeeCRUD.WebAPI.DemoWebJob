using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Domain.Models
{
    [Table("tblEmployee")]
    public class Employee
    {
        [Column("Employee_Id")]
        public int? EmployeeId { get; set; }
        [Column("Full_Name")]
        public string FullName { get; set; }   
        public string DOB { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }


    }
}
