using AutoMapper;
using Maxus.Application.DTOs.CustomerFeedbackOption;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class CustomerFeedbackOptionService : ICustomerFeedbackOptionService
    {
        private readonly ICustomerFeedbackOptionRepository _customerFeedbackOptionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomerFeedbackOptionService(ICustomerFeedbackOptionRepository customerFeedbackOptionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._customerFeedbackOptionRepository = customerFeedbackOptionRepository;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<CustomerFeedbackOptionByIdResponse> CreateAsync(CreateCustomerFeedbackOptionRequest request)
        {
            try
            {
                var customerFeedbackOption = new tbl_CustomerCheklistOption
                {
                    Name = request.Name,
                    CompanyId = request.CompanyId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId(),
                    IsDefault = request.IsDefault
                };
                var createdCustomerFeedbackOption = await _customerFeedbackOptionRepository.CreateAsync(customerFeedbackOption);
                return _mapper.Map<CustomerFeedbackOptionByIdResponse>(createdCustomerFeedbackOption);
            }
            catch (Exception ex)
            {
                throw new Exception("Create Customer Feedback Option Error In Service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _customerFeedbackOptionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Customer Feedback Option Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<CustomerFeedbackOptionListResponse>)> GetAllAsync(CustomerFeedbackOptionListRequest request)
        {
            try
            {
                var (paginationResponse, customerFeedbackOptions) = await _customerFeedbackOptionRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm , request.SearchColumn);
                var mappedCustomerFeedbackOptions = _mapper.Map<IEnumerable<CustomerFeedbackOptionListResponse>>(customerFeedbackOptions);
                return (paginationResponse, mappedCustomerFeedbackOptions);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Customer Feedback Options Error In Service.", ex);
            }
        }

        public async Task<CustomerFeedbackOptionByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var customerFeedbackOption = await _customerFeedbackOptionRepository.GetByIdAsync(id);
                return _mapper.Map<CustomerFeedbackOptionByIdResponse>(customerFeedbackOption);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Customer Feedback Option Error In Service.", ex);
            }
        }

        public async Task<CustomerFeedbackOptionByIdResponse> UpdateAsync(int id, UpdateCustomerFeedbackOptionRequest request)
        {
            try
            {
                var customerFeedbackOption = await _customerFeedbackOptionRepository.GetByIdAsync(id);
                if (customerFeedbackOption != null)
                {
                    customerFeedbackOption.UpdatedAt = DateTime.Now;
                    customerFeedbackOption.UpdatedBy = CurrentUserId();
                    customerFeedbackOption.Name = request.Name;
                    customerFeedbackOption.IsDefault = request.IsDefault;
                    customerFeedbackOption.CompanyId = request.CompanyId;
                    var success = await _customerFeedbackOptionRepository.UpdateAsync(customerFeedbackOption);
                    if (success)
                    {
                        return _mapper.Map<CustomerFeedbackOptionByIdResponse>(customerFeedbackOption);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Customer Feedback Option Error In Service.", ex);
            }
        }
    }
}
