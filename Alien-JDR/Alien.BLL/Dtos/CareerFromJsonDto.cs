using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class CareerFromJsonDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string KeyAttribute { get; set; }
        public string[] Competences { get; set; }
        public string[] Talents { get; set; }
        public string[] TypicalNames { get; set; }
    }
}
