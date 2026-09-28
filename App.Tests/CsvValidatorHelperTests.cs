using App.Helpers;
using App.Models;

namespace App.Tests
{
    public class CsvValidatorHelperTests
    {
        [Fact]
        public void Validate_ShouldReturnValid_WhenRowIsCorrect()
        {
            // Arrange
            var row = new ClockingCsv
            {
                Rut = "12345678-5",
                FechaHora = "2026-09-16 08:00:00",
                Type = "E",
                Origen = "RELOJ"
            };

            // Act
            var result = CsvValidatorHelper.Validate(row);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Validate_ShouldReturnInvalid_WhenRutIsInvalid()
        {
            // Arrange
            var row = new ClockingCsv
            {
                Rut = "12345678-0",
                FechaHora = "2026-09-16 08:00:00",
                Type = "E",
                Origen = "RELOJ"
            };

            // Act
            var result = CsvValidatorHelper.Validate(row);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.Contains("RUT"));
        }
    }
}