using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Features.Tasks.Commands.CreateTask;
using TaskFlow.Application.Features.Tasks.Commands.DeleteTask;
using TaskFlow.Application.Features.Tasks.Commands.UpdateTask;
using TaskFlow.Application.Features.Tasks.Queries.GetAllTasks;
using TaskFlow.Application.Features.Tasks.Queries.GetTaskDetail;

namespace TaskFlow.API.Controllers;

/// <summary>
/// REST API controller for managing task items.
/// Notice how thin this controller is — it just receives HTTP requests
/// and delegates to MediatR commands/queries. All business logic
/// lives in the Application layer handlers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all tasks.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TaskItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TaskItemDto>>> GetAll()
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery());
        return Ok(tasks);
    }

    /// <summary>
    /// Get a single task by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskItemDto>> GetById(int id)
    {
        var task = await _mediator.Send(new GetTaskDetailQuery { Id = id });
        return Ok(task);
    }

    /// <summary>
    /// Create a new task.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskDto dto)
    {
        var command = new CreateTaskCommand { TaskDto = dto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update an existing task.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskItemDto>> Update(int id, [FromBody] UpdateTaskDto dto)
    {
        dto.Id = id;
        var command = new UpdateTaskCommand { TaskDto = dto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete a task.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteTaskCommand { Id = id });
        return NoContent();
    }
}
