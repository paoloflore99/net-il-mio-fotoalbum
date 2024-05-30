using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        public static Foto Update(int id)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                return null;
            }
        }


        public static  void Crea( FotoCategorieModel model)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = new Foto();
                model.Foto.Categorielist = new List<Categorie>();

                foreach (string categori in model.Categorias)
                {
                    Categorie categg = db.Categorie.FirstOrDefault(i => i.Id.ToString() == categori);
                    if (categg != null)
                    {
                        model.Foto.Categorielist.Add(categg);
                    }
                }
                
                db.Foto.Add(model.Foto);
                db.SaveChanges();
            }
        }


        public static Foto Detaglioma(int id, bool categorie = true)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                if (!categorie)
                {
                    return db.Foto.FirstOrDefault(x => x.Id == id);
                }
                else
                {
                    var query = db.Foto.Where(x => x.Id == id);
                    if (categorie)
                    {
                        query = query.Include(p => p.Categorielist);
                    }
                    return query.FirstOrDefault();
                }
            }
        }

        public static Foto PrendiPerTitolo(string titolo)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                return db.Foto.FirstOrDefault(t => t.Titolo == titolo);
            }
        }

    }

    
}


