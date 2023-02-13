using AutoMapper;
using EmployeeCrud.Domain.Models;
using EmployeeCrud.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<UserVM, User>().ReverseMap();
        }
    }
}
