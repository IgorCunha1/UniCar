using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;
using Unic.Repositories.Interfaces;
using Unic.Data;
using Microsoft.AspNetCore.Mvc;

namespace Unic.Repositories
{
    public class ManutencaoRepository : IManutencaoRepository
    {
        private readonly UnicContext _context;

        public ManutencaoRepository(UnicContext context)
        {
            _context = context;
        }

        public async Task AdicionarManutencao(Manutencao manutencao)
        {
            await _context.Manutencao.AddAsync(manutencao);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarManutencao(int id)
        {
            var manutencao = await RecuperarManutencao(id);
             _context.Manutencao.Remove(manutencao);
            await _context.SaveChangesAsync();
        }

        public Task EditarManutencao(int id, Manutencao manutencao)
        {
            throw new NotImplementedException();
        }

        public List<Manutencao> ListarManutencoes(int carroId)
        {
            var manutencoes =  _context.Manutencao
                .Where(m => m.CarroId == carroId)
                .OrderByDescending(m => m.DataCriacao)
                .ToList();
            
            return manutencoes;
        }

        public async Task<Manutencao> RecuperarManutencao(int id)
        {
            var manutencao = await _context.Manutencao.FindAsync(id);
            return manutencao;
        }
    }
}
