using AutoMapper;
using EmployeeCrud.Data;
using EmployeeCrud.Data.UnitOfWorks;
using EmployeeCrud.Domain.IConnection;
using EmployeeCrud.Domain.IUnitOfWorks;
using EmployeeCrud.Domain.Models;
using EmployeeCrud.Domain.Models.Wrapper;
using EmployeeCrud.Services.Iservices;
using EmployeeCrud.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        IMapper _mapper;
        string connString;
        EmployeeDBContext _context;
        public EmployeeService(IMapper mapper,IConnection conn, EmployeeDBContext context)
        {
            _mapper = mapper;
            connString = conn.GetConnectionString();
            _context = context;
        }
        public Result<EmployeeVM> DeleteEmployee(int id)
        {
            using (DapUnitOfWork _unitOfWork = new DapUnitOfWork(connString))
            {
                var res=_unitOfWork.EmployeeRepository.DeleteEmployee(id);
                if (res)
                {
                    return Result<EmployeeVM>.Success("Record Deleted");
                }
                return Result<EmployeeVM>.Success("Record Cannot be deleted");
            }
        }

        public Result<IEnumerable<EmployeeVM>> GetAllEmployees()
        {
            using (DapUnitOfWork _unitOfWork = new DapUnitOfWork(connString))
            {
                var res = _mapper.Map<IEnumerable<EmployeeVM>>(_unitOfWork.EmployeeRepository.GetAllEmployee());
                return Result<IEnumerable<EmployeeVM>>.Success(res);
            }
        }

        public Result<EmployeeVM> GetEmployee(int? id)
        {
            using (DapUnitOfWork _unitOfWork = new DapUnitOfWork(connString))
            {
                var res=_mapper.Map<EmployeeVM>(_unitOfWork.EmployeeRepository.GetEmployee(id));
                return Result<EmployeeVM>.Success(res);
            }
        }
        public Result<EmployeeVM> PostEmployee(EmployeeVM employee)
        {
            using (DapUnitOfWork _unitOfWork = new DapUnitOfWork(connString))
            {
                var res = _mapper.Map<EmployeeVM>(_unitOfWork.EmployeeRepository.PostEmployee(_mapper.Map<Employee>(employee)));
                return Result<EmployeeVM>.Success(res);
            }
        }

        public Result<EmployeeVM> UpdateEmployee(EmployeeVM employee)
        {
            using (DapUnitOfWork _unitOfWork = new DapUnitOfWork(connString))
            {
                var res = _mapper.Map<EmployeeVM>(_unitOfWork.EmployeeRepository.UpdateEmployee(_mapper.Map<Employee>(employee)));
                return Result<EmployeeVM>.Success(res);
            }
        }
    }
}
