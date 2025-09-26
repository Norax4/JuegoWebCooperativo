using Coopera.Models;

namespace Coopera.Tests.UnitTests.Models;

public class JugadorTests
{
    [Fact]
    public void ChequearExceptionConNombreVacioJugador()
    {
      string mensajeException = "No puede dejar el nombre vacío";
      string param = "nombre";

      var exceptionNombre = Assert.Throws<ArgumentException>(() => new Jugador(""));

      Assert.StartsWith(mensajeException, exceptionNombre.Message);
      Assert.Equal(param, exceptionNombre.ParamName);
    }
}
