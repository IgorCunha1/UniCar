using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;
using Unic.Repositories.Interfaces;

namespace Unic.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public Task AdicionarEndereco(Endereco Endereco)
        {
            throw new NotImplementedException();
        }

        public Task DeletarEndereco(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditarEndereco(int id, Endereco Endereco)
        {
            throw new NotImplementedException();
        }

        public List<Endereco> ListarEnderecos()
        {
            throw new NotImplementedException();
        }

        public Task<Endereco> RecuperarEndereco(int id)
        {
            throw new NotImplementedException();
        }
    }
}
