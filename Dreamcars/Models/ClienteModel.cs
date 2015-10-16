using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Dreamcars.Models
{
    public class ClienteModel
    {
        [DataMember(Name ="Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Nome")]
        public string Nome { get; set; }
        [DataMember(Name = "Cognome")]
        public string Cognome { get; set; }
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
        [DataMember(Name = "Cellulare")]
        public string Cellulare { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "Piva")]
        public string Piva { get; set; }
        [DataMember(Name = "Azienda")]
        public bool Azienda { get; set; }
        [DataMember(Name = "Rs")]
        public string Rs { get; set; }
        [DataMember(Name = "Referente")]
        public string Referente { get; set; }
        [DataMember(Name = "Note")]
        public string Note { get; set; }
        [DataMember(Name ="AutoAcq")]
        public IList<CarModel> AutoAcq { get; set; }
        [DataMember(Name = "AutoVen")]
        public IList<CarModel> AutoVen { get; set; }
    }
}