using ImenikApp.Data;
using ImenikApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace ImenikApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetom kontakt u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KontaktController : ControllerBase
    {

        // Dependency injection u controller
        // https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio#dependency-injection
        private readonly ImenikContext _context;

        public KontaktController(ImenikContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve kontakte iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Kontakt
        ///
        /// </remarks>
        /// <returns>Kontakti u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]
        public IActionResult Get()
        {
            // kontrola ukoliko upit nije dobar
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var kontakti = _context.Kontakt.ToList();
                if (kontakti == null || kontakti.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Kontakt.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                    ex.Message);
            }



        }


        /// <summary>
        /// Dodaje kontakt u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Kontakt
        ///    {ime:"",prezime:""}
        ///
        /// </remarks>
        /// <returns>Kreirani kontakt u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]
        public IActionResult Post(Kontakt kontakt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Kontakt.Add(kontakt);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, kontakt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                   ex.Message);
            }



        }




        /// <summary>
        /// Mijenja podatke postojećeg kontakta u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/kontakt/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "ime": "Novo ime",
        ///  "prezime":"Novo prezime",
        ///  "broj": ,
        ///  "adresa": ,
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra kontakta koji se mijenja</param>  
        /// <returns>Svi poslani podaci od kontakta</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi kontakta kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Kontakt kontakt)
        {

            if (sifra <= 0 || kontakt == null)
            {
                return BadRequest();
            }

            try
            {
                var kontaktBaza = _context.Kontakt.Find(sifra);
                if (kontaktBaza == null)
                {
                    return BadRequest();
                }
                // inače se rade Mapper-i
                // mi ćemo za sada ručno
                kontaktBaza.Ime = kontakt.Ime;
                kontaktBaza.Prezime = kontakt.Prezime;
                kontaktBaza.Broj = kontakt.Broj;
                kontaktBaza.Adresa = kontakt.Adresa;


                _context.Kontakt.Update(kontaktBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, kontaktBaza);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  ex); // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
            }

        }


        /// <summary>
        /// Briše kontakt iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/kontakt/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra kontakta koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi kontakta kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest();
            }

            var kontaktBaza = _context.Kontakt.Find(sifra);
            if (kontaktBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Kontakt.Remove(kontaktBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {

                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");

            }
        }
    }
}