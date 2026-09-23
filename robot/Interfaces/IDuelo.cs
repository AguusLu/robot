using TorneoRobots.Models;

namespace TorneoRobots.Interfaces;

public interface IDuelo
{
    Robot RealizarDuelo(Robot oponente);
}