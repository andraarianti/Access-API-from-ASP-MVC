using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesControllers : ControllerBase
    {
        private readonly IRoleBLL _roleBLL;
        public RolesControllers(IRoleBLL roleBLL)
        {
            roleBLL = _roleBLL;
        }

        //GET ALL
        [HttpGet]
        public Task<IEnumerable<RoleDTO>> GetAll()
        {
            var result = _roleBLL.GetAllRoles();
            return result;
        }


    }
}
