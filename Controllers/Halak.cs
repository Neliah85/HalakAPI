using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalakAPI.Models;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Halak : ControllerBase
    {

        [HttpGet("FajMeretTo")]

        public IActionResult GetHalakFajMeretTo()
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var halak = context.Halaks.ToList();
                    return Ok(context.Halaks.Select(k => new
                    {
                        k.Faj,
                        k.MeretCm,
                        k.To,
                    }).ToList());
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
        }
        [HttpPut()]
        public IActionResult Put()
        {
           
            using var context = new HalakContext();
            try
            {
                
                var hal = context.Halaks.FirstOrDefault();
                if (hal == null)
                {
                    return NotFound("Nincs ilyen azonosítójú hal!");
                }                               
              
                context.SaveChanges();

                return Ok("Sikeres módosítás");
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
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
