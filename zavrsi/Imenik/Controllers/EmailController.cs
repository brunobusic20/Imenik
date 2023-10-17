using ImenikApp.Data;
using ImenikApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace ImenikApp.Controllers
{
    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetom email u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {

        // Dependency injection u controller
        // https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio#dependency-injection
        private readonly ImenikContext _context;

        public EmailController(ImenikContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve emailove iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Email
        ///
        /// </remarks>
        /// <returns>Email u bazi</returns>
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
                var emaili = _context.Email.ToList();
                if (emaili == null || emaili.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Email.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                    ex.Message);
            }



        }


        /// <summary>
        /// Dodaje smjer u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Email
        ///    {emailadresa:""}
        ///
        /// </remarks>
        /// <returns>Kreirani email u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]
        public IActionResult Post(Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Email.Add(email);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, email);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                   ex.Message);
            }



        }




        /// <summary>
        /// Mijenja podatke postojećeg email-a u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/email/1
        ///
        /// {
        ///  emailadresa("")
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra email-a koji se mijenja</param>  
        /// <returns>Svi poslani podaci od email-a</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi email-a kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Email email)
        {

            if (sifra <= 0 || email == null)
            {
                return BadRequest();
            }

            try
            {
                var emailBaza = _context.Email.Find(sifra);
                if (emailBaza == null)
                {
                    return BadRequest();
                }
                // inače se rade Mapper-i
                // mi ćemo za sada ručno
                emailBaza.EmailAdresa = email.EmailAdresa;



                _context.Email.Update(emailBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, emailBaza);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  ex); // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
            }

        }


        /// <summary>
        /// Briše email iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/email/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra emaila koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi email-a kojeg želimo obrisati</response>
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