using DiagnosticaTuMascota.Core;
using Xunit;

namespace DiagnosticaTuMascota.Tests;

public class MotorTriajeTests
{
    [Fact]
    public void Evaluar_SinSintomas_DevuelveLeveConPuntuacionCero()
    {
        var resultado = MotorTriaje.Evaluar(System.Array.Empty<string>());

        Assert.Equal(NivelSeveridad.Leve, resultado.Severidad);
        Assert.Equal(0, resultado.Puntuacion);
        Assert.Empty(resultado.Implicaciones);
    }

    [Fact]
    public void Evaluar_ConListaNula_DevuelveLeve()
    {
        var resultado = MotorTriaje.Evaluar(null);

        Assert.Equal(NivelSeveridad.Leve, resultado.Severidad);
        Assert.Equal(0, resultado.Puntuacion);
    }

    [Theory]
    [InlineData("Estornudos")]
    [InlineData("Secreción nasal")]
    [InlineData("Pérdida de apetito")]
    public void Evaluar_ConUnSintomaLeve_DevuelveLeve(string sintoma)
    {
        var resultado = MotorTriaje.Evaluar(new[] { sintoma });

        Assert.Equal(NivelSeveridad.Leve, resultado.Severidad);
        Assert.Equal(1, resultado.Puntuacion);
        Assert.Single(resultado.Implicaciones);
    }

    [Fact]
    public void Evaluar_TresSintomasLeves_SumaMenorA4_DevuelveLeve()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Estornudos", "Secreción nasal", "Pérdida de apetito" });

        Assert.Equal(3, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Leve, resultado.Severidad);
    }

    [Theory]
    [InlineData("Fiebre")]
    [InlineData("Tos persistente")]
    [InlineData("Cambio de conducta")]
    public void Evaluar_ConUnSintomaModerado_DevuelveModerado(string sintoma)
    {
        var resultado = MotorTriaje.Evaluar(new[] { sintoma });

        Assert.Equal(NivelSeveridad.Moderado, resultado.Severidad);
        Assert.Equal(2, resultado.Puntuacion);
    }

    [Fact]
    public void Evaluar_SumaDePesosIgualA4_DevuelveModerado()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Fiebre", "Estornudos", "Secreción nasal" });

        Assert.Equal(4, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Moderado, resultado.Severidad);
    }

    [Fact]
    public void Evaluar_VomitosYDiarrea_DevuelveCriticoPorReglaEspecial()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Vómitos", "Diarrea" });

        Assert.Equal(4, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Critico, resultado.Severidad);
        Assert.Contains(resultado.Implicaciones, i => i.Contains("deshidratación"));
    }

    [Theory]
    [InlineData("Dificultad para respirar")]
    [InlineData("Temblores")]
    [InlineData("Convulsiones")]
    [InlineData("Parálisis")]
    public void Evaluar_ConSintomaCritico_DevuelveCritico(string sintoma)
    {
        var resultado = MotorTriaje.Evaluar(new[] { sintoma });

        Assert.Equal(3, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Critico, resultado.Severidad);
    }

    [Fact]
    public void Evaluar_ConCriticoYLve_EscalaACritico()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Convulsiones", "Estornudos" });

        Assert.Equal(4, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Critico, resultado.Severidad);
    }

    [Fact]
    public void Evaluar_ConSintomasDesconocidos_LosIgnora()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Síntoma inventado", "Otro síntoma" });

        Assert.Equal(NivelSeveridad.Leve, resultado.Severidad);
        Assert.Equal(0, resultado.Puntuacion);
        Assert.Empty(resultado.Implicaciones);
    }

    [Fact]
    public void Evaluar_MezclaConocidosYDesconocidos_SoloCuentaLosConocidos()
    {
        var resultado = MotorTriaje.Evaluar(new[] { "Fiebre", "No existe" });

        Assert.Equal(2, resultado.Puntuacion);
        Assert.Equal(NivelSeveridad.Moderado, resultado.Severidad);
        Assert.Single(resultado.Implicaciones);
    }

    [Theory]
    [InlineData(NivelSeveridad.Leve, "LEVE")]
    [InlineData(NivelSeveridad.Moderado, "MODERADO")]
    [InlineData(NivelSeveridad.Critico, "CRÍTICO")]
    public void Etiqueta_DevuelveTextoEnMayusculas(NivelSeveridad nivel, string esperado)
    {
        Assert.Equal(esperado, MotorTriaje.Etiqueta(nivel));
    }

    [Fact]
    public void Recomendacion_DescripcionYAcciones_CambianSegunNivel()
    {
        Assert.Contains("inmediata", MotorTriaje.Recomendacion(NivelSeveridad.Critico));
        Assert.Contains("emergencia", MotorTriaje.Descripcion(NivelSeveridad.Critico).ToLowerInvariant());
        Assert.Equal(4, MotorTriaje.Acciones(NivelSeveridad.Critico).Length);
        Assert.Equal(3, MotorTriaje.Acciones(NivelSeveridad.Moderado).Length);
        Assert.Equal(3, MotorTriaje.Acciones(NivelSeveridad.Leve).Length);
    }

    [Fact]
    public void ColorNivel_DevuelveVerdeAmbarRojo()
    {
        Assert.Equal(16, MotorTriaje.ColorNivel(NivelSeveridad.Leve).R);
        Assert.Equal(245, MotorTriaje.ColorNivel(NivelSeveridad.Moderado).R);
        Assert.Equal(239, MotorTriaje.ColorNivel(NivelSeveridad.Critico).R);
    }

    [Fact]
    public void DiccionarioY_Categorias_Contienen20Sintomas()
    {
        Assert.Equal(20, MotorTriaje.Diccionario.Count);
        Assert.Equal(4, MotorTriaje.Categorias.Length);
        Assert.Equal(20, MotorTriaje.Categorias.Sum(c => c.Sintomas.Length));
        Assert.All(MotorTriaje.Categorias.SelectMany(c => c.Sintomas),
            s => Assert.True(MotorTriaje.Diccionario.ContainsKey(s), $"Falta {s} en el diccionario"));
    }
}
