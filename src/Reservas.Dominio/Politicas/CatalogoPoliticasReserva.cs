using RrpReservas.Dominio.Enums;
using RrpReservas.Dominio.Excecoes;

namespace RrpReservas.Dominio.Politicas;

public class CatalogoPoliticasReserva
{
    private readonly Dictionary<CategoriaUsuario, IPoliticaReserva> _politicas = new();

    public CatalogoPoliticasReserva(IEnumerable<IPoliticaReserva> politicas)
    {
        foreach (var politica in politicas)
        {
            Registrar(politica);
        }
    }

    public void Registrar(IPoliticaReserva politica)
    {
        _politicas[politica.Categoria] = politica;
    }

    public IPoliticaReserva Obter(CategoriaUsuario categoria)
    {
        if (_politicas.TryGetValue(categoria, out var politica))
        {
            return politica;
        }

        throw new PoliticaReservaNaoEncontradaException($"Nenhuma política cadastrada para {categoria}.");
    }
}
