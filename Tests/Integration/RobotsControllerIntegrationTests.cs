using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotApi.Controllers;
using RobotApi.Data;
using RobotApi.Models;

namespace Tests.Integration
{
    [TestClass]
    public class RobotsControllerIntegrationTests
    {
        private SqliteConnection _connection = null!;
        private TorneoRobotsContext _context = null!;

        [TestInitialize]
        public void Preparar()
        {
            _connection =
                new SqliteConnection(
                    "DataSource=:memory:"
                );

            _connection.Open();

            var options =
                new DbContextOptionsBuilder<TorneoRobotsContext>()
                    .UseSqlite(_connection)
                    .Options;

            _context =
                new TorneoRobotsContext(options);

            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Limpiar()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [TestMethod]
        public async Task ObtenerTodos_ExisteUnRobot_LoDevuelve()
        {
            // Arrange
            RobotEntity robot =
                new RobotEntity
                {
                    Nombre = "Optimus Test",
                    Energia = 70,
                    NivelHabilidad = 53,
                    Tipo = "Asalto"
                };

            _context.Robots.Add(robot);

            await _context.SaveChangesAsync();

            RobotsController controller =
                new RobotsController(_context);

            // Act
            ActionResult<List<RobotEntity>> respuesta =
                await controller.ObtenerTodos();

            // Assert
            OkObjectResult? ok =
                respuesta.Result as OkObjectResult;

            Assert.IsNotNull(ok);

            List<RobotEntity>? robots =
                ok.Value as List<RobotEntity>;

            Assert.IsNotNull(robots);

            Assert.AreEqual(1, robots.Count);

            Assert.AreEqual(
                "Optimus Test",
                robots[0].Nombre
            );
        }
    }
}
