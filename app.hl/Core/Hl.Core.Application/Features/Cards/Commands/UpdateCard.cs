using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Interfaces;

namespace Hl.Core.Application.Features.Users.Commands
{
    public class UpdateCardRequest : IRequest<GetCardDto>
    {
        public int Id { get; set; }
        public string Note { get; set; }
    }

    public class UpdateCardHandler : IRequestHandler<UpdateCardRequest, GetCardDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public UpdateCardHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetCardDto> Handle(UpdateCardRequest request, CancellationToken cancellationToken)
        {
            var card  = unit.CardRepository.UpdateCard(request.Id, request.Note);
            await unit.SaveAsync();
            var result = mapper.Map<GetCardDto>(card);
            return await Task.FromResult(result);
        }
    }

}
