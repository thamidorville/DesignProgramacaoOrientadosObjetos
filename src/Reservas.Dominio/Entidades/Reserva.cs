using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.Excecoes;
using RrpReservas.Dominio.ObjetosValor;

namespace RrpReservas.Dominio.Entidades;

public class Reserva
{
    public Guid Id { get; }
    public Usuario Usuario { get; }
    public Recurso Recurso { get; }
    public PeriodoReserva Periodo { get; }
    public StatusReserva Status { get; private set; }

    public bool BloqueiaAgenda =>
        Status == StatusReserva.Solicitada || Status == StatusReserva.Confirmada;

    public Reserva(Guid id, Usuario usuario, Recurso recurso, PeriodoReserva periodo)
    {
        if (id == Guid.Empty)
            throw new RegraDeDominioException("O id da reserva não pode ser vazio.");

        Usuario = usuario ?? throw new RegraDeDominioException("A reserva exige um usuário.");
        Recurso = recurso ?? throw new RegraDeDominioException("A reserva exige um recurso.");
        Periodo = periodo ?? throw new RegraDeDominioException("A reserva exige um período.");

        Id = id;
        Status = StatusReserva.Solicitada;
    }

    public void Confirmar()
    {
        if (Status != StatusReserva.Solicitada)
            throw new TransicaoDeReservaInvalidaException(
                $"Não é possível confirmar uma reserva no estado {Status}.");

        Status = StatusReserva.Confirmada;
    }

    public void Cancelar()
    {
        if (Status != StatusReserva.Solicitada && Status != StatusReserva.Confirmada)
            throw new TransicaoDeReservaInvalidaException(
                $"Não é possível cancelar uma reserva no estado {Status}.");

        Status = StatusReserva.Cancelada;
    }

    public void Concluir()
    {
        if (Status != StatusReserva.Confirmada)
            throw new TransicaoDeReservaInvalidaException(
                $"Não é possível concluir uma reserva no estado {Status}.");

        Status = StatusReserva.Concluida;
    }
}
