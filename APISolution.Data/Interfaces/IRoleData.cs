using APISolution.Domain;

namespace APISolution.Data;

public interface IRoleData : ICrud<Role>
{
    Task<Task> AddUserToRole(string username, int roleId);
}
