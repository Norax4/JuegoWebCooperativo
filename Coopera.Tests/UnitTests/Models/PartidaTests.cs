using Coopera.Models;
using System.Threading.Tasks;

namespace Coopera.Tests.UnitTests.Models;

public class PartidaTests
{
    [Theory]
    [InlineData(22, 6, Dificultad.Facil)]
    [InlineData(4234, 9, Dificultad.Facil)]
    [InlineData(932, 8, Dificultad.Facil)]
    [InlineData(8812, 7, Dificultad.Facil)]
    [InlineData(7326, 5, Dificultad.Facil)]
    [InlineData(22, 11, Dificultad.Media)]
    [InlineData(4234, 14, Dificultad.Media)]
    [InlineData(932, 13, Dificultad.Media)]
    [InlineData(8812, 12, Dificultad.Media)]
    [InlineData(22, 17, Dificultad.Dificil)]
    [InlineData(4234, 22, Dificultad.Dificil)]
    [InlineData(932, 21, Dificultad.Dificil)]
    [InlineData(8812, 18, Dificultad.Dificil)]
    [InlineData(7326, 15, Dificultad.Dificil)]
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

    [Fact]
    public async Task ChequearDuracionPartida()
    {
        Partida partida = new(Dificultad.Facil);

        Double duracionEsperada = 2;
        Double tolerancia = 0.1;

        await Task.Delay(2000);
        partida.Finalizar();

        Double duracionPartida = partida.Duracion;

        Assert.Equal(duracionEsperada, duracionPartida, tolerancia);
    }

    [Fact]
    public void ChequearExceptionSiNoIngresaDificultad()
    {
        Dificultad valorInvalido = (Dificultad)3;

        string mensajeException = "Debe ingresar una dificultad valida.";
        string param = "dificultad";

        var exceptionNombre = Assert.Throws<ArgumentException>(() => new Partida(valorInvalido));

        Assert.StartsWith(mensajeException, exceptionNombre.Message);
        Assert.Equal(param, exceptionNombre.ParamName);
    }

    [Theory]
    [InlineData(10, 20, 15, 20)]
    [InlineData(30, 45, 50, 50)]
    [InlineData(100, 100, 15, 100)]
    [InlineData(20, 20, 20, 20)]
    [InlineData(70, 68, 30, 66)]
    public void ChequearRecurso(int madera, int piedra, int comida, int meta)
    {
        Partida partida = new(Dificultad.Facil);
        partida.Madera = madera;
        partida.Piedra = piedra;
        partida.Comida = comida;
        partida.Meta = meta;

        bool resultadoEsperadoMadera = madera >= meta;
        bool resultadoEsperadoPiedra = piedra >= meta;
        bool resultadoEsperadoComida = comida >= meta;

        bool resultadoMadera = partida.ChequearMetaRecurso(partida.Madera);
        bool resultadoPiedra = partida.ChequearMetaRecurso(partida.Piedra);
        bool resultadoComida = partida.ChequearMetaRecurso(partida.Comida);

        Assert.Equal(resultadoEsperadoMadera, resultadoMadera);
        Assert.Equal(resultadoEsperadoPiedra, resultadoPiedra);
        Assert.Equal(resultadoEsperadoComida, resultadoComida);
    }

    [Theory]
    [InlineData(10, 20, 15, 20, false)]
    [InlineData(30, 45, 50, 50, false)]
    [InlineData(100, 100, 100, 100, true)]
    [InlineData(20, 20, 20, 20, true)]
    [InlineData(70, 68, 30, 66, false)]
    public void ChequearPartidaFinalizada(int madera, int piedra, int comida, int meta, bool resultado)
    {
        Partida partida = new(Dificultad.Facil);
        partida.Madera = madera;
        partida.Piedra = piedra;
        partida.Comida = comida;
        partida.Meta = meta;

        bool resultadoEsperado = resultado;
        bool estadoPartidaFinalizada = partida.PartidaFinalizada;

        Assert.Equal(resultadoEsperado, estadoPartidaFinalizada);
    }
}
