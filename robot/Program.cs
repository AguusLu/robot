using TorneoRobots.Interfaces;
using TorneoRobots.Models;
using TorneoRobots.Data;

//List<Robot> robots = new List<Robot>();
RobotRepository robotRepository =
    new RobotRepository();

List<Robot> robots =
    robotRepository.ObtenerTodos();

bool continuar = true;

while (continuar)
{
    Console.WriteLine();
    Console.WriteLine("===== TORNEO DE ROBOTS =====");
    Console.WriteLine("1. Crear robot");
    Console.WriteLine("2. Listar robots");
    Console.WriteLine("3. Entrenar robot");
    Console.WriteLine("4. Descansar robot");
    Console.WriteLine("5. Realizar duelo");
    Console.WriteLine("6. Salir");

    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine() ?? "";

    Console.WriteLine();

    switch (opcion)
    {
        case "1":
            CrearRobot();
            break;

        case "2":
            ListarRobots();
            break;

        case "3":
            EntrenarRobot();
            break;

        case "4":
            DescansarRobot();
            break;

        case "5":
            RealizarDuelo();
            break;

        case "6":
            continuar = false;
            Console.WriteLine("Programa finalizado.");
            break;

        default:
            Console.WriteLine("Opción incorrecta.");
            break;
    }
}

void CrearRobot()
{
    Console.Write("Nombre del robot: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Energía inicial: ");
    int energia = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Nivel de habilidad: ");
    int habilidad = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine();
    Console.WriteLine("Tipo de robot:");
    Console.WriteLine("1. Asalto");
    Console.WriteLine("2. Defensivo");

    Console.Write("Seleccione: ");
    string tipo = Console.ReadLine() ?? "";

    Robot? nuevoRobot = null;

    if (tipo == "1")
    {
        nuevoRobot =
            new RobotAsalto(
                nombre,
                energia,
                habilidad
            );
    }
    else if (tipo == "2")
    {
        nuevoRobot =
            new RobotDefensivo(
                nombre,
                energia,
                habilidad
            );
    }

    if (nuevoRobot != null)
    {
        //robots.Add(nuevoRobot);
        robotRepository.Agregar(nuevoRobot);

        robots.Add(nuevoRobot);

        Console.WriteLine(
            $"Robot {nombre} creado correctamente."
        );
    }
    else
    {
        Console.WriteLine("Tipo de robot incorrecto.");
    }
}

void ListarRobots()
{
    if (robots.Count == 0)
    {
        Console.WriteLine(
            "No hay robots cargados."
        );

        return;
    }

    Console.WriteLine("ROBOTS");
    Console.WriteLine("-------------------------");

    for (int i = 0; i < robots.Count; i++)
    {
        Console.WriteLine(
            $"{i + 1}. {robots[i]}"
        );
    }
}

Robot? SeleccionarRobot()
{
    if (robots.Count == 0)
    {
        Console.WriteLine(
            "No hay robots cargados."
        );

        return null;
    }

    ListarRobots();

    Console.Write(
        "Seleccione el número del robot: "
    );

    int numero =
        int.Parse(Console.ReadLine() ?? "0");

    int indice = numero - 1;

    if (indice < 0 || indice >= robots.Count)
    {
        Console.WriteLine(
            "Robot inexistente."
        );

        return null;
    }

    return robots[indice];
}

void EntrenarRobot()
{
    Robot? robot = SeleccionarRobot();

    if (robot == null)
        return;

    robot.Entrenar();

    robotRepository.Actualizar(robot);
}

void DescansarRobot()
{
    Robot? robot = SeleccionarRobot();

    if (robot == null)
        return;

    robot.Descansar();

    robotRepository.Actualizar(robot);
}

void RealizarDuelo()
{
    if (robots.Count < 2)
    {
        Console.WriteLine(
            "Debe haber al menos dos robots."
        );

        return;
    }

    Console.WriteLine(
        "Seleccione el primer robot:"
    );

    Robot? robot1 = SeleccionarRobot();

    if (robot1 == null)
        return;

    Console.WriteLine();

    Console.WriteLine(
        "Seleccione el oponente:"
    );

    Robot? robot2 = SeleccionarRobot();

    if (robot2 == null)
        return;

    if (robot1 == robot2)
    {
        Console.WriteLine(
            "Un robot no puede pelear contra sí mismo."
        );

        return;
    }

    if (robot1 is IDuelo duelista)
    {
        Console.WriteLine();
        Console.WriteLine("===== DUELO =====");

        Robot ganador =
            duelista.RealizarDuelo(robot2);

        Console.WriteLine();
        Console.WriteLine(
            $"GANADOR: {ganador.Nombre}"
        );
    }
}