using Maxus.Application.DTOs.Topic;

namespace Maxus.Application.Interfaces
{
    public interface ITopicService : IBaseService<TopicListRequest, TopicListResponse, TopicByIdResponse, CreateTopicRequest, UpdateTopicRequest>
    { }
}
