using Unic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using AutoMapper;
using Unic.Repositories.Interfaces;
using Unic.Data.Dtos.Carro;

namespace Unic.Controllers
{

    public class CarroController : Controller
    {
        private UnicContext _context;
        private IMapper _mapper;
        private ICarroRepository _carroRepository;

        public CarroController(UnicContext unicContext, IMapper mapper, ICarroRepository carroRepository)
        {
            _context = unicContext;
            _mapper = mapper;
            _carroRepository = carroRepository;
        }

        [HttpGet]
        [Route("Carro")]
        public IActionResult Carro()
        {
            return View();
        }

        [HttpPost]
        [Route("Carro/AdicionarCarro")]
        public async Task<IActionResult> AdicionarCarroAsync([FromBody] CreateCarroDto CarroDto)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
           
            Carro Carro = _mapper.Map<Carro>(CarroDto);
            await _carroRepository.AdicionarCarro(Carro);

            return Ok();
        }

        [HttpGet]
        [Route("Carro/RecuperarCarro/{id}")]
        public IActionResult RecuperarCarro(int id)
        {

            Carro carro = _context.Carro.FirstOrDefault(p => p.Id == id);
            var carroDto = _mapper.Map<RecuperarCarroDto>(carro);
            if (carro != null)
            {
                return Ok(carroDto);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("Carro/EditarCarro/{id}")]
        public IActionResult Editar(int id, [FromBody] AlterarCarroDto CarroEditado)
        {
            
            Carro carro = _context.Carro.FirstOrDefault(p => p.Id == id);
            if(carro == null)
            {
                return NotFound();
            }

            carro = _mapper.Map<Carro>(CarroEditado);

            _context.SaveChanges();
            
            return Ok(carro);
        }


        [HttpGet]
        [Route("Carro/ListarCarro")]
        public IActionResult ListarCarro()
        {
            var Carros = _carroRepository.ListarCarros();
            List<ListarCarroDto> CarroDto = new();
            foreach (var p in Carros)
            {

                CarroDto.Add(_mapper.Map<ListarCarroDto>(p));
            }
            
            return Json(Carros);
        }
        
        [HttpPost]
        [Route("Carro/Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try { 

                Carro Carro = _context.Carro.FirstOrDefault(p => p.Id == id);
                if(Carro != null)
                {
                    _context.Carro.Remove(Carro);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Json("Carro não Encontrado");
                }
                
            }
            catch (Exception ex)
            {
               return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("Carro/RecuperarCarroPorPlaca/{placa}")]
        public IActionResult RecuperarCarroPorPlaca(string placa)
        {

            Carro carro = _carroRepository.RecuperarCarroPorPlaca(placa);
            var carroDto = _mapper.Map<RecuperarCarroDto>(carro);
            if (carro != null)
            {
                return Ok(carroDto);
            }

            return NotFound();
        }


    }
}
