using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data.Dtos.Carro;
using Unic.Models;

namespace Unic.Profiles
{
    public class CarroProfile : Profile
    {
       public CarroProfile()
        {
            CreateMap<CreateCarroDto, Carro>();
            CreateMap<AlterarCarroDto, Carro>();
            CreateMap<Carro, RecuperarCarroDto>();
            CreateMap<Carro, ListarCarroDto>();
        }
        
    }
}
