using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Migrations;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace net_il_mio_fotoalbum.Controllers
{
    public class HomeController : Controller
    {
        /*
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */

        public IActionResult Index()
        {
            return View(FotoManager.GetAllFoto());
        }
        //Detaglio
        public IActionResult PerID(int Id)
        {
            return View(FotoManager.Detaglioma(Id));
        }

        [HttpGet]
        [Authorize(Roles ="ADMIN")]
        public IActionResult Create()
        {
            FotoCategorieModel model = new FotoCategorieModel();
            List<Categorie> categgoria = FotoManager.GetAllCategorie();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach(var categ in categgoria)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = categ.Name,
                    Value = categ.Id.ToString()
                });
            }
            model.Foto = new Foto();
            model.Categoria = selectList;
            return View("Create" ,model);
        }
       
        private static void PopolaCategorie(FotoCategorieModel model)
        {
            List<Categorie> categgoria = FotoManager.GetAllCategorie();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var categ in categgoria)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = categ.Name,
                    Value = categ.Id.ToString()
                });
            }
            model.Categoria = selectList;
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(FotoCategorieModel model)
        {
            if (!ModelState.IsValid)
            {
                PopolaCategorie(model);
                return View("Create" , model);
            }
            FotoManager.Crea(model);
            return RedirectToAction("Index");
        }




        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = db.Foto.Where(foto => foto.Id == id).FirstOrDefault();
                if (foto == null)
                {
                    return NotFound();
                }
                else
                {
                    
                    List<Categorie> categgoria = FotoManager.GetAllCategorie();
                    List<SelectListItem> selectList = new List<SelectListItem>();
                    FotoCategorieModel model= new FotoCategorieModel();
                   
                    foreach (var categ in categgoria)
                    {
                        selectList.Add(new SelectListItem()
                        {
                            Text = categ.Name,
                            Value = categ.Id.ToString()
                        });
                        
                    }
                    model.Foto = foto;
                    model.Categoria = selectList;
                    return View(model);
                }
            }
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, FotoCategorieModel d)
        {
            if (!ModelState.IsValid)
            {

                
                PopolaCategorie(d);
                return View("Update", id);

            }
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = db.Foto.Where(foto => foto.Id == id).Include(i => i.Categorielist).FirstOrDefault();
                foto.Categorielist.Clear();
                if (foto != null)
                {
                    if (d.Categorias != null)
                    {
                        foreach (string selezione in d.Categorias)
                        {
                            int selezionacategoria = int.Parse(selezione);
                            Categorie categorie = db.Categorie.Where(x => x.Id == selezionacategoria).FirstOrDefault();
                            foto.Categorielist.Add(categorie);
                        }
                    }

                    foto.Titolo =d.Foto.Titolo;
                    foto.Descrizione = d.Foto.Descrizione;
                    foto.Visibile = d.Foto.Visibile;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
                else
                {
                    return NotFound(); 
                }
            }
        }






        public IActionResult Delite(int id)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = db.Foto.Where(foto => foto.Id == id).Include(i => i.Categorielist).FirstOrDefault();
                if (foto != null)
                {
                    db.Foto.Remove(foto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

        }

    }
}
