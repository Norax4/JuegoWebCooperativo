using Coopera.Models;
using Coopera.Enums;

namespace Coopera.Tests.UnitTests.Models;

public class ValidationTests
{
    [Theory]
    [InlineData(22, 14, Dificultad.Facil)]
    [InlineData(4234, 24, Dificultad.Facil)]
    [InlineData(932, 23, Dificultad.Facil)]
    [InlineData(8812, 17, Dificultad.Facil)]
    [InlineData(7326, 10, Dificultad.Facil)]
    [InlineData(22, 34, Dificultad.Media)]
    [InlineData(4234, 44, Dificultad.Media)]
    [InlineData(932, 43, Dificultad.Media)]
    [InlineData(8812, 37, Dificultad.Media)]
    [InlineData(7326, 30, Dificultad.Media)]
    [InlineData(22, 61, Dificultad.Dificil)]
    [InlineData(4234, 84, Dificultad.Dificil)]
    [InlineData(932, 81, Dificultad.Dificil)]
    [InlineData(8812, 67, Dificultad.Dificil)]
    [InlineData(7326, 50, Dificultad.Dificil)]
    public void ChequearGeneradorDeSiempreIgualPasandoMismosSeeds(int input, int esperado, Dificultad dificultad)
    {
        //Arrange
        Partida partida = new(dificultad);
        int resultadoEsperado = esperado;

        //Act
        int resultado = partida.CalcularMeta(input);

        //Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}