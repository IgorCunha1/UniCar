using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data.Dtos.Manutencao;
using Unic.Models;

namespace Unic.Profiles
{
    public class ManutencaoProfile : Profile
    {
        public ManutencaoProfile()
        {
            CreateMap<CreateManutencaoDto, Manutencao>();
            CreateMap<Manutencao, CreateManutencaoDto>();
            CreateMap<Manutencao, ListarManutencoesDto>();
            CreateMap<ListarManutencoesDto, Manutencao>();
            
            CreateMap<RecuperarManutencaoDto, Manutencao>();
        }
    }
}
