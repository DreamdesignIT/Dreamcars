using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Dreamcars.Models
{
    [DataContract(Name = "Ripristino")]
    public class RipristinoModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [DataMember(Name = "Prezzo")]
        public int Prezzo { get; set; }
        
    }
}