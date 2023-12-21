using Controle_de_contato.Models;

namespace Controle_de_contato.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);
    }
}
