using AutoMapper;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Persons;
using RiraCRUD.Application.Common.Interfaces;
using RiraCRUD.Application.Person.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Person.Queries
{
    public record GetPersonQuery: IRequest<PersonDto?>
    {
        public int Id { get; set; }
    }
    internal class GetPerosnQueryHandler : IRequestHandler<GetPersonQuery, PersonDto?>
    {
        private readonly IGenericRepository<RiraCRUD.Domain.Entities.Person> _personRepository;
        private readonly IMapper _mapper;

        public GetPerosnQueryHandler(IGenericRepository<Domain.Entities.Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonDto?> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new Exception("");
            }
            try
            {
                return _mapper.Map<PersonDto>(await _personRepository.GetAsync(request.Id));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
