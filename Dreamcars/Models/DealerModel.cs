using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Dreamcars.Models
{
    [DataContract(Name = "Dealer")]
   public class DealerModel
    {
        
        [DataMember(Name = "Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Azienda")]
        public string Azienda { get; set; }
        [DataMember(Name = "Indirizzo")]
        public string Indirizzo { get; set; }
        [DataMember(Name = "Citta")]
        public string Citta { get; set; }
        [DataMember(Name = "Provincia")]
        public string Provincia { get; set; }
        [DataMember(Name = "Cap")]
        public string Cap { get; set; }
        [DataMember(Name = "Telefono")]
        public string Telefono { get; set; }
        [DataMember(Name = "Logo")]
        public string Logo { get; set; }
        [DataMember(Name = "Piva")]
        public string Piva { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }

    }
}