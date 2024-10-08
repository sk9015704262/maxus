using AccountingAPI.Responses;
using Maxus.Application.DTOs.CustomerFeedbackReport;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Maxus.Application.DTOs.dashboard;
using System.Globalization;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  

    public class MobileApiController : ControllerBase
    {
        private readonly IVisitReportService _visitReportService;
        private readonly IDashBoardService dashBoardService;
        private readonly ITrainingReportService _trainingReportService;
        private readonly ICustomerFeedbackReportService _customerFeedbackReportService;
        private readonly IMOMReportService _mOMReportService;

        public MobileApiController(IVisitReportService visitReportService , IDashBoardService dashBoardService, ITrainingReportService  trainingReportService, ICustomerFeedbackReportService customerFeedbackReportService , IMOMReportService mOMReportService)
        {
            _visitReportService = visitReportService;
            this.dashBoardService = dashBoardService;
            _trainingReportService = trainingReportService;
            _customerFeedbackReportService = customerFeedbackReportService;
            _mOMReportService = mOMReportService;

        }

        [HttpPost]
        public async Task<IActionResult> GetDashBoardData(DashBoardListRequest request)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (string.IsNullOrEmpty(request.Date))
            {
                // If date is empty, set fromDate and toDate to null
                fromDate = null;
                toDate = null;
            }
            else
            {
                if (!DateTime.TryParseExact(request.Date, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return BadRequest("Invalid date format. Please use MM/yyyy.");
                }

                int year = parsedDate.Year;
                int month = parsedDate.Month;
                fromDate = new DateTime(year, month, 1);
                toDate = fromDate.Value.AddMonths(1).AddDays(-1);
            }

            
            try
            {
                var GetMOMReportRequest = new GetMOMReportRequest
                {
                    PageNumber = 1,
                    PageSize = 5,
                    SortBy = 0,
                    SortDir = "Desc",
                    SearchTerm = "",
                    CompanyId = request.CompanyId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    IsDraft = request.IsDraft,
                    SearchColumn = 0

                };

                var TrainingReportListRequest = new TrainingReportListRequest
                {
                    PageNumber = 1,
                    PageSize = 5,
                    SortBy = 0,
                    SortDir = "Desc",
                    SearchTerm = "",
                    CompanyId = request.CompanyId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    IsDraft = request.IsDraft

                };

                var CustomerFeedbackReportListRequest = new CustomerFeedbackReportListRequest
                {
                    PageNumber = 1,
                    PageSize = 5,
                    SortBy = 0,
                    SortDir = "Desc",
                    SearchTerm = "",
                    CompanyId = request.CompanyId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    IsDraft = request.IsDraft

                };

                var VisitReportListRequest = new VisitReportListRequest
                {
                    PageNumber = 1,
                    PageSize = 5,
                    SortBy = 0,
                    SortDir = "Desc",
                    SearchTerm = "",
                    CompanyId = request.CompanyId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    IsDraft = request.IsDraft
                };

                var Count = await dashBoardService.Getcount(request.CompanyId);


                var (paginationResponse1, MOMReports) = await _mOMReportService.GetAllAsync(GetMOMReportRequest);

                var (paginationResponse2, TraningReport) = await _trainingReportService.GetAllAsync(TrainingReportListRequest);

                var (paginationResponse3, CustomerFeedbackReport) = await _customerFeedbackReportService.GetAllAsync(CustomerFeedbackReportListRequest);

                var (paginationResponse4, VisitReport) = await _visitReportService.GetAllAsync(VisitReportListRequest);

                var MomReports = new
                {
                    Reports = MOMReports,
                    TotalReport = Count.MomReports
                };

                var TraningReports = new 
                {
                    Reports = TraningReport,
                    TotalReport = Count.TrainingReports
                };

                var CustomerFeedbackReports = new
                {
                    Reports = CustomerFeedbackReport,
                    TotalReport = Count.CustomerFeedbackReports
                };

                var VisitReports = new
                {
                    Reports = VisitReport,
                    TotalReport = Count.VisitReports
                };

                return Ok(new ApiResponse<object>(new { MomReports, TraningReports, CustomerFeedbackReports  , VisitReports }, "Report fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
