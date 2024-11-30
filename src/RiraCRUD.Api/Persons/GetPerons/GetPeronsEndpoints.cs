using Carter;
using MediatR;
using RiraCRUD.Application.Common.DTOs.Base;
using RiraCRUD.Application.Common.DTOs.Persons;
using RiraCRUD.Application.Person.Queries;

namespace RiraCRUD.Api.Persons.GetPerons
{
    public record GetPeronsRequest(GetPeronsQuery GridFilterDto);
    public record GetPeronsResponse(PageListDto<PersonDto> IsSuccess);

    public class GetPeronsEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Person/GetList", async (GetPeronsRequest request, IMediator mediator) =>
            {

                var result = await mediator.Send(request);

                return Results.Ok(result);
            })
            .WithName("GetPerons")
            .Produces<GetPeronsRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Persons")
            .WithDescription("Get Persons");
        }
    }
}
