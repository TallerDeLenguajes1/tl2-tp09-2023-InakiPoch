using Microsoft.AspNetCore.Mvc;
using tl2_tp09_2023_InakiPoch.Models;
using tl2_tp09_2023_InakiPoch.Repositories;

namespace tl2_tp09_2023_InakiPoch.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController : ControllerBase {
    BoardRepository boardRepository;
    private readonly ILogger<BoardController> _logger;

    public BoardController(ILogger<BoardController> logger) {
        boardRepository = new BoardRepository();
        _logger = logger;
    }

    [HttpGet("GetAll")]
    public ActionResult<List<Board>> GetAll() => Ok(boardRepository.GetAll());

    [HttpGet("GetById")]
    public ActionResult<Board> GetById(int id) => Ok(boardRepository.GetById(id));

    [HttpGet("GetByUser")]
    public ActionResult<Board> GetByUser(int userId) => Ok(boardRepository.GetByUser(userId));

    [HttpPost("Add")]
    public ActionResult Add(Board board) {
        boardRepository.Add(board);
        return Ok("Tablero agregado");
    }

    [HttpPut("Update")]
    public ActionResult Update(int id, Board Board) {
        boardRepository.Update(id, Board);
        return Ok("Tablero actualizado con exito");
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(int id) {
        boardRepository.Delete(id);
        return Ok("Tablero borrado con exito");
    }
}
