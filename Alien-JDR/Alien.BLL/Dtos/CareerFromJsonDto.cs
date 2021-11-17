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
        public string Description { get; set; }
        public string KeyAttribute { get; set; }
        public IEnumerable<string> Competences { get; set; }
        public IEnumerable<string> Talents { get; set; }
        public IEnumerable<string> TypicalNames { get; set; }
    }
}
