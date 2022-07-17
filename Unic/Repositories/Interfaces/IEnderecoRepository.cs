using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {

        List<Endereco> ListarEnderecos();
        Task<Endereco> RecuperarEndereco(int id);
        Task DeletarEndereco(int id);
        Task EditarEndereco(int id, Endereco Endereco);
        Task AdicionarEndereco(Endereco Endereco);
    }
}
