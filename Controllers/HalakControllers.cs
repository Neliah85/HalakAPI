using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalakAPI.Models;
using HalakAPI.DTOs;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HalakControllers : ControllerBase
    {
        [HttpGet("FajMeretTo")]
        public IActionResult GetFajMeretTo()
        {
            try
            {
                using (var cx = new HalakContext())
                {
                    var response = cx.Halaks
                        .Select(x => new
                        {
                            x.Faj,
                            x.MeretCm,
                            x.To.Nev
                        })
                        .ToList();
                    return Ok(response);
                }

                /* VAAGY
                 var response = cx.Halaks.Include(x => x.To).Select(f=>new FajMeretTo{Faj=f.Faj,
                     MeretCm=f.MeretCm,
                     ToNev=f.To.Nev
                 }).ToList();
                    return Ok(response);                     
                 */
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }

        [HttpPost]

        public IActionResult PostHalak(string uid, Halak hal)
        {
            try
            {
                if (Program.UID == uid)
                    return StatusCode(401);
                using (var cx = new HalakContext())
                {
                    cx.Halaks.Add(hal);
                    cx.SaveChanges();
                    return Ok("Hal hozzáadása sikeresen megtörtént.");
                }
            }
            catch (Exception)
            {
                return StatusCode(401, "Nincs jogosultsága új hal felvételéhez!");
            }
        }

        [HttpPut("Halak")]
        public async Task<IActionResult> Put(Halak hal)
        {
            try
            {
                using (var cx = new HalakContext())
                {
                    var response = cx.Halaks.FirstOrDefault(x => x.Id == hal.Id);
                    if (response != null)
                    {
                        cx.Halaks.Update(hal);
                        await cx.SaveChangesAsync();
                        return Ok("Hal módosítása sikeresen megtörtént.");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen azonosítójú hal!");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "Nincs azonosítójú hal!");
            }
        }

        [HttpDelete()]
        public IActionResult DelById(int id)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var hal = context.Halaks.Find(id);
                    if (hal == null)
                    {
                        return NotFound("Nincs ilyen azonosítójú hal.");
                    }
                    context.Halaks.Remove(hal);
                    context.SaveChanges();
                    return Ok("Sikeres törlés.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
        }
    }
}
