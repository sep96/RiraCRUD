using AutoMapper;
using Grpc.Core;
using MediatR;
using RiraCRUD.Api.Protos.PerosnsEndpoint;
using RiraCRUD.Application.Person.Commands;
using RiraCRUD.Application.Person.Queries;
using RiraCRUD.Infrastructure.Persistence.Contexts;
using static RiraCRUD.Api.Protos.PerosnsEndpoint.PersonService;

namespace RiraCRUD.Api.Services
{
    public class PersonService : PersonServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<AddPersonResponse> AddPerson(AddPersonRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(_mapper.Map<AddPersonCommand>(request));
            return new AddPersonResponse { IsSuccess = result };
        }

        public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(_mapper.Map<DeletePersonCommand>(request));
            return new DeletePersonResponse { IsSuccess = result };
        }

        public override async Task<GetPersonsResponse> GetPersons(GetPersonsRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(_mapper.Map<GetPeronsQuery>(request));
            return new GetPersonsResponse
            {
               Persons = new PageListDto
               {
                   Data = result.Data ,
                   DataCount = result.DataCount ,
               }
            };
        }

        public override async Task<GetPersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetPersonQuery(request.PersonId));
            return result == null ? null : new GetPersonResponse { Person = new PersonDto { Id = result.Id, Name = result.Name, Family = result.Family } };
        }

        public override async Task<UpdatePersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdatePersonCommand(request.Id, request.Name, request.Family));
            return new UpdatePersonResponse { IsSuccess = result };
        }
    }
}
