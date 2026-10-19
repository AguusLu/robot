using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobotApi.Data;
using RobotApi.Models;

namespace RobotApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RobotsController : ControllerBase
{
    private readonly TorneoRobotsContext _context;

    public RobotsController(TorneoRobotsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<RobotEntity>>> ObtenerTodos()
    {
        List<RobotEntity> robots =
            await _context.Robots.ToListAsync();

        return Ok(robots);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RobotEntity>> ObtenerPorId(int id)
    {
        RobotEntity? robot =
            await _context.Robots.FindAsync(id);

        if (robot == null)
        {
            return NotFound();
        }

        return Ok(robot);
    }

    [HttpPost]
    public async Task<ActionResult<RobotEntity>> Crear(
    RobotEntity robot)
    {
        robot.Id = 0;

        _context.Robots.Add(robot);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = robot.Id },
            robot
        );
    }
}