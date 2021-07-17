using AutoMapper;
using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Repos;
using CustomIdentityWebApi.Res.RoleRes;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepo roleRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RolesController(IRoleRepo roleRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.roleRepo = roleRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public async Task<List<SelectRoleRes>> Get()
        {
            return await roleRepo.GetRolesAsync();
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            Role role = await roleRepo.GetRoleAsync(id);

            if (role is null)
                return NotFound();

            return Ok(mapper.Map<SelectRoleRes>(role));
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<SelectRoleRes> PostAsync([FromBody] SaveRoleRes res)
        {
            Role role = await roleRepo.CreateRoleAsync(res);

            await unitOfWork.CompleteAsync();

            return mapper.Map<SelectRoleRes>(role);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<SelectRoleRes> Put(long id, [FromBody] SaveRoleRes res)
        {
            Role role = await roleRepo.UpdateRoleAsync(id,res);

            await unitOfWork.CompleteAsync();

            return mapper.Map<SelectRoleRes>(role);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            Role role = await roleRepo.GetRoleAsync(id);

            if (role is null)
                return NotFound();

            role = roleRepo.RemoveRole(role);

            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<SelectRoleRes>(role));
        }
    }
}
