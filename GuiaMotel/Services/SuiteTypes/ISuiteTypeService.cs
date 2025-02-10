using DTOs.SuiteType;
using Models.SuiteType;

namespace Services.SuiteTypes
{
    public interface ISuiteTypeService
    {
        Task<Suite> CreateSuiteTypeAsync(SuiteTypeDTO suiteTypeDto);
        Task<Suite?> GetSuiteTypeByIdAsync(int id);
    }
}