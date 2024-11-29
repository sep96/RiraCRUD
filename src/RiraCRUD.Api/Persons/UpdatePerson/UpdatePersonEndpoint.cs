using Carter;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Persons;

namespace RiraCRUD.Api.Persons.UpdatePerson
{
    public record UpdatePersonRequest(PersonDto AddPersonDto);
    public record UpdatePersonResponse(bool IsSuccess);

    public class UpdatePersonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Person/Update", async (UpdatePersonRequest request, IMediator mediator) =>
            {

                var result = await mediator.Send(request);

                return Results.Ok(result);
            })
            .WithName("UpdatePerson")
            .Produces<UpdatePersonResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Person")
            .WithDescription("Update Person");
        }
    }
}
