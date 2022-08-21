using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Repositories.Interfaces
{
    public interface ICarroRepository
    {

        List<Carro> ListarCarros();
        Carro RecuperarCarro(int id);
        Carro RecuperarCarroPorPlaca(string id);
        Task AdicionarCarro(Carro carro);
        bool ValidarPlaca(string placa);
        List<Marca> GetApiMarcas();
        Task GetApiModelos();
        Task GetApiAnos();

        List<Marca> verificarMarcas();
        List<Modelo> verificarModelosMarcas(int MarcaId);
        List<Anos> verificarAnosModelo(int Modeloid);
    }
}
