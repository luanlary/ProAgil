using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Local obrigatório")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="O local deve conter entre 3 e 50 caracteres") ]
        public string Local { get; set; }
        [Required (ErrorMessage ="O tema é obrigatório")]
        public string Tema { get; set; }
        public string DataEvento { get; set; }
        [Range(2,120000, ErrorMessage ="Quantidade de pessoas deve ser entre 2 a 120000")]
        public int QtdPessoas { get; set; }
        public string Imagemurl { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto>Plestrantes { get; set; }
    }
}
