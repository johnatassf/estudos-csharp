﻿

using Microsoft.AspNetCore.Identity;

namespace Dominio.Entities
{
    public class User: IdentityUser<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
