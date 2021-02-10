using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    /// <summary>
    /// The <c>Company</c> class.
    /// Represents database entity "Company" as class
    /// </summary>
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LegalForm { get; set; }

        //using for storing number of workers
        public int CountOfWorkers { get; set; } = 0;
    }
}