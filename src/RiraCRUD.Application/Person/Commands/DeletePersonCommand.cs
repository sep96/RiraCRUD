using AutoMapper;
using MediatR;
using RiraCRUD.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Person.Commands
{
    public record DeletePersonCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    internal class DeletePersonCommanHanlder : IRequestHandler<DeletePersonCommand, bool>
    {

        private readonly IGenericRepository<RiraCRUD.Domain.Entities.Person> _personRepository;

        public DeletePersonCommanHanlder(IGenericRepository<Domain.Entities.Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("");
            }
            try
            {
                await _personRepository.DeleteAsync(request.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
