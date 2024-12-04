using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using RiraCRUD.Api.Protos.PerosnsEndpoint;
using RiraCRUD.Application.Person.Commands;
using RiraCRUD.Application.Person.Queries;
using RiraCRUD.Domain.Entities;

namespace RiraCRUD.Api.ProtoConfigurations
{
    public class PersonEndpointConfigurations : Profile
    {
        public PersonEndpointConfigurations()
        { 

            CreateMap<AddPersonRequest, AddPersonCommand>().ReverseMap();
             
            CreateMap<UpdatePersonCommand, UpdatePersonRequest>().ReverseMap();
             
            CreateMap<GetPersonsRequest, GetPersonQuery>().ReverseMap();

            CreateMap<DeletePersonCommand, DeletePersonRequest>().ReverseMap();
             
            CreateMap<GetPersonRequest, GetPersonQuery>().ReverseMap();
              
        }
    }
}
