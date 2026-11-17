using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.Excecoes;

namespace RrpReservas.Dominio.Entidades;

public class Recurso
{
    public Guid Id { get; }
    public string Nome { get; }
    public TipoRecurso Tipo { get; }
    public bool EstaAtivo { get; private set; }

    public Recurso(Guid id, string nome, TipoRecurso tipo, bool estaAtivo = true)
    {
        if (id == Guid.Empty)
            throw new RegraDeDominioException("O id do recurso não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraDeDominioException("O nome do recurso não pode ser vazio.");

        Id = id;
        Nome = nome;
        Tipo = tipo;
        EstaAtivo = estaAtivo;
    }

    public void Ativar() => EstaAtivo = true;
    public void Desativar() => EstaAtivo = false;
}

//so