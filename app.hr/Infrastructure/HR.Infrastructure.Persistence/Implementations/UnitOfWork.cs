using HR.Core.Application.Interfaces;
using HR.Core.Application.Interfaces.Repositories;
using HR.Infrastructure.Persistence.Implementations.Repositories;
using System.Threading.Tasks;

namespace HR.Infrastructure.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IPositionRepository positionRepository;
        private IEmployeeRepository employeeRepository;
        private IOrderRepository orderRepository;
        private IOrderTypeRepository orderTypeRepository;
        private ISectionRepository sectionRepository;
        private IStructureRepository structureRepository;


        private readonly DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public IPositionRepository PositionRepository => positionRepository ??= new PositionRepository(context);
        public IEmployeeRepository EmployeeRepository => employeeRepository ??= new EmployeeRepository(context);
        public IOrderRepository OrderRepository => orderRepository ??= new OrderRepository(context);
        public IOrderTypeRepository OrderTypeRepository => orderTypeRepository ??= new OrderTypeRepository(context);
        public ISectionRepository SectionRepository => sectionRepository ??= new SectionRepository(context);
        public IStructureRepository StructureRepository => structureRepository ??= new StructureRepository(context);


        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

    }
}
