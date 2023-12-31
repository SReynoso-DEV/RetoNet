﻿namespace Reto.API.Models.Request.Client
{
    public class UpdateClientRequest
    {
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Status { get; set; }
    }
}
