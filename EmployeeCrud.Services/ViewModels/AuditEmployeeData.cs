using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.ViewModels
{
    public class AuditEmployeeData
    { 
        public int? EmployeeId { get; set; }
        public string? PreviousValue { get; set; }
        public string? CurrentValue { get; set; }
        public string? Operation { get; set; }
    }
}
