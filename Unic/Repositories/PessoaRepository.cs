using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Models;
using Unic.Repositories.Interfaces;

namespace Unic.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly UnicContext _context;

        public PessoaRepository(UnicContext context)
        {
            _context = context;
        }

        public async Task AdicionarPessoa(Pessoa pessoa)
        {
            await _context.Pessoa.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public List<Pessoa> ListarPessoas()
        {
            var pessoas = _context.Pessoa.OrderBy(p => p.NomeCompleto).ToList();
            
            return pessoas;                     
        }
        public Task<Pessoa> RecuperarPessoa(int id)
        {
            throw new NotImplementedException();
        }
        public Task EditarPessoa(int id, Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        public Task DeletarPessoa(int id)
        {
            throw new NotImplementedException();
        }


    }
}
