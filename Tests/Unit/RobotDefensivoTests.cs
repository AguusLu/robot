using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TorneoRobots.Models;

namespace Tests.Unit
{
    [TestClass]
    public class RobotDefensivoTests
    {
        [TestMethod]
        public void CalcularPuntajeDuelo_Energia80Habilidad50_Devuelve58()
        {
            // Arrange
            RobotDefensivo robot =
                new RobotDefensivo(
                    "Wall-E",
                    80,
                    50
                );

            // Act
            int resultado =
                robot.CalcularPuntajeDuelo();

            // Assert
            Assert.AreEqual(58, resultado);
        }

        [TestMethod]
        public void Descansar_Energia90_NoSupera100()
        {
            // Arrange
            RobotDefensivo robot =
                new RobotDefensivo(
                    "Wall-E",
                    90,
                    50
                );

            // Act
            robot.Descansar();

            // Assert
            Assert.AreEqual(100, robot.Energia);
        }
    }
}