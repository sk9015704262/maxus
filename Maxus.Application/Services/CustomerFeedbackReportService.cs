using AutoMapper;
using Maxus.Application.DTOs.CustomerFeedbackReport;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Maxus.Application.Services
{
    public class CustomerFeedbackReportService : ICustomerFeedbackReportService
    {
        private readonly IConfiguration _configuration;
        private string ImagePath;
        private readonly ICustomerFeedbackReportRepository _customerFeedbackReportRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomerFeedbackReportService(ICustomerFeedbackReportRepository customerFeedbackReportRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            ImagePath = _configuration["ImageBasePath"];
            _customerFeedbackReportRepository = customerFeedbackReportRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<int> CreateCustomerFeedbackReport(CreateCustomerFeedbackReportRequest request)
        {
            try
            {
                if (request.Id != 0)
                {
                    bool IsUpdate = await UpdateAsync(request.Id, request);
                    if (IsUpdate == true)
                    {
                        return 1;

                    }
                    return 0;
                }
                else
                {

                    var CustomerFeedback = new tbl_CustomerFeedbackReport
                    {
                        SiteId = request.SiteId,
                        Remark = request.Remark,
                        Date = DateTime.Now,
                        FeedbackCheckList = request.FeedbackCheckList,
                        clientRepresentatives = request.clientRepresentatives,
                        IsDraft = request.IsDraft,
                        CreatedBy = CurrentUserId(),
                        CreatedAt = request.CreatedAt,
                        Status = request.Status,
                        ClientSignature = request.ClientSignature,
                        ManagerSignature = request.ManagerSignature,
                        Attachment = request.Attachment
                    };

                    var createdCustomerFeedback = await _customerFeedbackReportRepository.CreateAsync(CustomerFeedback);
                    if (createdCustomerFeedback != null)
                    {
                        return 2;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Create CustomerFeedback Report Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<CustomerFeedbackReportListResponse>)> GetAllAsync(CustomerFeedbackReportListRequest request)
        {
            try
            {
                var (paginationResponse, CustomerFeedback) = await _customerFeedbackReportRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm, request.CompanyId, request.FromDate, request.ToDate, request.IsDraft, request.SearchColumn);
                return (paginationResponse, _mapper.Map<IEnumerable<CustomerFeedbackReportListResponse>>(CustomerFeedback));

            }
            catch (Exception ex)
            {
                throw new Exception("GetAll CustomerFeedback Reports Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<CustomerFeedbackReportListResponse>?> GetFeedbackByCompanyIdAsync(GetCustomerFeedbackReportByCompanyRequest request)
        {
            try
            {
                var CustomerFeedback = await _customerFeedbackReportRepository.GetFeedbackByComanyIdAsync(request.UserId, request.CompanyId);
                return _mapper.Map<IEnumerable<CustomerFeedbackReportListResponse>>(CustomerFeedback);

            }
            catch (Exception ex)
            {
                throw new Exception("GetAll CustomerFeedback Reports Error In Service.", ex);
            }
        }

        public async Task<CustomerFeedbackReportByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var Report = await _customerFeedbackReportRepository.GetByIdAsync(id);
                if (Report != null)
                {

                    Report.ClientSignature = new Uri(new Uri(ImagePath), Report.ClientSignature).ToString();
                    Report.ManagerSignature = new Uri(new Uri(ImagePath), Report.ManagerSignature).ToString();

                    if (Report.AttchmentPath != null)
                    {

                        foreach (var item in Report.AttchmentPath)
                        {
                            if (item.AttachmentPath != null)
                            {
                                item.AttachmentPath = new Uri(new Uri(ImagePath), item.AttachmentPath).ToString();

                            }
                        }


                    }
                }

                var Feedback = _mapper.Map<CustomerFeedbackReportByIdResponse>(Report);
                foreach (var item in Feedback.CheckLists)
                {
                    item.Options = Report.CheckListOptions.Where(c => c.CustomerFeedbackId == item.Id).ToList();
                }
                return Feedback;

            }
            catch (Exception ex)
            {
                throw new Exception("Get Report Error In Service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, CreateCustomerFeedbackReportRequest request)
        {
            try
            {
                var CustomerFeedback = new tbl_CustomerFeedbackReport
                {
                    Id = id,
                    SiteId = request.SiteId,
                    Remark = request.Remark,
                    Date = DateTime.Now,
                    FeedbackCheckList = request.FeedbackCheckList,
                    clientRepresentatives = request.clientRepresentatives,
                    IsDraft = request.IsDraft,
                    UpdatedBy = CurrentUserId(),
                    UpdatedAt = DateTime.Now,
                    CreatedAt = request.CreatedAt,
                    Status = request.Status,
                    ClientSignature = request.ClientSignature,
                    ManagerSignature = request.ManagerSignature,
                    Attachment = request.Attachment
                };

                var createdCustomerFeedback = await _customerFeedbackReportRepository.UpdateAsync(CustomerFeedback);
                if (createdCustomerFeedback == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Create CustomerFeedback Report Error In Service.", ex);
            }
        }
    }
}
