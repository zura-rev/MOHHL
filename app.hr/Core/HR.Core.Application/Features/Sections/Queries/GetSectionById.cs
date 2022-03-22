using AutoMapper;
using FluentValidation;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Sections.Queries
{

    public class GetSectionByIdRequest : IRequest<GetSectionDto>
    {
        public int Id { get; set; }
        public GetSectionByIdRequest(int id)
        {
            this.Id = id;
        }
    }

    public class GetSectionByIdHandler : IRequestHandler<GetSectionByIdRequest, GetSectionDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetSectionByIdHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetSectionDto> Handle(GetSectionByIdRequest request, CancellationToken cancellationToken)
        {
            var section = unit.SectionRepository.Read(request.Id);
            return await Task.FromResult(mapper.Map<GetSectionDto>(section));
        }

     
    }

    public class GetSectionValidator : AbstractValidator<GetSectionByIdRequest>
    {
        public GetSectionValidator() { }
    }

}
