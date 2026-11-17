using RrpReservas.Dominio.Excecoes;

namespace RrpReservas.Dominio.ObjetosValor;

public class PeriodoReserva
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }
    public TimeSpan Duracao => Fim - Inicio;

    public PeriodoReserva(DateTime inicio, DateTime fim)
    {
        if (inicio >= fim)
            throw new RegraDeDominioException("O início do período deve ser anterior ao fim.");

        Inicio = inicio;
        Fim = fim;
    }

    public bool ConflitaCom(PeriodoReserva outro)
    {
        return Inicio < outro.Fim && outro.Inicio < Fim;
    }
}
