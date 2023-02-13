using EmployeeCrud.Data.Reopsitories.EFCoreRepository;
using EmployeeCrud.Domain.IRepositories;
using EmployeeCrud.Domain.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private EmployeeDBContext _context;
        public IEmployeeRepository EmployeeRepository { get; set; }

        public IAuthRepository AuthRepository => throw new NotImplementedException();

        public UnitOfWork(EmployeeDBContext context)
        {
            _context = context;
            EmployeeRepository = new EFCoreEmployeeRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(_context != null) { _context.Dispose(); }
        }
    }
}
