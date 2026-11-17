using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.Excecoes;

namespace RrpReservas.Dominio.Entidades;

public class Usuario
{
    public Guid Id { get; }
    public string Nome { get; }
    public CategoriaUsuario Categoria { get; set; }

    public Usuario(Guid id, string nome, CategoriaUsuario categoria)
    {
        if (id == Guid.Empty)
            throw new RegraDeDominioException("O id do usuário não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraDeDominioException("O nome do usuário não pode ser vazio.");

        Id = id;
        Nome = nome;
        Categoria = categoria;
    }
}

//encapsulamento aplicado tirand o set pra impedir que o valor seja 
//mudado em qualquer parte do codigo. so o construtor atribui valor