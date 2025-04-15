using TodoLists.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using TodoLists.Application.UseCases.GetItems;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.UpdateItem;
using TodoLists.Application.UseCases.RemoveItem;
using TodoLists.Application.UseCases.RegisterProgression;

namespace TodoLists.Web.Endpoints;

public class TodoItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetItemsWithPagination)
            .MapPost(AddItem)
            .MapPost(RegisterProgression, "{id}/RegisterProgression")
            .MapPut(UpdateItem, "{id}")
            .MapDelete(DeleteItem, "{id}");
    }

    public async Task<Ok<PaginatedList<TodoItemDto>>> GetItemsWithPagination(ISender sender, [AsParameters] GetItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> AddItem(ISender sender, AddItemCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(TodoItems)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateItem(ISender sender, int id, UpdateItemCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, BadRequest>> RegisterProgression(ISender sender, int id, RegisterProgressionCommand command)
    {
        if (id != command.TodoItemId) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteItem(ISender sender, int id)
    {
        await sender.Send(new RemoveItemCommand() { Id = id });

        return TypedResults.NoContent();
    }
}
