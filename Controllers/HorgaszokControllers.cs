using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalakAPI.Models;


namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HorgaszokControllers : ControllerBase
    {
        [HttpGet("All")]

        public IActionResult GetHorgaszok()
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var response = context.Horgaszoks.ToList();

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }

            }
        }


        [HttpGet("ById/{id}")]

        public IActionResult GetById(int id)
        {
            try
            {
                using (var context = new HalakContext())
                {
                    var horgaszok = context.Horgaszoks.Find(id);
                    if (horgaszok == null)
                    {
                        return StatusCode(404, "Nincs ilyen azonosítójú horgász!");
                    }
                    return Ok(horgaszok);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
