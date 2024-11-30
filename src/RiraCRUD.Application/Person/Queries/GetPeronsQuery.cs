using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RiraCRUD.Application.Common.DTOs.Base;
using RiraCRUD.Application.Common.DTOs.Persons;
using RiraCRUD.Application.Common.Extensions;
using RiraCRUD.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Person.Queries
{
    public class GetPeronsQuery : GridFilterDto , IRequest<PageListDto<PersonDto>>
    {
    }
    internal class GetPeronsQueryHanlder : IRequestHandler<GetPeronsQuery, PageListDto<PersonDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public async Task<PageListDto<PersonDto>> Handle(GetPeronsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _appDbContext.Persons.ApplyGridFilter(request).ToListAsync();
            return new PageListDto<PersonDto>
            {
                Data = _mapper.Map<List<PersonDto>>(persons),
                DataCount = persons.Count()
            };
        }
    }
}
