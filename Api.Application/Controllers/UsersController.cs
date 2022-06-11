using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")] //  rota de url
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid) // se não for valido devolve uma BAdRequest
            {
                return BadRequest(ModelState); // 400 Bad Request - solicitação invalida
            }

            try
            {
                return Ok(await service.GetAll()); // codigo 200
            }
            catch (ArgumentException e) // tratamento de erros de controller
            {
                // utiliza o status code do httl para ter um erro interno e vai subir a
                // mensagem para quem chamou o codigo, com o codigo 500
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
            }
        }
    }
}
