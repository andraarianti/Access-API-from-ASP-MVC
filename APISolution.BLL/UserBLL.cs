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
    public class UserBLL : IUserBLL
    {
        private readonly IUserData _userData;
        private readonly IMapper _mapper;
        public UserBLL(IUserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }      
        public async Task<Task> ChangePassword(string username, string newPassword)
        {
            try
            {
                var changePassword = await _userData.ChangePassword(username, newPassword);
                return changePassword;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Task> Delete(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var user = await _userData.GetAll();
                var userDTO = _mapper.Map<IEnumerable<UserDTO>>(user);
                return userDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllWithRoles()
        {
            try
            {
                var user = await _userData.GetAllWithRoles();
                var userDTO = _mapper.Map<IEnumerable<UserDTO>>(user);
                return userDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO> GetByUsername(string username)
        {
            try
            {
                var user = await _userData.GetByUsername(username);
                var userDTO = _mapper.Map<UserDTO>(user);
                return userDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO> GetUserWithRoles(string username)
        {
            try
            {
                var user = await _userData.GetUserWithRoles(username);
                var userDTO = _mapper.Map<UserDTO>(user); 
                return userDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Task> Insert(UserCreateDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            try
            {
                var userLogin = await _userData.Login(username, password);
                var loginDTO = _mapper.Map<UserDTO>(userLogin);
                return loginDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<UserDTO> LoginMVC(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
