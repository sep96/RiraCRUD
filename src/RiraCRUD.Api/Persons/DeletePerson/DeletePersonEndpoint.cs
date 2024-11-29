using Carter;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Persons;

namespace RiraCRUD.Api.Persons.DeletePerson
{
    public record DeletePersonRequest(DeletePersonDto AddPersonDto);
    public record DeletePersonResponse(bool IsSuccess);

    public class DeletePersonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Person/Delete", async (DeletePersonRequest request, IMediator mediator) =>
            {

                var result = await mediator.Send(request);

                return Results.Ok(result);
            })
            .WithName("DeletePerson")
            .Produces<DeletePersonResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Person")
            .WithDescription("Delete Person");
        }
    }
}
