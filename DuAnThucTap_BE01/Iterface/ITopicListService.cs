using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITopicListService
    {
        Task<IEnumerable<Topiclist>> GetAllAsync();
        Task<Topiclist?> GetByIdAsync(int id);
        Task<Topiclist> CreateAsync(Topiclist topicList);
        Task<Topiclist?> UpdateAsync(int id, Topiclist topicList);
        Task<bool> DeleteAsync(int id);
    }
}