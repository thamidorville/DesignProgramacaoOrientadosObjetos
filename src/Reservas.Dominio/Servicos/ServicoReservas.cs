using RrpReservas.Dominio.Entidades;
using RrpReservas.Dominio.Excecoes;
using RrpReservas.Dominio.ObjetosValor;
using RrpReservas.Dominio.Politicas;

namespace RrpReservas.Dominio.Servicos;

public class ServicoReservas
{
    private readonly AgendaReservas _agenda;
    private readonly CatalogoPoliticasReserva _catalogo;

    public ServicoReservas(AgendaReservas agenda, CatalogoPoliticasReserva catalogo)
    {
        _agenda = agenda;
        _catalogo = catalogo;
    }

    public Reserva Solicitar(Usuario usuario, Recurso recurso, PeriodoReserva periodo)
    {
        var politica = _catalogo.Obter(usuario.Categoria);

        if (!politica.PodeReservar(usuario, recurso, periodo))
        {
            throw new RegraDeDominioException("A política atual não permite esta reserva.");
        }

        var reserva = new Reserva(Guid.NewGuid(), usuario, recurso, periodo);
        _agenda.Adicionar(reserva);
        return reserva;
    }

    public void Confirmar(Reserva reserva)
    {
        reserva.Confirmar();
    }
}
