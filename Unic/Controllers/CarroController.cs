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
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

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

        public async Task<IActionResult> att()
        {
            try
            {
                await _carroRepository.GetApiAnos();
                return Ok();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        [Route("Carro/AdicionarCarro")]
        public async Task<IActionResult> AdicionarCarroAsync([FromBody] CreateCarroDto CarroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }

                if (CarroDto == null)
                {
                    return StatusCode(400, "Erro ao receber os Dados");
                }

                bool validarPlaca = _carroRepository.ValidarPlaca(CarroDto.Placa);

                if (!validarPlaca)
                {
                    return BadRequest("A Placa digitada já consta na base atual");
                }

                Carro Carro = _mapper.Map<Carro>(CarroDto);
                await _carroRepository.AdicionarCarro(Carro);

                return Ok();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
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

            carro = _mapper.Map<Carro>(CarroEditado);

            if (carro == null || !ModelState.IsValid)
            {
                return NotFound();
            }
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
            try
            {

                Carro Carro = _context.Carro.FirstOrDefault(p => p.Id == id);
                if (Carro != null)
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
            try
            {
                Carro carro = _carroRepository.RecuperarCarroPorPlaca(placa);
                var carroDto = _mapper.Map<RecuperarCarroDto>(carro);
                if (carro != null)
                {
                    return Ok(carroDto);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        [Route("Carro/verificarMarcas")]
        public IActionResult verificarMarcas()
        {
            try
            {
                List<Marca> marca = _carroRepository.verificarMarcas();

                if (marca != null)
                {
                    return Ok(marca);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        [Route("Carro/verificarModelosMarcas/{MarcaId}")]
        public IActionResult verificarModelosMarcas(int MarcaId)
        {
            try
            {
                List<Modelo> modelo = _carroRepository.verificarModelosMarcas(MarcaId);

                if (modelo != null)
                {
                    return Ok(modelo);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        [Route("Carro/verificarAnosModelo/{Modeloid}")]
        public IActionResult verificarAnosModelo(int Modeloid)
        {
            try
            {
                List<Anos> Anos = _carroRepository.verificarAnosModelo(Modeloid);

                if (Anos != null)
                {
                    return Ok(Anos);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarcaUpload(IFormFile file)
        {
            try
            {
                string pasta = "imagens";
                //upload para a pasta [files]
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/files", file.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{pasta}", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                FileStream meuArq = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(meuArq, Encoding.UTF8);

                while (!sr.EndOfStream)
                {
                    string[] str = sr.ReadLine().Split(";");

                    Marca importacao = null;

                    var codigo = str[0];
                    var nome = str[1];

                    importacao = new Marca()
                    {
                        codigo = int.Parse(codigo),
                        nome = nome
                    };

                }

                return Ok();
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}