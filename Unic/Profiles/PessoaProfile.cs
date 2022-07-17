using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data.Dtos.Pessoa;
using Unic.Models;

namespace Unic.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<CreatePessoaDto, Pessoa>();
            CreateMap<AlterarPessoaDto, Pessoa>();
            CreateMap<Pessoa, RecuperarPessoaDto>();
            CreateMap<Pessoa, ListarPessoaDto>();
        }
    }
}
