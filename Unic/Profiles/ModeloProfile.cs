using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data.Dtos.ModeloDto;
using Unic.Data.Dtos.Pessoa;
using Unic.Models;

namespace Unic.Profiles
{
    public class ModeloProfile : Profile
    {
        public ModeloProfile()
        {
            CreateMap<APIModeloDto, Modelo>();
            CreateMap<Modelo, APIModeloDto>();

        }
    }
}
