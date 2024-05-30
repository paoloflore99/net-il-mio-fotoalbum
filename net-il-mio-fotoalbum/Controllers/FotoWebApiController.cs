using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotoWebApiController : ControllerBase
    {


        public IActionResult Index()
        {
            return Ok(FotoManager.GetAllFoto());
        }
        //Detaglio
        public IActionResult PerID(int Id)
        {
            return Ok(FotoManager.Detaglioma(Id));
        }

        [HttpPut("Titolo")]
        public IActionResult Filtraggio(string titolo)
        {
            var foto = FotoManager.PrendiPerTitolo(titolo);
            if(foto == null)
            {
                return NotFound();
            }
            return Ok(foto);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            FotoCategorieModel model = new FotoCategorieModel();
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
            model.imgFi();
            model.Foto = new Foto();
            model.Categoria = selectList;
            return Ok( model);
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
        public IActionResult Create([FromBody] FotoCategorieModel model)
        {
            if (!ModelState.IsValid)
            {
                PopolaCategorie(model);
                return Ok(model);
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
                    FotoCategorieModel model = new FotoCategorieModel();

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
                    return Ok(model);
                }
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, FotoCategorieModel d)
        {
            if (!ModelState.IsValid)
            {


                PopolaCategorie(d);
                return Ok( id);

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

                    foto.Titolo = d.Foto.Titolo;
                    foto.Descrizione = d.Foto.Descrizione;
                    foto.Visibile = d.Foto.Visibile;
                    db.SaveChanges();
                    return Ok("index");
                }
                else
                {
                    return NotFound();
                }
            }
        }





        [HttpDelete("{id}")]
        public IActionResult Delite(int id)
        {
            using (FotoDbContext db = new FotoDbContext())
            {
                Foto foto = db.Foto.Where(foto => foto.Id == id).Include(i => i.Categorielist).FirstOrDefault();
                if (foto != null)
                {
                    db.Foto.Remove(foto);
                    db.SaveChanges();
                    return Ok("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
