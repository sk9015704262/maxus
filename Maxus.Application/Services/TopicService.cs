using AutoMapper;
using Maxus.Application.DTOs.Topic;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public TopicService(ITopicRepository topicRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<TopicByIdResponse> CreateAsync(CreateTopicRequest obj)
        {
            try
            {
                var topic = new tbl_TopicsMaster
                {
                    CompanyId = obj.CompanyId,
                    CreatedBy = CurrentUserId(),
                    IndustrySegmentId = obj.IndustrySegmentId,
                    Name = obj.Name,
                    Description = obj.Description,
                    CreatedAt = DateTime.Now
                };

                var createdTopic = await _topicRepository.CreateAsync(topic);
                return _mapper.Map<TopicByIdResponse>(createdTopic);
            }
            catch (Exception ex)
            {
                throw new Exception("Create Topic Error In Service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _topicRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Topic Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<TopicListResponse>)> GetAllAsync(TopicListRequest obj)
        {
            try
            {
                var (paginationResponse, topics) = await _topicRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn);
                return (paginationResponse, _mapper.Map<IEnumerable<TopicListResponse>>(topics));
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Topic Error In Service.", ex);
            }
        }

        public async Task<TopicByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var topic = await _topicRepository.GetByIdAsync(id);
                return _mapper.Map<TopicByIdResponse>(topic);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Topic Error In Service.", ex);
            }
        }

        public async Task<TopicByIdResponse> UpdateAsync(int id, UpdateTopicRequest request)
        {
            try
            {
                var topic = await _topicRepository.GetByIdAsync(id);
                if (topic != null)
                {
                    topic.UpdatedAt = DateTime.Now;
                    topic.UpdatedBy = CurrentUserId();
                    topic.Name = request.Name;
                    topic.Description = request.Description;
                    topic.CompanyId = request.CompanyId;
                    topic.IndustrySegmentId = request.IndustrySegmentId;
                    var success = await _topicRepository.UpdateAsync(topic);
                    if (success)
                    {
                        return _mapper.Map<TopicByIdResponse>(topic);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Topic Error In Service.", ex);
            }
        }
    }
}
