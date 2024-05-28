using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Data
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Foto> FotoList { get; set; }

        public Categorie() { }
    }
}
