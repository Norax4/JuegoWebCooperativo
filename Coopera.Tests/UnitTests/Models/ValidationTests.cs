namespace Coopera.Tests.UnitTests.Models;

public class ValidationTests
{
    [Theory]
    [InlineData(22, 22)]
    public void ChequearGeneradorPorSeeds(int input, int esperado)
    {
        //Arrange
        Partida partida = new(1);
        int resultadoEsperado = esperado;

        //Act
        int resultado = partida.CalcularMeta(input);

        //Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}