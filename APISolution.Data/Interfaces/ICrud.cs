using System.Collections.Generic;

namespace APISolution.Domain;

public interface ICrud<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task<T> Insert(T entity);
	Task<T> Update(int id, T entity);
    Task<bool> Delete(int id);
}
