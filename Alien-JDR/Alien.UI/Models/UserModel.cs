using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset ConnectedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        
        // Peut-être rajouter une propriété comme un token, reste à voir
    }
}
