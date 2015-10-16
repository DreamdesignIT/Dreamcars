using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.WebApi;
using Umbraco.Web;
using Umbraco.Core.Models;
using Dreamcars.Models;
using System.Web.Http;

namespace Dreamcars.Controllers
{
    public class AdminApiController : UmbracoApiController
    {
        #region GET
        #region Dealer
        public IContent getDealer()
        {
            //int idm = Members.GetCurrentMemberId();
            int dealerID = Members.GetCurrentMember().GetPropertyValue<int>("dealer");
            var cont = Services.ContentService;
            var dealer = cont.GetById(dealerID);
            return dealer;
        }
        public DealerModel getDealerData()
        {
            var dealer = getDealer();
            DealerModel myd = new DealerModel();
            var ms = Services.MediaService;
            
            myd.Id = dealer.Id;
            myd.Name = dealer.Name;
            myd.Azienda = dealer.GetValue<string>("azienda");
            myd.Indirizzo = dealer.GetValue<string>("indirizzo");
            myd.Citta = dealer.GetValue<string>("citta");
            myd.Provincia = dealer.GetValue<string>("provincia");
            myd.Cap = dealer.GetValue<string>("cap");
            myd.Telefono = dealer.GetValue<string>("telefono");
            myd.Piva = dealer.GetValue<string>("piva");
            myd.Email = dealer.GetValue<string>("email");
            object logo = dealer.GetValue("logo");
            if(logo != null)
                myd.Logo = ms.GetById(dealer.GetValue<int>("logo")).GetValue<string>("umbracoFile");
            return myd;
        }
        [HttpGet]
        public bool checkDealer()
        {
            int dealerID = Members.GetCurrentMember().GetPropertyValue<int>("dealer");
            if (dealerID == 0)
                return true;
            else
                return false;
        }
        #endregion
        #region Auto
        public IEnumerable<CarModel> getAuto(int tipo, bool venduta)
        {
            var dealer = getDealer();
            var ms = Services.MediaService;
            var q = from a in dealer.Descendants()
                    where a.ContentType.Alias == "Car" && a.GetValue<bool>("venduta") == venduta
                    select a;
            if (tipo != 0)
                q = q.Where(a => a.GetValue<int>("tipo") == tipo);
           
            IList<CarModel> carList = new List<CarModel>();
            foreach (var c in q) {
                CarModel mc = IconvCarModel(c);
                carList.Add(mc);
            }
            return carList;
        }
        public CarModel getAutoDetails(int id) {
            var dealer = getDealer();
            var ms = Services.MediaService;
            var c = (from a in dealer.Descendants()
                    where a.ContentType.Alias == "Car" && a.Id == id
                    select a).First();
            CarModel mm = IconvCarModel(c);
            return mm;
        }
        public IList<RipristinoModel> getAllRipristini(int idcar)
        {
            var dealer = getDealer();
            var ms = Services.MediaService;
            var c = from a in dealer.Descendants()
                     where a.ContentType.Alias == "Ripristino" && a.ParentId == idcar
                     select a;

            IList<RipristinoModel> rp = new List<RipristinoModel>();
            foreach (var r in c)
            {
                RipristinoModel rps = IconvRipristinoModel(r);
                rp.Add(rps);
            }
            return rp;
        }
        public IList<ImmaginiCarModel> getAllImmagini(int id)
        {
            var dealer = getDealer();
            var c = from a in dealer.Descendants()
                    where a.ContentType.Alias == "Immagine" && a.ParentId == id
                    select a;
            IList<ImmaginiCarModel> imgs = new List<ImmaginiCarModel>();
            foreach (var i in c) {
                ImmaginiCarModel img = IconvImmaginiCarModel(i);
                imgs.Add(img);
            }
            return imgs;
        }
        #endregion
        #region Clienti
        public IList<ClienteModel> getClientiAll(bool azienda) {
            var dealer = getDealer();
            var ms = Services.MediaService;
            var q = from a in dealer.Descendants()
                    where a.ContentType.Alias == "Cliente" && a.GetValue<bool>("azienda") == azienda
                    select a;
            IList<ClienteModel> clis = new List<ClienteModel>();
            foreach (var c in q)
            {
                ClienteModel cli = IconvClienteModel(c);
                clis.Add(cli);
            }
            return clis;
        }
        public ClienteModel getCliente(int id)
        {
            var dealer = getDealer();
            var ms = Services.MediaService;
            var c = (from a in dealer.Descendants()
                    where a.ContentType.Alias == "Cliente" && a.Id == id
                    select a).First();

            ClienteModel cli = IconvClienteModel(c);
            
            return cli;
        }
        public IList<CarModel> getAutoAcqCli(int id)
        {
            var dealer = getDealer();
            var q = from a in dealer.Descendants()
                     where a.ContentType.Alias == "Car" && a.GetValue<int>("clienteVen") == id
                     select a;
            IList<CarModel> vens = new List<CarModel>();
            foreach (var c in q)
            {
                CarModel mc = IconvCarModel(c);
                vens.Add(mc);
            }
            return vens;

        }
        public IList<CarModel> getAutoVenCli(int id)
        {
            var dealer = getDealer();
            var q = from a in dealer.Descendants()
                    where a.ContentType.Alias == "Car" && a.GetValue<int>("clienteAcq") == id
                    select a;
            IList<CarModel> vens = new List<CarModel>();
            foreach (var c in q)
            {
                CarModel mc = IconvCarModel(c);
                vens.Add(mc);
            }
            return vens;

        }
        #endregion
        #endregion
        #region CRUD
        #region Create
        [HttpPost]
        public bool creaDealer(DealerModel d)
        {
            try
            {
                int dealerID = Members.GetCurrentMember().Id;
                var cs = Services.ContentService;
                var ms = Services.MediaService;
                var mbs = Services.MemberService;
                var dealer = cs.CreateContent(d.Azienda, getDealersFolderId(), "Dealer");
                cs.SaveAndPublishWithStatus(dealer);
                var stock = cs.CreateContent("Stock", dealer.Id, "Stock");
                cs.SaveAndPublishWithStatus(stock);
                var clienti = cs.CreateContent("Clienti", dealer.Id, "Clienti");
                cs.SaveAndPublishWithStatus(clienti);
                var mediaFolder = ms.CreateMedia(d.Azienda, 1073, "Folder");
                ms.Save(mediaFolder);
                dealer.SetValue("dealerMediaFolder", mediaFolder.Id);
                dealer.SetValue("azienda", d.Azienda);
                dealer.SetValue("cap", d.Cap);
                dealer.SetValue("citta", d.Citta);
                dealer.SetValue("email", d.Email);
                dealer.SetValue("indirizzo", d.Indirizzo);
                dealer.SetValue("piva", d.Piva);
                dealer.SetValue("provincia", d.Provincia);
                dealer.SetValue("telefono", d.Telefono);
                cs.SaveAndPublishWithStatus(dealer);
                IMember mb = mbs.GetById(dealerID);
                mb.SetValue("dealer", dealer.Id);
                mbs.Save(mb);
                return true;
            }
            catch
            {
                return false;
            }

        }
        [HttpGet]
        public int creaAuto(string name)
        {
            int idStock = getStockFolderID();
            var sc = Services.ContentService;
            var ca = sc.CreateContent(name, idStock, "Car");
            ca.SetValue("anno", 0);
            ca.SetValue("mese", 0);
            ca.SetValue("tipo", 0);
            ca.SetValue("foto", 0);
            ca.SetValue("prezzoAcquisto", 0);
            ca.SetValue("prezzoVendita", 0);
            ca.SetValue("prezzoDealers", 0);
            ca.SetValue("dataEntrata", DateTime.Today);
            ca.SetValue("pubblicaDealers", false);
            ca.SetValue("PubblicaPortale", false);
            ca.SetValue("neoPatentati", false);
            ca.SetValue("esenzioneBollo", false);
            ca.SetValue("venduta", false);
            ca.SetValue("clienteAcq", 0);
            ca.SetValue("clienteVen", 0);
            sc.SaveAndPublishWithStatus(ca);
            return ca.Id;
        }
        [HttpPost]
        public int updateAuto(CarModel c)
        {
            var sc = Services.ContentService;
            IContent mc = sc.GetById(c.Id);
            mc.Name = c.Name;
            mc.SetValue("marca", c.Marca);
            mc.SetValue("modello", c.Modello);
            mc.SetValue("anno", c.Anno);
            mc.SetValue("mese", c.Mese);
            mc.SetValue("tipo", c.Tipo);
            mc.SetValue("telaio", c.Telaio);
            mc.SetValue("motore", c.Motore);
            mc.SetValue("targa", c.Targa);
            mc.SetValue("stato", c.Stato);
            mc.SetValue("descrizione", c.Descrizione);
            mc.SetValue("prezzoAcquisto", c.PrezzoAcquisto);
            mc.SetValue("prezzoVendita", c.PrezzoVendita);
            mc.SetValue("prezzoDealers", c.PrezzoDealer);
            mc.SetValue("dataEntrata", c.DataEntrata);
            mc.SetValue("dataUscita", c.DataUscita);
            mc.SetValue("venduta", c.Venduta);
            mc.SetValue("esenzioneBollo", c.EsenzioneBollo);
            mc.SetValue("neopatentati", c.Neopatentati);
            mc.SetValue("pubblicaPortale", c.Portale);
            mc.SetValue("pubblicaDealers", c.PortaleDealers);
            mc.SetValue("clienteAcq", c.ClienteAcq);
            mc.SetValue("clienteVen", c.ClienteVen);
            sc.SaveAndPublishWithStatus(mc);
            return mc.Id;
        }
        [HttpGet]
        public int creaRipristino(int idcar, string name)
        {
            var sc = Services.ContentService;
            var ca = sc.CreateContent(name, idcar, "Ripristino");
            sc.SaveAndPublishWithStatus(ca);
            return ca.Id;
        }
        [HttpPost]
        public int updateRipristino(RipristinoModel r)
        {
            var cs = Services.ContentService;
            IContent rp = cs.GetById(r.Id);
            rp.Name = r.Name;
            rp.SetValue("descrizione", r.Descrizione);
            rp.SetValue("prezzo", r.Prezzo);
            cs.SaveAndPublishWithStatus(rp);
            return r.Id;
        }
        [HttpGet]
        public int creaCliente(string name, bool azienda)
        {
            int idCli = getClientiFolderID();
            var sc = Services.ContentService;
            var ca = sc.CreateContent(name, idCli, "Cliente");
            ca.SetValue("azienda", azienda);
            sc.SaveAndPublishWithStatus(ca);
            return ca.Id;
        }
        [HttpPost]
        public int updateCliente(ClienteModel c)
        {
            var sc = Services.ContentService;
            IContent cl = sc.GetById(c.Id);
            cl.Name = c.Name;
            cl.SetValue("azienda", c.Azienda);
            cl.SetValue("cap", c.Cap);
            cl.SetValue("cellulare", c.Cellulare);
            cl.SetValue("citta", c.Citta);
            cl.SetValue("cognome", c.Cognome);
            cl.SetValue("email", c.Email);
            cl.SetValue("indirizzo", c.Indirizzo);
            cl.SetValue("nome", c.Nome);
            cl.SetValue("note", c.Note);
            cl.SetValue("piva", c.Piva);
            cl.SetValue("provincia", c.Provincia);
            cl.SetValue("referente", c.Referente);
            cl.SetValue("rs", c.Rs);
            cl.SetValue("telefono", c.Telefono);
            sc.SaveAndPublishWithStatus(cl);
            return cl.Id;
        }
        #endregion
        #endregion
        #region HELPER CONVERT
        private CarModel IconvCarModel(IContent c)
        {
            var ms = Services.MediaService;
            CarModel mc = new CarModel();
            mc.Id = c.Id;
            mc.Name = c.Name;
            mc.Marca = c.GetValue<string>("marca");
            mc.Anno = c.GetValue<int>("anno");
            mc.DataEntrata = c.GetValue<DateTime>("dataEntrata");
            mc.DataUscita = c.GetValue<DateTime>("dataUscita");
            mc.Descrizione = c.GetValue<string>("descrizione");
            mc.EsenzioneBollo = c.GetValue<bool>("esenzioneBollo");
            if (c.GetValue<int>("foto") != 0)
            mc.Foto = ms.GetById(c.GetValue<int>("foto")).GetValue<string>("umbracoFile");
            if (mc.DataUscita == DateTime.MinValue)
            {
                mc.Giacenza = (DateTime.Today - mc.DataEntrata).Days;
            }
            else
            {
                mc.Giacenza = (mc.DataUscita - mc.DataEntrata).Days;
            }
            mc.Mese = c.GetValue<int>("mese");
            mc.Modello = c.GetValue<string>("modello");
            mc.Motore = c.GetValue<string>("motore");
            mc.Stato = c.GetValue<string>("stato");
            mc.Portale = c.GetValue<bool>("pubblicaPortale");
            mc.PortaleDealers = c.GetValue<bool>("pubblicaDealers");
            mc.PrezzoAcquisto = c.GetValue<int>("prezzoAcquisto");
            mc.PrezzoDealer = c.GetValue<int>("prezzoDealers");
            mc.PrezzoVendita = c.GetValue<int>("prezzoVendita");
            mc.Targa = c.GetValue<string>("targa");
            mc.Telaio = c.GetValue<string>("telaio");
            mc.Tipo = c.GetValue<int>("tipo");
            mc.Immagini = getAllImmagini(c.Id);
            mc.RipristiniEff = getAllRipristini(c.Id);
            foreach (var cos in mc.RipristiniEff)
            {
                mc.TotaleRipristini += cos.Prezzo;
            }
            object cv = c.GetValue("clienteVen");
            object ca = c.GetValue("clienteAcq");
            if (cv != null)
                mc.ClienteVen = getCliente(c.GetValue<int>("clienteVen"));
            if (ca != null)
                mc.ClienteAcq = getCliente(c.GetValue<int>("clienteAcq"));
            return mc;
        }
        private RipristinoModel IconvRipristinoModel(IContent r) {
            RipristinoModel rps = new RipristinoModel();
            rps.Id = r.Id;
            rps.Name = r.Name;
            rps.Descrizione = r.GetValue<string>("descrizione");
            object p = r.GetValue<int>("prezzo");
            if(p != null)
                rps.Prezzo = r.GetValue<int>("prezzo");
            
            return rps;
        }
        private ImmaginiCarModel IconvImmaginiCarModel(IContent i)
        {
            var ms = Services.MediaService;
            ImmaginiCarModel img = new ImmaginiCarModel();
            img.Name = i.Name;
            img.Id = i.Id;
            img.Path = ms.GetById(i.GetValue<int>("immagine")).GetValue<string>("umbracoFile");
            return img;
        }
        private ClienteModel IconvClienteModel(IContent c)
        {
            ClienteModel cli = new ClienteModel();
            cli.Id = c.Id;
            cli.Name = c.Name;
            cli.Nome = c.GetValue<string>("nome");
            cli.Cognome = c.GetValue<string>("cognome");
            cli.Indirizzo = c.GetValue<string>("indirizzo");
            cli.Note = c.GetValue<string>("note");
            cli.Piva = c.GetValue<string>("piva");
            cli.Provincia = c.GetValue<string>("provincia");
            cli.Referente = c.GetValue<string>("referente");
            cli.Rs = c.GetValue<string>("rs");
            cli.Telefono = c.GetValue<string>("telefono");
            cli.Azienda = c.GetValue<bool>("azienda");
            cli.Cap = c.GetValue<string>("cap");
            cli.Cellulare = c.GetValue<string>("cellulare");
            cli.Citta = c.GetValue<string>("citta");
            cli.Email = c.GetValue<string>("email");
            cli.AutoAcq = getAutoAcqCli(c.Id);
            cli.AutoVen = getAutoVenCli(c.Id);
            return cli;
        }
        
        private int getDealersFolderId()
        {
            return 1069;
        }
        private int getStockFolderID()
        {
            var dealer = getDealer();
            var q = (from a in dealer.Descendants()
                    where a.ContentType.Alias == "Stock"
                    select a).First().Id;
            return q;
        }
        private int getDealerFolderID()
        {
            var dealer = getDealer();
            var q = (from a in dealer.Descendants()
                     where a.ContentType.Alias == "Dealer"
                     select a).First().Id;
            return q;
        }
        private int getClientiFolderID()
        {
            var dealer = getDealer();
            var q = (from a in dealer.Descendants()
                    where a.ContentType.Alias == "Clienti"
                    select a).First().Id;
            return q;
        }
        
        #endregion
    }
}