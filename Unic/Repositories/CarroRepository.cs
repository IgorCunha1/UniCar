using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Data.Dtos.Carro;
using Unic.Models;
using Unic.Repositories.Interfaces;

namespace Unic.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private readonly UnicContext _context;
        private readonly IMapper _mapper;

        public CarroRepository(UnicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AdicionarCarro(Carro carro)
        {
            await _context.Carro.AddAsync(carro);
            await _context.SaveChangesAsync();
        }

        public List<Carro> ListarCarros()
        {
            var carros = _context.Carro.OrderByDescending(p => p.DataCadastro).ToList();
            return carros;                     
        }
        public Carro RecuperarCarro(int id)
        {
            var carro = _context.Carro.Where(c => c.Id == id).First();
            return carro;
        }
        public void EditarCarro(int id, AlterarCarroDto carroEditado)
        {

          Carro carro = RecuperarCarro(id);
          carro = _mapper.Map<Carro>(carroEditado);
           _context.SaveChanges();

        }

        public void DeletarCarro(int id)
        {
            var carro = RecuperarCarro(id);
            _context.Carro.Remove(carro);
            _context.SaveChanges();
        }

        public Carro RecuperarCarroPorPlaca(string placa)
        {
            var carro = _context.Carro.FirstOrDefault(c => c.Placa == placa);
            return carro;
        }
    }
}
