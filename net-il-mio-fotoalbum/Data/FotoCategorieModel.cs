using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Data
{
    public class FotoCategorieModel
    {
        public Foto Foto { get; set; }
        public List<SelectListItem>? Categoria { get; set; }
        public List<string>? Categorias { get; set; }

        public FotoCategorieModel() { }
    }
}
