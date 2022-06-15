using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")] //  rota de url
    [ApiController]//Determina que será uma API e não um MVC
    //determinando como é chamada os metodos (http://localhost:5000/api/users)  
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                //Verificação se a entrada do usuário é válida para o método
                return BadRequest(ModelState); //400 solicitação inválida
            }
            try
            {
                return Ok(await _service.GetAll()); //Retorno da Interface, injetada no método
            }
            catch (ArgumentException e) //ArgumentException é usado para tratar Controllers
            {
                //Enviando o erro para o usuário, visto ser a última camada do sistema
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //http://localhost:5000/api/users/513513521138513513581
        [HttpGet]
        [Route("{id}", Name = "GetWithId")] // nomeando uma rota
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 solicitação inválida
            }
            try
            {
                return Ok(await _service.Get(id));
            }
            catch(ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 solicitação inválida
            }
            try
            {
                var result = await _service.Post(user);
                if(result != null)
                {
                    return Created(new Uri(Url.Link("GetWithId", new {id = result.Id} )), result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
