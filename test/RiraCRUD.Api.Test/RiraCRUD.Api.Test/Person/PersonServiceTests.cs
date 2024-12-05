using AutoMapper;
using Grpc.Core;
using MediatR;
using Moq;
using RiraCRUD.Api.Protos.PerosnsEndpoint;
using RiraCRUD.Api.Services;
using RiraCRUD.Application.Common.DTOs.Base;
using RiraCRUD.Application.Person.Commands;
using RiraCRUD.Application.Person.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonService = RiraCRUD.Api.Services.PersonService;

namespace RiraCRUD.Api.Test.Person
{
    public class PersonServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _personService = new PersonService(_mediatorMock.Object, _mapperMock.Object);
        }
         
        [Fact]
        public async Task AddPerson_ShouldReturnSuccess_WhenMediatorReturnsTrue()
        {
            // Arrange
            var request = new AddPersonRequest
            {
                AddPersonDto = new AddPersonDto
                {
                    FirstName = "Sepehr",
                    LastName = "Ashtari",
                    NationalId = "123456789",
                    DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
                }
            };

            var command = new AddPersonCommand();
            _mapperMock.Setup(m => m.Map<AddPersonCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(true);

            // Act
            var response = await _personService.AddPerson(request, null);

            // Assert
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task AddPerson_ShouldReturnFailure_WhenMediatorReturnsFalse()
        {
            // Arrange
            var request = new AddPersonRequest
            {
                AddPersonDto = new AddPersonDto
                {
                    FirstName = "Sepehr",
                    LastName = "Ashtari",
                    NationalId = "123456789",
                    DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
                }
            };

            var command = new AddPersonCommand();
            _mapperMock.Setup(m => m.Map<AddPersonCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(false);

            // Act
            var response = await _personService.AddPerson(request, null);

            // Assert
            Assert.False(response.IsSuccess);
        }
          
        [Fact]
        public async Task DeletePerson_ShouldReturnSuccess_WhenMediatorReturnsTrue()
        {
            // Arrange
            var request = new DeletePersonRequest { Id = 1 };
            var command = new DeletePersonCommand { Id = 1 };
            _mapperMock.Setup(m => m.Map<DeletePersonCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(true);

            // Act
            var response = await _personService.DeletePerson(request, null);

            // Assert
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task DeletePerson_ShouldReturnFailure_WhenMediatorReturnsFalse()
        {
            // Arrange
            var request = new DeletePersonRequest { Id = 1 };
            var command = new DeletePersonCommand { Id = 1 };
            _mapperMock.Setup(m => m.Map<DeletePersonCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(false);

            // Act
            var response = await _personService.DeletePerson(request, null);

            // Assert
            Assert.False(response.IsSuccess);
        }
         
        [Fact]
        public async Task GetPersons_ShouldReturnPersons_WhenMediatorReturnsData()
        {
            // Arrange
            var request = new GetPersonsRequest();
            var query = new GetPeronsQuery();
             
            var pageListDto = new PageListDto<Application.Common.DTOs.Persons.PersonDto>
            {
                DataCount = 1,
                Data = new List<Application.Common.DTOs.Persons.PersonDto>
                {
                    new Application.Common.DTOs.Persons.PersonDto
                    {
                        Id = 1,
                        FirstName = "Sepehr",
                        LastName = "Ashtari"
                    }
                }
            };
             
            var mappedPersonDto = new Api.Protos.PerosnsEndpoint.PersonDto
            {
                Id = 1,
                FirstName = "Sepehr",
                LastName = "Ashtari"
            };

            _mapperMock.Setup(m => m.Map<GetPeronsQuery>(It.IsAny<GetPersonsRequest>()))
                .Returns(query);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPeronsQuery>(), default))
                .ReturnsAsync(pageListDto);
             
            _mapperMock.Setup(m => m.Map<Api.Protos.PerosnsEndpoint.PersonDto>(
                It.IsAny<Application.Common.DTOs.Persons.PersonDto>()))
                .Returns(mappedPersonDto);

            // Act
            var response = await _personService.GetPersons(request, null);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Persons.DataCount);
            Assert.Single(response.Persons.Data);
            Assert.Equal(1, response.Persons.Data[0].Id);
            Assert.Equal("Sepehr", response.Persons.Data[0].FirstName);
        }

         
        [Fact]
        public async Task GetPerson_ShouldReturnCorrectPerson()
        {
            // Arrange
            var request = new GetPersonRequest { Id = 1 };
            var query = new GetPersonQuery { Id = 1 };
            var personDto = new PersonDto
            {
                Id = 1,
                FirstName = "Sepehr",
                LastName = "Ashtari",
                NationalId = "123456789",
                DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
            };

            _mapperMock.Setup(m => m.Map<GetPersonQuery>(request)).Returns(query); 
            _mapperMock.Setup(m => m.Map<GetPersonResponse>(personDto)).Returns(new GetPersonResponse { Person = personDto });

            // Act
            var response = await _personService.GetPerson(request, null);

            // Assert
            Assert.NotNull(response.Person);
            Assert.Equal(1, response.Person.Id);
        }
         
        [Fact]
        public async Task UpdatePerson_ShouldReturnSuccess_WhenMediatorReturnsTrue()
        {
            // Arrange
            var request = new UpdatePersonRequest
            {
                PersonDto = new PersonDto
                {
                    Id = 1,
                    FirstName = "Sepehr",
                    LastName = "Ashtari",
                    NationalId = "123456789",
                    DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
                }
            };

            var command = new UpdatePersonCommand();
            _mapperMock.Setup(m => m.Map<UpdatePersonCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(true);

            // Act
            var response = await _personService.UpdatePerson(request, null);

            // Assert
            Assert.True(response.IsSuccess);
        }
    }
}
