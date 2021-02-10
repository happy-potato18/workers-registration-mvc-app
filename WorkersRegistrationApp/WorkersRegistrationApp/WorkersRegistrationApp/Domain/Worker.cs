using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime Date { get; set; }
        public string Job { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = default;

    }
}