using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CritiqlyNexusCore.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public DateTime? email_verified_at { get; set; }
        public string password { get; set; }
        public bool is_admin { get; set; }

    }
}
