using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Plestrantes, opt =>
                {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                }).ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Eventos, opt =>
                 {
                     opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList());
                 }).ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();



        }

        public EventoDto Evento { get; }
    }
}
