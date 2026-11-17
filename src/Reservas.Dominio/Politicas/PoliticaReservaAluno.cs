using RrpReservas.Dominio.Entidades;
using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.ObjetosValor;

namespace RrpReservas.Dominio.Politicas;

public sealed class PoliticaReservaAluno : IPoliticaReserva
{
    public CategoriaUsuario Categoria => CategoriaUsuario.Aluno;

    public bool PodeReservar(Usuario usuario, Recurso recurso, PeriodoReserva periodo)
    {
        return periodo.Duracao <= TimeSpan.FromHours(2);
    }
}
