using AutoMapper;
using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.DTOs.Client;
using Maxus.Application.DTOs.Company;
using Maxus.Application.DTOs.CustomerFeedback;
using Maxus.Application.DTOs.CustomerFeedbackOption;
using Maxus.Application.DTOs.CustomerFeedbackReport;
using Maxus.Application.DTOs.dashboard;
using Maxus.Application.DTOs.IndustrySegments;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.Site;
using Maxus.Application.DTOs.Topic;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.DTOs.UserFromRight;
using Maxus.Application.DTOs.UserRights;
using Maxus.Application.DTOs.Users;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.DTOs.VisitReportChekList;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Entities.PartialEntities;

namespace Maxus.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<tbl_CompanyMaster, CompanyListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_BranchMaster, BranchByIdResponse>()
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_BranchMaster, BranchListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_CompanyMaster, CompanyByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_IndustrySegments, IndustrySegmentsByIdResponse>()
                  .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ReverseMap();
            CreateMap<tbl_IndustrySegments, IndustrySegmentsListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ReverseMap();
            CreateMap<tbl_TopicsMaster, TopicByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
             .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
             .ReverseMap();
            CreateMap<tbl_TopicsMaster, TopicListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
             .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
             .ReverseMap();
            CreateMap<tbl_ClientMaster, ClientByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
            .ReverseMap();
            CreateMap<tbl_ClientMaster, ClientListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
           .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
           .ReverseMap();
            CreateMap<tbl_SiteMaster, SiteByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
           .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
          .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
          .ReverseMap();
            CreateMap<tbl_SiteMaster, SiteListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
          .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
         .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
         .ReverseMap();
            CreateMap<tbl_SiteMaster, GetSiteByUserResponse>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
               .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
        .ReverseMap();
            CreateMap<tbl_VisitReportChekListMaster, VisitReportByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
        .ReverseMap();
            CreateMap<tbl_VisitReportChekListMaster, VisitReportListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ReverseMap();
            CreateMap<tbl_VisitReportChekListMaster, VisitReportByCompanyListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ReverseMap();
            CreateMap<tbl_CustomerFeedbackMaster, CustomerFeedbackListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
       .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName??""))
              .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName ?? ""))
       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ReverseMap();
            CreateMap<tbl_CustomerFeedbackMaster, CustomerFeedbackByCompanyListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
       .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName ?? ""))
              .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName ?? ""))
       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ReverseMap();
            CreateMap<tbl_CustomerFeedbackMaster, CustomerFeedbackByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
      .ReverseMap();
            CreateMap<tbl_UserCompany, CompanyListResponseByUser>()
        .ReverseMap();
            CreateMap<tbl_CustomerCheklistOption, CustomerFeedbackOptionByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ReverseMap();
            CreateMap<tbl_CustomerCheklistOption, CustomerFeedbackOptionListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
       .ReverseMap();
            CreateMap<tblTrainingReport, TrainingReportByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.AttchmentPath, opt => opt.MapFrom(src => src.AttchmentPath.Select(a => new AttachmentDto
                {
                    AttachmentPath = a.AttachmentPath,
                    CreatedAt = a.CreatedAt.ToString("dd-MMM-yyyy HH:mm"),
                    CreatedByName = a.CreatedByName
                }).ToList()))
                .ReverseMap();
            CreateMap<tblTrainingReport, TrainingListResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_UserRights, UserRightsByIdResponse>()
               .ReverseMap();
            CreateMap<tbl_UserRights, UserRightListResponse>()
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
              .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ReverseMap();
            CreateMap<tbl_UserFormRights, UserFormRightByIdResponse>()
              .ReverseMap();
            CreateMap<tbl_Users, UsersListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_Users, UserByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_ClientRepresentativeDetails, ClientRepresentativeDetails>()
              .ReverseMap();
            CreateMap<tbl_AttachmentLimits, AttachmentLimitByIdResponse>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ReverseMap();
            CreateMap<tbl_AttachmentLimits, AttachmentLimitListResponse>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                 .ReverseMap();
            CreateMap<tbl_MOMReport, GetMOMByIdResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                  .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.CloserDate, opt => opt.MapFrom(src => src.CloserDate.ToString("dd-MMM-yyyy HH:mm")))
                  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ForMember(dest => dest.AttchmentPath, opt => opt.MapFrom(src => src.AttchmentPath.Select(a => new AttachmentDto
               {
                   AttachmentPath = a.AttachmentPath,
                   CreatedAt = a.CreatedAt.ToString("dd-MMM-yyyy HH:mm"),
                   CreatedByName = a.CreatedByName
               }).ToList()))
                 .ReverseMap();
            CreateMap<tbl_MOMReport, MOMReportListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.CloserDate, opt => opt.MapFrom(src => src.CloserDate.ToString("dd-MMM-yyyy HH:mm")))
                .ReverseMap();
            CreateMap<tbl_CustomerFeedbackReport, CustomerFeedbackReportByIdResponse>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
                 .ForMember(dest => dest.AttchmentPath, opt => opt.MapFrom(src => src.AttchmentPath.Select(a => new AttachmentDto
                 {
                     AttachmentPath = a.AttachmentPath,
                     CreatedAt = a.CreatedAt.ToString("dd-MMM-yyyy HH:mm"),
                     CreatedByName = a.CreatedByName
                 }).ToList()))
                 .ForMember(dest => dest.CheckLists, opt => opt.MapFrom(src => src.CheckLists.Select(a => new CustomerFeedbackMasterResponseDTO
                 {
                     Id = a.Id,
                     Name = a.Name,
                     Description = a.Description
                }).ToList()))
               .ReverseMap();
            CreateMap<tbl_CustomerFeedbackReport, CustomerFeedbackReportListResponse>()
                  .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
              .ReverseMap();
            CreateMap<tbl_VisitReport, VisitReportByidResponse>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
               
                .ForMember(dest => dest.AttchmentPath, opt => opt.MapFrom(src => src.AttchmentPath.Select(a => new AttachmentDto
                {
                    AttachmentPath = a.AttachmentPath,
                    CreatedAt = a.CreatedAt.ToString("dd-MMM-yyyy HH:mm"),
                    CreatedByName = a.CreatedByName
                }).ToList()))
               .ReverseMap();
            CreateMap<tbl_VisitReport, VisitReportByListResponse>()
                 .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByName))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByName))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy HH:mm")))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy HH:mm")))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MMM-yyyy HH:mm")))
              
              .ReverseMap();
            CreateMap<tbl_DashboardCount, GetDashBoardResponse>()
               
             .ReverseMap();


        }
    }
}
