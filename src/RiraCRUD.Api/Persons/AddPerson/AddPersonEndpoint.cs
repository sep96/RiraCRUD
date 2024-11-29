using Carter;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Persons;

namespace RiraCRUD.Api.Persons.AddPerson
{
    public record AddPersonRequest(AddPersonDto AddPersonDto);
    public record AddPersonResponse(bool IsSuccess);

    public class AddPersonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Person/Add", async (AddPersonRequest request, IMediator mediator) =>
            { 

                var result = await mediator.Send(request); 

                return Results.Ok(result);
            })
            .WithName("AddPersonDto")
            .Produces<AddPersonResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Person")
            .WithDescription("Add Person");
        }
    }

}
