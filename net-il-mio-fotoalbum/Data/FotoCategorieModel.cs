using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Data
{
    public class FotoCategorieModel
    {
        public Foto Foto { get; set; }
        public List<SelectListItem>? Categoria { get; set; }
        public List<string>? Categorias { get; set; }
        public IFormFile? ImgFile { get; set; }
        public FotoCategorieModel() { }
        public byte[] SetFileImg()
        {
            if (ImgFile == null)
            {
                return null;
            }

            return null;
        }

        public byte[] imgFi()
        {
            if (ImgFile == null)
            {
                return null;
            } 
            using var stream = new MemoryStream();
            this.ImgFile?.CopyTo(stream);
            Foto.Imaggine = stream.ToArray();
            return Foto.Imaggine;
        }

    }
}
