using RrpReservas.Dominio.Entidades;
using RrpReservas.Dominio.Excecoes;
using RrpReservas.Dominio.ObjetosValor;

namespace RrpReservas.Dominio.Servicos;

public class AgendaReservas
{
    private readonly List<Reserva> _reservas = new();

    public IReadOnlyList<Reserva> Reservas => _reservas.AsReadOnly();

    public bool TemConflito(Recurso recurso, PeriodoReserva periodo, Guid? ignorarReservaId = null)
    {
        return _reservas.Any(r =>
            r.Recurso.Id == recurso.Id &&
            r.Id != ignorarReservaId &&
            r.BloqueiaAgenda &&
            r.Periodo.ConflitaCom(periodo));
    }

    public void Adicionar(Reserva reserva)
    {
        if (!reserva.Recurso.EstaAtivo)
            throw new RegraDeDominioException("Um recurso inativo não pode receber reservas.");

        if (TemConflito(reserva.Recurso, reserva.Periodo, reserva.Id))
            throw new ConflitoDeReservaException("Já existe uma reserva em período conflitante para este recurso.");

        _reservas.Add(reserva);
    }
}
