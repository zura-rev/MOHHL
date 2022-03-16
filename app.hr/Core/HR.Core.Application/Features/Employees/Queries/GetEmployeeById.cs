using AutoMapper;
using HR.Core.Application.DTOs;
using HR.Core.Application.Exceptions;
using HR.Core.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Queries
{
    public class GetEmployeeByIdRequest: IRequest<GetEmployeeDto>
    {
        public int Id { get; set; }
        public GetEmployeeByIdRequest(int id) 
        { 
            Id = id;
        }
    }

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdRequest, GetEmployeeDto>
    {

        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetEmployeeByIdHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<GetEmployeeDto> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken) 
        {
            return Task.FromResult(mapper.Map<GetEmployeeDto>(unit.EmployeeRepository.Read(request.Id)));
        }
      
    }

}
