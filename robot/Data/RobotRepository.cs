using Microsoft.Data.SqlClient;
using TorneoRobots.Models;

namespace TorneoRobots.Data;

public class RobotRepository
{
    public List<Robot> ObtenerTodos()
    {
        List<Robot> robots = new List<Robot>();

        using SqlConnection conexion =
            new SqlConnection(ConexionBD.CadenaConexion);

        string consulta =
            @"SELECT Id,
                     Nombre,
                     Energia,
                     NivelHabilidad,
                     Tipo
              FROM Robots";

        using SqlCommand comando =
            new SqlCommand(consulta, conexion);

        conexion.Open();

        using SqlDataReader reader =
            comando.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string nombre = reader.GetString(1);
            int energia = reader.GetInt32(2);
            int habilidad = reader.GetInt32(3);
            string tipo = reader.GetString(4);

            Robot? robot = null;

            if (tipo == "Asalto")
            {
                robot = new RobotAsalto(
                    nombre,
                    energia,
                    habilidad,
                    id
                );
            }
            else if (tipo == "Defensivo")
            {
                robot = new RobotDefensivo(
                    nombre,
                    energia,
                    habilidad,
                    id
                );
            }

            if (robot != null)
                robots.Add(robot);
        }

        return robots;
    }
    public void Agregar(Robot robot)
    {
        using SqlConnection conexion =
            new SqlConnection(ConexionBD.CadenaConexion);

        string tipo;

        if (robot is RobotAsalto)
            tipo = "Asalto";
        else
            tipo = "Defensivo";

        string consulta =
            @"INSERT INTO Robots
            (Nombre, Energia, NivelHabilidad, Tipo)
          OUTPUT INSERTED.Id
          VALUES
            (@Nombre, @Energia, @NivelHabilidad, @Tipo)";

        using SqlCommand comando =
            new SqlCommand(consulta, conexion);

        comando.Parameters.AddWithValue(
            "@Nombre",
            robot.Nombre
        );

        comando.Parameters.AddWithValue(
            "@Energia",
            robot.Energia
        );

        comando.Parameters.AddWithValue(
            "@NivelHabilidad",
            robot.NivelHabilidad
        );

        comando.Parameters.AddWithValue(
            "@Tipo",
            tipo
        );

        conexion.Open();

        int idGenerado =
            (int)comando.ExecuteScalar()!;

        robot.Id = idGenerado;
    }

    public void Actualizar(Robot robot)
    {
        using SqlConnection conexion =
            new SqlConnection(ConexionBD.CadenaConexion);

        string consulta =
            @"UPDATE Robots
          SET Nombre = @Nombre,
              Energia = @Energia,
              NivelHabilidad = @NivelHabilidad
          WHERE Id = @Id";

        using SqlCommand comando =
            new SqlCommand(consulta, conexion);

        comando.Parameters.AddWithValue(
            "@Nombre",
            robot.Nombre
        );

        comando.Parameters.AddWithValue(
            "@Energia",
            robot.Energia
        );

        comando.Parameters.AddWithValue(
            "@NivelHabilidad",
            robot.NivelHabilidad
        );

        comando.Parameters.AddWithValue(
            "@Id",
            robot.Id
        );

        conexion.Open();

        comando.ExecuteNonQuery();
    }
}