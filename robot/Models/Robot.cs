namespace TorneoRobots.Models;

public abstract class Robot
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public int Energia { get; set; }

    public int NivelHabilidad { get; set; }

    protected Robot(
        string nombre,
        int energia,
        int nivelHabilidad,
        int id = 0)
    {
        Id = id;
        Nombre = nombre;
        Energia = energia;
        NivelHabilidad = nivelHabilidad;
    }

    public void Entrenar()
    {
        int mejora = Random.Shared.Next(3, 8);

        NivelHabilidad += mejora;
        Energia -= 10;

        if (Energia < 0)
            Energia = 0;

        Console.WriteLine(
            $"{Nombre} entrenó y mejoró {mejora} puntos."
        );

        Console.WriteLine(
            $"Habilidad: {NivelHabilidad} - Energía: {Energia}"
        );
    }

    public void Descansar()
    {
        Energia += 20;

        if (Energia > 100)
            Energia = 100;

        Console.WriteLine(
            $"{Nombre} descansó. Energía: {Energia}"
        );
    }

    public abstract int CalcularPuntajeDuelo();

    public override string ToString()
    {
        return
            $"ID: {Id} | {Nombre} | " +
            $"Energía: {Energia} | " +
            $"Habilidad: {NivelHabilidad}";
    }
}