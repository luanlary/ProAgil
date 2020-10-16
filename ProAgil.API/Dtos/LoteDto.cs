using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal preco { get; set; }
        public string DataFim { get; set; }
        public int Quantidade { get; set; }

    }
}
