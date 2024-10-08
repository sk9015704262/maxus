using AutoMapper;
using Maxus.Application.DTOs.Company;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<CompanyByIdResponse> CreateAsync(CreateCompanyRequest obj)
        {
            try
            {
                var company = new tbl_CompanyMaster
                {
                    Code = obj.Code,
                    Name = obj.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var createdCompany = await _companyRepository.CreateAsync(company);
                return _mapper.Map<CompanyByIdResponse>(createdCompany);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _companyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Company Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<CompanyListResponse>)> GetAllAsync(CompanyListRequest obj)
        {
            try
            {
                var (paginationResponse, companies) = await _companyRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn);
                var mappedCompanies = _mapper.Map<IEnumerable<CompanyListResponse>>(companies);
                return (paginationResponse, mappedCompanies);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Company Error In Service.", ex);
            }
        }

        public async Task<CompanyByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(id);
                return _mapper.Map<CompanyByIdResponse>(company);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Company Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<CompanyListResponseByUser>> GetCompanyByUser(GetCompanyByUserRequest obj)
        {
            try
            {
                var companies = await _companyRepository.GetCompanyByUser(obj.UserId);
                return _mapper.Map<IEnumerable<CompanyListResponseByUser>>(companies);
            }
            catch (Exception ex)
            {
                throw new Exception("GetByUser Company Error In Service.", ex);
            }
        }

        public async Task<CompanyByIdResponse> UpdateAsync(int id, UpdateCompanyRequest request)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(id);
                if (company != null)
                {
                    company.UpdatedAt = DateTime.Now;
                    company.UpdatedBy = CurrentUserId();
                    company.Name = request.Name;
                    company.Code = request.Code;
                    var success = await _companyRepository.UpdateAsync(company);
                    if (success)
                    {
                        
                    return _mapper.Map<CompanyByIdResponse>(company);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Company Error In Service.", ex);
            }
        }
    }
}
