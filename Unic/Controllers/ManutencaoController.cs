using Unic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using AutoMapper;
using Unic.Repositories.Interfaces;
using Unic.Data.Dtos.Manutencao;
using Unic.Data.Dtos.ModeloDto;

namespace Unic.Controllers
{

    public class ManutencaoController : Controller
    {
        private UnicContext _context;
        private IMapper _mapper;
        private IManutencaoRepository _ManutencaoRepository;

        public ManutencaoController(UnicContext unicContext, IMapper mapper, IManutencaoRepository ManutencaoRepository)
        {
            _context = unicContext;
            _mapper = mapper;
            _ManutencaoRepository = ManutencaoRepository;
        }

        [HttpGet]
        [Route("Manutencao")]
        public IActionResult Manutencao()
        {
            return View();
        }

        [HttpPost]
        [Route("Manutencao/AdicionarManutencao")]
        public async Task<IActionResult> AdicionarManutencaoAsync([FromBody] CreateManutencaoDto ManutencaoDto)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
           
            Manutencao Manutencao = _mapper.Map<Manutencao>(ManutencaoDto);
            await _ManutencaoRepository.AdicionarManutencao(Manutencao);

            return Ok();
        }

        [HttpGet]
        [Route("Manutencao/RecuperarManutencao/{id}")]
        public IActionResult RecuperarManutencao(int id)
        {

            Manutencao Manutencao = _context.Manutencao.FirstOrDefault(p => p.Id == id);
            var ManutencaoDto = _mapper.Map<APIModeloDto>(Manutencao);
            if (Manutencao != null)
            {
                return Ok(ManutencaoDto);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("Manutencao/EditarManutencao/{id}")]
        public IActionResult Editar(int id, [FromBody] Manutencao ManutencaoEditado)
        {
            
            Manutencao Manutencao = _context.Manutencao.FirstOrDefault(p => p.Id == id);
            if(Manutencao == null)
            {
                return NotFound();
            }

            Manutencao = _mapper.Map<Manutencao>(ManutencaoEditado);

            _context.SaveChanges();
            
            return Ok(Manutencao);
        }


        [HttpGet]
        [Route("Manutencao/ListarManutencao/{carroId}")]
        public IActionResult ListarManutencao(int carroId)
        {
            try 
            { 
                var Manutencaos = _ManutencaoRepository.ListarManutencoes(carroId);
                List<ListarManutencoesDto> ManutencaoDto = new();
                foreach (var p in Manutencaos)
                {
                    ManutencaoDto.Add(_mapper.Map<ListarManutencoesDto>(p));
                }
            
                return Json(Manutencaos);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }
        
        [HttpPost]
        [Route("Manutencao/Deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try { 

                Manutencao Manutencao = _context.Manutencao.FirstOrDefault(p => p.Id == id);
                if(Manutencao != null)
                {
                    await _ManutencaoRepository.DeletarManutencao(Manutencao.Id);
                    return Ok();
                }
                else
                {
                    return Json("Manutencao não Encontrado");
                }
                
            }
            catch (Exception ex)
            {
               return Json(ex.Message);
            }
        }


    }
}
