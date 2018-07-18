using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy2018_.NET_Homework5.Core.Abstractions
{
    public interface IService<TDto> 
        where TDto: class
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(object id);

        Task<object> AddAsync(TDto dto);

        Task UpdateAsync(object id, TDto dto);

        Task DeleteAsync(object id);
    }
}
