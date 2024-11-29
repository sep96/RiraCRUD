using AutoMapper;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Persons;
using RiraCRUD.Application.Common.Interfaces;
using RiraCRUD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Person.Commands
{
    public class AddPersonCommand : AddPersonDto , IRequest<bool>
    {
    }
    internal class AddPersonCommandHadnler : IRequestHandler<AddPersonCommand, bool>
    {
        private readonly IGenericRepository<RiraCRUD.Domain.Entities.Person> _personRepository;
        private readonly IMapper _mapper;
        public AddPersonCommandHadnler(IGenericRepository<Domain.Entities.Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("");
            }
            try
            {
                await _personRepository.AddAsync(_mapper.Map<RiraCRUD.Domain.Entities.Person>(request));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
