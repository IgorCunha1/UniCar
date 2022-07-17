using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data.Dtos;
using Unic.Models;

namespace Unic.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<AlterarEnderecoDto, Endereco>();
            CreateMap<Endereco, RecuperarEnderecoDto>();
            CreateMap<Endereco, ListarEnderecoDto>();
        }
    }
}
