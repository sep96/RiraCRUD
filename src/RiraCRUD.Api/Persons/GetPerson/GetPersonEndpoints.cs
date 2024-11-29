using Carter;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Base;
using RiraCRUD.Application.Common.DTOs.Persons;

namespace RiraCRUD.Api.Persons.GetPerson
{
    public record GetPeronsRequest(GridFilterDto GridFilterDto);
    public record GetPeronsResponse(PersonDto IsSuccess);

    public class GetPersonEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Person/Get", async (GetPeronsRequest request, IMediator mediator) =>
            {

                var result = await mediator.Send(request);

                return result is null ? Results.NotFound() :  Results.Ok(result);
            })
            .WithName("GetPeron")
            .Produces<GetPeronsRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Person")
            .WithDescription("Get Person");
        }
    } 
}
