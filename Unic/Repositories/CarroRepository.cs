using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Data.Dtos.Carro;
using Unic.Data.Dtos.Manutencao;
using Unic.Data.Dtos.ModeloDto;
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
            if(carro != null)
            {
                carro.ValorTotalManutencao = 0;
            
                foreach (var m in carro.Manutencoes)
                {
                    carro.ValorTotalManutencao += m.Valor;
                }

                return carro;
            }
            return null;
        }

        public bool ValidarPlaca(string placa)
        {
            var carro = RecuperarCarroPorPlaca(placa);
            if (string.IsNullOrEmpty(placa)) return false;
            if (carro != null) return false;

            return true;
        }

        public List<Marca> GetApiMarcas()
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://parallelum.com.br/fipe/api/v1/carros/marcas");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.ContentType = "Json";
            var resposta = requisicaoWeb.GetResponse();
            var dados = resposta.GetResponseStream();
            StreamReader reader = new StreamReader(dados);
            object objResposta = reader.ReadToEnd();
            var marcas = JsonConvert.DeserializeObject<List<Marca>>(objResposta.ToString());

            return marcas;
        }
        public async Task GetApiModelos()
        {
            try
            {

                var marcasDbo = _context.Marca.ToList();

                HttpWebRequest requisicaoWeb = null;
                var modelos = new List<Modelo>();
                var anos = new List<Anos>();
            

                foreach (var m in marcasDbo)
                {
                    requisicaoWeb = WebRequest.CreateHttp("https://parallelum.com.br/fipe/api/v1/carros/marcas/"+m.codigo+"/modelos");
                    requisicaoWeb.Method = "GET";
                    requisicaoWeb.ContentType = "Json";
                    var resposta = requisicaoWeb.GetResponse();
                    var dados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(dados);
                    object objResposta = reader.ReadToEnd();
                    var modelosDto = JsonConvert.DeserializeObject<ModeloAnoDto>(objResposta.ToString());
                    
                    foreach(var mdl in modelosDto.modelos.ToList())
                    {
                        Modelo mnovo = _mapper.Map<Modelo>(mdl);
                        mnovo.MarcaId = m.codigo;
                        modelos.Add(mnovo);
                        await _context.Modelo.AddAsync(mnovo);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }

        public async Task GetApiAnos()
        {
            var modelosDbo = _context.Modelo.ToList();

            HttpWebRequest requisicaoWeb = null;
            
            foreach (var m in modelosDbo)
            {
                requisicaoWeb = WebRequest.CreateHttp("https://parallelum.com.br/fipe/api/v1/carros/marcas/"+m.MarcaId+"/modelos/"+m.codigo+"/anos");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "Json";

                var resposta = await requisicaoWeb.GetResponseAsync();
                var dados = resposta.GetResponseStream();

                StreamReader reader = new StreamReader(dados);
                object objResposta = await reader.ReadToEndAsync();
                var anos = JsonConvert.DeserializeObject<List<Anos>>(objResposta.ToString());

                var anosModelosDbo = verificarAnosModelo(m.codigo);

                if (anos != null && anosModelosDbo.Count() < anos.Count())
                {
                    foreach (var a in anos)
                    {
                        Anos novoAno = _mapper.Map<Anos>(a);
                        novoAno.modeloID = m.codigo;

                        await _context.Anos.AddAsync(novoAno);
                        await _context.SaveChangesAsync();
                    }
                }
                resposta.Close();

            }
        }

        public List<Marca> verificarMarcas()
        {
            var marcas = _context.Marca.ToList();
            return marcas.ToList();
        }

        public List<Modelo> verificarModelosMarcas(int MarcaId)
        {
            var modelos = _context.Modelo.ToList().Where(m => m.MarcaId == MarcaId);
            return modelos.ToList();
        }

        public List<Anos> verificarAnosModelo(int Modeloid)
            {
                var anos = _context.Anos.ToList().Where(a => a.modeloID == Modeloid);
                return anos.ToList();
            }



    }
}
