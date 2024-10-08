using Maxus.Application.DTOs.IndustrySegments;

namespace Maxus.Application.Interfaces
{
    public interface IIndustrySegmentsService : IBaseService<IndustrySegmentsListRequest, IndustrySegmentsListResponse, IndustrySegmentsByIdResponse, CreateIndustrySegmentsRequest, UpdateIndustrySegmentsRequest>
    {
    }
}
