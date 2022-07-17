using Unic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Data.Dtos.Pessoa;
using AutoMapper;
using Unic.Repositories.Interfaces;

namespace Unic.Controllers
{

    public class PessoaController : Controller
    {
        private UnicContext _context;
        private IMapper _mapper;
        private IPessoaRepository _pessoaRepository;

        public PessoaController(UnicContext unicContext, IMapper mapper, IPessoaRepository pessoaRepository)
        {
            _context = unicContext;
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        [Route("Pessoa")]
        public IActionResult Pessoa()
        {
            return View();
        }

        [HttpPost]
        [Route("Pessoa/AdicionarPessoa")]
        public async Task<IActionResult> AdicionarPessoaAsync([FromBody] CreatePessoaDto pessoaDto)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }

            Pessoa pessoa = _mapper.Map<Pessoa>(pessoaDto);
            await _pessoaRepository.AdicionarPessoa(pessoa);

            return Ok();

        }

        [HttpGet]
        [Route("Pessoa/RecuperarPessoa/{id}")]
        public IActionResult RecuperarPessoa(int id)
        {

            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            var pessoaDto = _mapper.Map<RecuperarPessoaDto>(pessoa);
            if (pessoa != null)
            {
                return Ok(pessoaDto);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("Pessoa/EditarPessoa/{id}")]
        public IActionResult Editar(int id, [FromBody] AlterarPessoaDto PessoaEditada)
        {
            
            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            if(pessoa == null)
            {
                return NotFound();
            }

            pessoa = _mapper.Map<Pessoa>(PessoaEditada);

            _context.SaveChanges();
            
            return Ok(pessoa);
        }


        [HttpGet]
        [Route("Pessoa/ListarPessoa")]
        public IActionResult ListarPessoa()
        {
            var pessoas = _pessoaRepository.ListarPessoas();
            List<ListarPessoaDto> pessoaDto = new();
            foreach (var p in pessoas)
            {
                pessoaDto.Add(_mapper.Map<ListarPessoaDto>(p));
            }
            
            return Json(pessoas);
        }
        
        [HttpPost]
        [Route("Pessoa/Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try { 

                Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
                if(pessoa != null)
                {
                    _context.Pessoa.Remove(pessoa);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Json("Cliente não Encontrado");
                }
                
            }
            catch (Exception ex)
            {
               return Json(ex.Message);
            }
        }


    }
}
