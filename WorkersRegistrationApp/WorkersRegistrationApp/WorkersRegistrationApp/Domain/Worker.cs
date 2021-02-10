using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    /// <summary>
    /// The <c>Worker</c> class.
    /// Represents database entity "Worker" as class
    /// </summary>
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime Date { get; set; }
        public string Job { get; set; }

        //reperesentation of foreign key
        public int CompanyId { get; set; }

        //navigation property
        public Company Company { get; set; } = default;

    }
}