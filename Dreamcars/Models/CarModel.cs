using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Dreamcars.Models
{
    [DataContract(Name = "Car")]
    public class CarModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Marca")]
        public string Marca { get; set; }
        [DataMember(Name = "Modello")]
        public string Modello { get; set; }
        [DataMember(Name = "Anno")]
        public int Anno { get; set; }
        [DataMember(Name = "Mese")]
        public int Mese { get; set; }
        [DataMember(Name = "Tipo")]
        public int Tipo { get; set; }
        [DataMember(Name = "Giacenza")]
        public int Giacenza { get; set; }
        [DataMember(Name = "Telaio")]
        public string Telaio { get; set; }
        [DataMember(Name = "Motore")]
        public string Motore { get; set; }
        [DataMember(Name = "Targa")]
        public string Targa { get; set; }
        [DataMember(Name = "Stato")]
        public string Stato { get; set; }
        [DataMember(Name = "Foto")]
        public string Foto { get; set; }
        [DataMember(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [DataMember(Name = "PrezzoAcquisto")]
        public int PrezzoAcquisto { get; set; }
        [DataMember(Name = "PrezzoVendita")]
        public int PrezzoVendita { get; set; }
        [DataMember(Name = "PrezzoDealer")]
        public int PrezzoDealer { get; set; }
        [DataMember(Name = "DataEntrata")]
        public DateTime DataEntrata { get; set; }
        [DataMember(Name = "DataUscita")]
        public DateTime DataUscita { get; set; }
        [DataMember(Name = "Venduta")]
        public bool Venduta { get; set; }
        [DataMember(Name="Neopatentati")]
        public bool Neopatentati { get; set; }
        [DataMember(Name = "EsenzioneBollo")]
        public bool EsenzioneBollo { get; set; }
        [DataMember(Name = "Portale")]
        public bool Portale { get; set; }
        [DataMember(Name = "PortaleDealers")]
        public bool PortaleDealers { get; set; }
        [DataMember(Name = "RipristiniEff")]
        public IList<RipristinoModel> RipristiniEff { get; set; }
        [DataMember(Name="Immagini")]
        public IList<ImmaginiCarModel> Immagini { get; set; }
        [DataMember(Name = "TotaleRipristini")]
        public int TotaleRipristini { get; set; }
        [DataMember(Name = "IdClienteAcq")]
        public int IdClienteAcq { get; set; }
        [DataMember(Name="ClienteAcq")]
        public ClienteModel ClienteAcq { get; set; }
        [DataMember(Name = "IdClienteVen")]
        public int IdClienteVen { get; set; }
        [DataMember(Name = "ClienteVen")]
        public ClienteModel ClienteVen { get; set; }

    }
}