using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tartak.WebApp.Server.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
