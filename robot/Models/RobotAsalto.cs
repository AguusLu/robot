using TorneoRobots.Interfaces;

namespace TorneoRobots.Models;

public class RobotAsalto : Robot, IDuelo
{
    public RobotAsalto(
        string nombre,
        int energia,
        int nivelHabilidad,
        int id = 0)
        : base(nombre, energia, nivelHabilidad, id)
    {
    }

    public override int CalcularPuntajeDuelo()
    {
        return NivelHabilidad + 10;
    }

    public Robot RealizarDuelo(Robot oponente)
    {
        int puntajePropio = CalcularPuntajeDuelo();
        int puntajeOponente = oponente.CalcularPuntajeDuelo();

        Energia -= 15;

        if (Energia < 0)
            Energia = 0;

        Console.WriteLine(
            $"{Nombre}: {puntajePropio} puntos"
        );

        Console.WriteLine(
            $"{oponente.Nombre}: {puntajeOponente} puntos"
        );

        if (puntajePropio >= puntajeOponente)
            return this;

        return oponente;
    }
}