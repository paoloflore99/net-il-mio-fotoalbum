using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Migrations;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;
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
        
        [HttpGet]
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
       

        [HttpPost]
        public IActionResult Create(FotoCategorieModel model)
        {
            if (!ModelState.IsValid)
            {
                using FotoDbContext db = new FotoDbContext();
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
                return View("Create" , model);
            }

            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = new Foto();
                model.Foto.Categorielist = new List<Categorie>();
                if (ModelState.IsValid != null)
                {
                    foreach (string categori in model.Categorias)
                    {
                        Categorie categg = db.Categorie.FirstOrDefault(i => i.Id.ToString() == categori );
                        if(categg != null)
                        {
                            model.Foto.Categorielist.Add(categg);
                        }
                    }
                }
                db.Foto.Add(model.Foto);
                db.SaveChanges();
            }
            return View("Index");
        }




        [HttpGet]
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
                    model.Foto = foto;
                    foreach (var categ in categgoria)
                    {
                        selectList.Add(new SelectListItem()
                        {
                            Text = categ.Name,
                            Value = categ.Id.ToString()
                        });
                        
                    }
                    model.Categoria = selectList;
                    return View(model);
                }
            }
        }


        [HttpPost]
        public IActionResult Update(int id, FotoCategorieModel d)
        {
            if (!ModelState.IsValid)
            {

                List<Categorie> categoria = FotoManager.GetAllCategorie();
                List<SelectListItem> selectList = new List<SelectListItem>();
                FotoCategorieModel model = new FotoCategorieModel();
                

            }

                return View();
        }






            public IActionResult Delite()
        {
            return null;
        }

    }
}
