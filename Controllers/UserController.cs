using Microsoft.AspNetCore.Mvc;
using tl2_tp09_2023_InakiPoch.Models;
using tl2_tp09_2023_InakiPoch.Repositories;

namespace tl2_tp09_2023_InakiPoch.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    UserRepository userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger) {
        userRepository = new UserRepository();
        _logger = logger;
    }

    [HttpGet("GetAll")]
    public ActionResult<List<User>> GetAll() => Ok(userRepository.GetAll());

    [HttpGet("GetById")]
    public ActionResult<User> GetById(int id) => Ok(userRepository.GetById(id));

    [HttpPost("Add")]
    public ActionResult Add(User user) {
        userRepository.Add(user);
        return Ok("Usuario agregado");
    }

    [HttpPut("Update")]
    public ActionResult Update(int id, User user) {
        userRepository.Update(id, user);
        return Ok();
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(int id) {
        userRepository.Delete(id);
        return Ok();
    }
}
