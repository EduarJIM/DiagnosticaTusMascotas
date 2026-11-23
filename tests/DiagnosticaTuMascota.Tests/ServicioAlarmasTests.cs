using DiagnosticaTuMascota.Core;
using Xunit;

namespace DiagnosticaTuMascota.Tests;

public class ServicioAlarmasTests
{
    private static Recordatorio Crear(long id, DateTime fechaHora) => new()
    {
        Id = id,
        Titulo = "Vacuna",
        Mascota = "Luna",
        Mensaje = "Recordatorio de prueba",
        Prioridad = "Media",
        FechaHora = fechaHora
    };

    [Fact]
    public void DebeSonar_ConElMismoMinuto_DevuelveTrue()
    {
        var recordatorio = Crear(1, new DateTime(2026, 11, 23, 9, 30, 0));
        var ahora = new DateTime(2026, 11, 23, 9, 30, 47);

        Assert.True(ServicioAlarmas.DebeSonar(recordatorio, ahora));
    }

    [Theory]
    [InlineData(2026, 11, 23, 9, 31)]
    [InlineData(2026, 11, 24, 9, 30)]
    [InlineData(2026, 12, 23, 9, 30)]
    public void DebeSonar_ConOtroMinutoDiaOMes_DevuelveFalse(int anio, int mes, int dia, int hora, int minuto)
    {
        var recordatorio = Crear(1, new DateTime(2026, 11, 23, 9, 30, 0));
        var ahora = new DateTime(anio, mes, dia, hora, minuto, 0);

        Assert.False(ServicioAlarmas.DebeSonar(recordatorio, ahora));
    }

    [Fact]
    public void DebeSonar_ConRecordatorioNulo_DevuelveFalse()
    {
        Assert.False(ServicioAlarmas.DebeSonar(null, DateTime.Now));
    }

    [Fact]
    public void Pendientes_FiltraPorMinutoYExcluyeYaAvisados()
    {
        var ahora = new DateTime(2026, 11, 23, 9, 30, 10);
        var alarmas = new[]
        {
            Crear(1, new DateTime(2026, 11, 23, 9, 30, 0)),
            Crear(2, new DateTime(2026, 11, 23, 9, 30, 0)),
            Crear(3, new DateTime(2026, 11, 23, 10, 0, 0))
        };

        var pendientes = ServicioAlarmas.Pendientes(alarmas, ahora, new HashSet<long> { 1 });

        Assert.Single(pendientes);
        Assert.Equal(2, pendientes[0].Id);
    }
}
