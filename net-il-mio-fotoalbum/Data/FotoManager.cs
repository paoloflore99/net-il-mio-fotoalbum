using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace net_il_mio_fotoalbum.Data
{
    public class FotoManager
    {
        public static List<Foto> GetAllFoto()
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                return db.Foto.ToList();
            };
        }


        public static List<Categorie> GetAllCategorie()
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                return db.Categorie.ToList();
            };
        }

        public static Foto CreateFoto()
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = new Foto();
                
                return null;
            }
        }

        public static Foto Update(int id)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                return null;
            }
        }

    }
}
