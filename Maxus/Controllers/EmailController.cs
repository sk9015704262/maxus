using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Maxus.Application.DTOs.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Maxus.Application.Interfaces;
using Maxus.Application.Services;
using Maxus.Application.DTOs.MOM;
using AccountingAPI.Responses;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.DTOs.Site;
using Maxus.Application.DTOs.CustomerFeedbackReport;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class EmailController : ControllerBase
    {
        private readonly IMOMReportService _momReportService;
        private readonly IConfiguration _configuration;
        private readonly IVisitReportService _visitReportService;
        private readonly ITrainingReportService _trainingReportService;
        private readonly ISiteService _siteService;
        private readonly ICustomerFeedbackReportService _customerFeedbackReportService;

        public EmailController(IMOMReportService momReportService , IConfiguration configuration , IVisitReportService visitReportService , ITrainingReportService trainingReportService , ISiteService siteService , ICustomerFeedbackReportService customerFeedbackReportService)
        {
            _momReportService = momReportService;
            _configuration = configuration;
            _visitReportService = visitReportService;
            _trainingReportService = trainingReportService;
            _siteService = siteService;
            _customerFeedbackReportService = customerFeedbackReportService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] SendReportMailRequest request)
        {
            string htmlBody = string.Empty;
            var EmailTo = string.Empty;
            var clientRepresentativeDetail = new List<ClientRepresentativeDetails>();


            if (request.ReportType == 1)
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var htmlFilePath = Path.Combine(webRootPath, "EmailTemplates", "MOMEmail.html");

                try
                {
                    GetMOMByIdResponse momReport = await _momReportService.GetByIdAsync(request.Id);

                    if(momReport == null)
                    {
                        return BadRequest(new ApiResponse<object>(null, "MOM Report Not Found"));
                    }
                    var siteId = new GetRepresentativeBySiteRequest { SiteId = momReport.SiteId };
                    var clientRepresentativeDetails = await _siteService.GetRepresentativeBySiteIdAsync(siteId);
                    foreach (var item in clientRepresentativeDetails)
                    {
                        clientRepresentativeDetail.Add(item);
                    }

                    htmlBody = await System.IO.File.ReadAllTextAsync(htmlFilePath);
                    htmlBody = htmlBody.Replace("[Client Site]", momReport.SiteName);
                    htmlBody = htmlBody.Replace("[Date]", momReport.Date);
                    htmlBody = htmlBody.Replace("[Action By]", $"<li>{momReport.ActionBy}</li>");
                    htmlBody = htmlBody.Replace("[Estimate Closure Date]", $"<li>{momReport.CloserDate}</li>");
                    htmlBody = htmlBody.Replace("[Remark]", $"<li>{momReport.Remark}</li>");
                    string[] actionablePoints = new string[]
                    {};
                    var actionablePointsHtml = string.Join(string.Empty, actionablePoints.Select(point => $"<li>{momReport.ActionablePoint}</li>"));
                    htmlBody = htmlBody.Replace("[Actionable Points]", actionablePointsHtml);
                    
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error reading HTML file: {ex.Message}");
                }
            }


            if (request.ReportType == 2)
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var htmlFilePath = Path.Combine(webRootPath, "EmailTemplates", "VisitEmail.html");

                try
                {
                    VisitReportByidResponse VisitReport = await _visitReportService.GetByIdAsync(request.Id);

                    if (VisitReport == null)
                    {
                        return BadRequest(new ApiResponse<object>(null, "Visit Report  Not Found"));
                    }
                    htmlBody = await System.IO.File.ReadAllTextAsync(htmlFilePath);
                    htmlBody = htmlBody.Replace("[Site Name]", VisitReport.SiteName);
                    htmlBody = htmlBody.Replace("[Date of Visit]", VisitReport.Date);
                    htmlBody = htmlBody.Replace("[Site Supervisor Name]", VisitReport.SiteSupervisorName);
                    var siteId = new GetRepresentativeBySiteRequest { SiteId = VisitReport.SiteId };
                    var clientRepresentativeDetails = await _siteService.GetRepresentativeBySiteIdAsync(siteId);
                    foreach (var item in clientRepresentativeDetails)
                    {
                        clientRepresentativeDetail.Add(item);
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error reading HTML file: {ex.Message}");
                }
            }


            if (request.ReportType == 3)
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var htmlFilePath = Path.Combine(webRootPath, "EmailTemplates", "TrainingEmail.html");

                try
                {
                    TrainingReportByIdResponse TrainingReport = await _trainingReportService.GetByIdAsync(request.Id);

                    if (TrainingReport == null)
                    {
                        return BadRequest(new ApiResponse<object>(null, "Training Report  Not Found"));
                    }

                    htmlBody = await System.IO.File.ReadAllTextAsync(htmlFilePath);
                    htmlBody = htmlBody.Replace("[Site Name]", TrainingReport.SiteName);
                    string topicNames = string.Join(", ", TrainingReport.TopicId.Select(t => t.TopicName));
                    htmlBody = htmlBody.Replace("[Topic Name]", topicNames);
                    htmlBody = htmlBody.Replace("[Deparment Name]", TrainingReport.Department);
                    var siteId = new GetRepresentativeBySiteRequest { SiteId = TrainingReport.SiteId };
                    var clientRepresentativeDetails = await _siteService.GetRepresentativeBySiteIdAsync(siteId);
                    foreach (var item in clientRepresentativeDetails)
                    {
                        clientRepresentativeDetail.Add(item);
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error reading HTML file: {ex.Message}");
                }
            }

            if (request.ReportType == 4)
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var htmlFilePath = Path.Combine(webRootPath, "EmailTemplates", "FeedbackReport.html");

                try
                {
                    CustomerFeedbackReportByIdResponse FeedbackReport = await _customerFeedbackReportService.GetByIdAsync(request.Id);

                    if (FeedbackReport == null)
                    {
                        return BadRequest(new ApiResponse<object>(null, "Feedback Report  Not Found"));
                    }
                    htmlBody = await System.IO.File.ReadAllTextAsync(htmlFilePath);
                    htmlBody = htmlBody.Replace("[Site Name]", FeedbackReport.SiteName);
                    var siteId = new GetRepresentativeBySiteRequest { SiteId = FeedbackReport.SiteId };
                    var clientRepresentativeDetails = await _siteService.GetRepresentativeBySiteIdAsync(siteId);
                    foreach (var item in clientRepresentativeDetails)
                    {
                        clientRepresentativeDetail.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error reading HTML file: {ex.Message}");
                }

            }

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Email:FromAddress"]));
            foreach (var representative in clientRepresentativeDetail)
            {
                if (!string.IsNullOrWhiteSpace(representative.EmailTo)) 
                {
                    message.To.Add(MailboxAddress.Parse(representative.EmailTo));
                }

                if (request.ReportType == 1)
                {
                    message.Subject = "MOM Report";
                }
                if (request.ReportType == 2)
                {
                    message.Subject = "Visit Report";
                }
                if (request.ReportType == 3)
                {
                    message.Subject = "Training Report";
                }
                if (request.ReportType == 4)
                {
                    message.Subject = "Customer Feedback Report";
                }

                var personalizedHtmlBody = htmlBody.Replace("[RecipientName]", representative.RepresentativeName);

                var builder = new BodyBuilder
                {
                    HtmlBody = personalizedHtmlBody
                };

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:SmtpPort"]), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }

                return Ok(new { message = "Email sent successfully" });
           
        }

    }
}
