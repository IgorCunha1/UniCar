using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Repositories.Interfaces
{
    public interface IPessoaRepository
    {
        List<Pessoa> ListarPessoas();
        Task<Pessoa> RecuperarPessoa(int id);
        Task DeletarPessoa(int id);
        Task EditarPessoa(int id, Pessoa pessoa);
        Task AdicionarPessoa(Pessoa pessoa);


    }
}
