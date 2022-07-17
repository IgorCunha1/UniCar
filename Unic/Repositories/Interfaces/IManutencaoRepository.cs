using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Repositories.Interfaces
{
    public interface IManutencaoRepository
    {
        List<Manutencao> ListarManutencoes(int carroId);
        Task<Manutencao> RecuperarManutencao(int id);
        Task DeletarManutencao(int id);
        Task EditarManutencao(int id, Manutencao manutencao);
        Task AdicionarManutencao(Manutencao manutencao);


    }
}
