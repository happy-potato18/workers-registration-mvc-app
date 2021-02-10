using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LegalForm { get; set; }

        public int CountOfWorkers { get; set; } = 0;
    }
}