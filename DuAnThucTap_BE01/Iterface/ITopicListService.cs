using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITopicListService
    {
        Task<IEnumerable<TopicList>> GetAllAsync();
        Task<TopicList?> GetByIdAsync(int id);
        Task<TopicList> CreateAsync(TopicList topic);
        Task<TopicList?> UpdateAsync(int id, TopicList topic);
        Task<bool> DeleteAsync(int id);
    }
}
