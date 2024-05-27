using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Data
{
    public class Foto
    {
        [Key]
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string  Descrizione { get; set; }
        public bool Visibile { get; set; }
        public List<Categorie>? Categorielist { get; set; }

        public Foto() { }
        public Foto(string titolo , string descrizione ,bool visibile)
        {
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.Visibile = visibile;
        }

    }
}
