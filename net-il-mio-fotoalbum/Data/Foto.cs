using System.ComponentModel.DataAnnotations;
using System.Composition;

namespace net_il_mio_fotoalbum.Data
{
    public class Foto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Titolo obligatorio")]
        public string Titolo { get; set; }
        [Required(ErrorMessage = "Descrizione obligatoria")]
        public string  Descrizione { get; set; }
        public bool Visibile { get; set; }
        public List<Categorie>? Categorielist { get; set; }
        public byte[]? Imaggine { get; set; }
        public string ImgScr => Imaggine != null ? $"data:image/png;base64,{Convert.ToBase64String(Imaggine)}" : "";
        public Foto() { }
        public Foto(string titolo , string descrizione ,bool visibile)
        {
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.Visibile = visibile;
        }
        //non metto nessun limiti minimo , credo che non serva in un blog di fot 
    }


}
