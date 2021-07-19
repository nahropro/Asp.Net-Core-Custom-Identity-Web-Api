using AutoMapper;
using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Repos;
using CustomIdentityWebApi.Res.UserRes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomIdentityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;

        public UsersController(IUnitOfWork unitOfWork,IUserRepo userRepo, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRes res)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user= await userRepo.CreateUserAsync(res);

            await unitOfWork.CompleteAsync();
            await userRepo.IncludeRelatedTables(user);

            return Ok(mapper.Map<SelectUserRes>(user));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRes res)
        {
            string token =await userRepo.LoginAsync(res);

            if (String.IsNullOrWhiteSpace(token))
                return BadRequest();

            return Ok(token);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
