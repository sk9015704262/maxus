using AutoMapper;
using Maxus.Application.DTOs.CustomerFeedback;
using Maxus.Application.DTOs.CustomerFeedbackOption;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly ICustomerFeedBackRepository _customerFeedbackRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomerFeedbackService(ICustomerFeedBackRepository customerFeedbackRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._customerFeedbackRepository = customerFeedbackRepository;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<CustomerFeedbackByIdResponse> CreateAsync(CreateCustomerFeedbackRequest request)
        {
            try
            {
                var customerFeedback = new tbl_CustomerFeedbackMaster
                {
                    Description = request.Description,
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId(),
                    IndustrySegmentId = request.IndustrySegmentId,
                    CompanyId = request.CompanyId,
                    IsMandatory = request.IsMandatory,
                    ChekListOption = request.ChekListOption
                };
                var createdCustomerFeedback = await _customerFeedbackRepository.CreateAsync(customerFeedback);
                return _mapper.Map<CustomerFeedbackByIdResponse>(createdCustomerFeedback);
            }
            catch (Exception ex)
            {
                throw new Exception("Create Customer Feedback Error In Service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _customerFeedbackRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Customer Feedback Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<CustomerFeedbackListResponse>)> GetAllAsync(CustomerFeedbackListRequest request)
        {
            try
            {
                var a = _httpContextAccessor.HttpContext.User.Identity.Name;
                var (paginationResponse, customerFeedbacks) = await _customerFeedbackRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm,request.CompanyId , request.SearchColumn);
                var mappedCustomerFeedbacks = _mapper.Map<IEnumerable<CustomerFeedbackListResponse>>(customerFeedbacks);
                return (paginationResponse, mappedCustomerFeedbacks);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Customer Feedbacks Error In Service.", ex);
            }
        }

        public async Task<CustomerFeedbackByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var customerFeedback = await _customerFeedbackRepository.GetByIdAsync(id);
                return _mapper.Map<CustomerFeedbackByIdResponse>(customerFeedback);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Customer Feedback Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<CustomerFeedbackByCompanyListResponse>> GetCustomerFeedbackByCompanyIdAsync(GetCustomerFeedbackByCompanyIdRequest obj , int UserId)
        {
            try
            {
                var customerFeedbacks = await _customerFeedbackRepository.GetCustomerFeedbackByCompanyAsync(UserId , obj.CompanyId);
                 return _mapper.Map<IEnumerable<CustomerFeedbackByCompanyListResponse>>(customerFeedbacks);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Customer Feedbacks Error In Service.", ex);
            }
        }

        public async Task<CustomerFeedbackByIdResponse> UpdateAsync(int id, UpdateCustomerFeedbackRequest request)
        {
            try
            {

                var customerFeedback = new tbl_CustomerFeedbackMaster
                {
                    Id = id,
                    Description = request.Description,
                    Name = request.Name,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId(),
                    IndustrySegmentId = request.IndustrySegmentId,
                    CompanyId = request.CompanyId,
                    IsMandatory = request.IsMandatory,
                    ChekListOption = request.ChekListOption
                };

              
                    var success = await _customerFeedbackRepository.UpdateAsync(customerFeedback);
                    if (success)
                    {
                        return _mapper.Map<CustomerFeedbackByIdResponse>(customerFeedback);
                    }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Customer Feedback Error In Service.", ex);
            }
        }
    }
}
