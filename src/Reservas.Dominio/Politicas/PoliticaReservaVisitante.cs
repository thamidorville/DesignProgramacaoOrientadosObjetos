using RrpReservas.Dominio.Entidades;
using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.ObjetosValor;

namespace RrpReservas.Dominio.Politicas;

public sealed class PoliticaReservaVisitante : IPoliticaReserva
{
    public CategoriaUsuario Categoria => CategoriaUsuario.Visitante;

    public bool PodeReservar(Usuario usuario, Recurso recurso, PeriodoReserva periodo)
    {
        return periodo.Duracao <= TimeSpan.FromHours(1)
            && (recurso.Tipo == TipoRecurso.Sala || recurso.Tipo == TipoRecurso.Projetor)
            && recurso.EstaAtivo;
    }
}
