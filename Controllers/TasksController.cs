using Microsoft.AspNetCore.Mvc;
using tl2_tp09_2023_InakiPoch.Models;
using tl2_tp09_2023_InakiPoch.Repositories;

namespace tl2_tp09_2023_InakiPoch.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase {
    TasksRepository tasksRepository;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger) {
        tasksRepository = new TasksRepository();
        _logger = logger;
    }

    [HttpGet("GetById")]
    public ActionResult<Tasks> GetById(int id) => Ok(tasksRepository.GetById(id));

    [HttpGet("GetByUser")]
    public ActionResult<List<Tasks>> GetByUser(int userId) => Ok(tasksRepository.GetByUser(userId));

    [HttpGet("GetByBoard")]
    public ActionResult<List<Tasks>> GetByBoard(int boardId) => Ok(tasksRepository.GetByBoard(boardId));

    [HttpPost("Add")]
    public ActionResult Add(int boardId, Tasks tasks) {
        tasksRepository.Add(boardId, tasks);
        return Ok("Tarea agregada");
    }

    [HttpPut("AssignTask")]
    public ActionResult AssignTask(int userId, int taskId) {
        tasksRepository.AssignTask(userId, taskId);
        return Ok("Tarea asignada");
    }

    [HttpPut("Update")]
    public ActionResult Update(int id, Tasks task) {
        tasksRepository.Update(id, task);
        return Ok("Tarea actualizada con exito");
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(int id) {
        tasksRepository.Delete(id);
        return Ok("Tarea borrada con exito");
    }
}
