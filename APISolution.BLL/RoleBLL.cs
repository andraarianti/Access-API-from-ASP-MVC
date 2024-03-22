using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using APISolution.Data;
using AutoMapper;

namespace APISolution.BLL
{

    public class RoleBLL : IRoleBLL
    {
        private readonly IRoleData _roleData;
        private readonly IMapper _mapper;
        public RoleBLL(IRoleData roleData, IMapper mapper)
        {
            _roleData = roleData;
            _mapper = mapper;
        }
        public Task<Task> AddRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Task> AddUserToRole(string username, int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            try
            {
                var roles = await _roleData.GetAll();
                var rolesDTO = _mapper.Map<IEnumerable<RoleDTO>>(roles);
                return rolesDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RoleDTO> GetRoleById(int roleId)
        {
            try
            {
                var roles = await _roleData.GetById(roleId);
                var rolesDTO = _mapper.Map<RoleDTO>(roles);
                return rolesDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
