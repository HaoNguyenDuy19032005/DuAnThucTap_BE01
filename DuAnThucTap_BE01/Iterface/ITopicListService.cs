using DuAnThucTap_BE01.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITopicListService
    {
        Task<IEnumerable<TopicListDto>> GetAllAsync();
        Task<TopicListDto?> GetByIdAsync(Guid id);
        Task<TopicListDto> CreateAsync(TopicListDto topicList);
        Task<TopicListDto?> UpdateAsync(Guid id, TopicListDto topicList);
        Task<bool> DeleteAsync(Guid id);
    }
}