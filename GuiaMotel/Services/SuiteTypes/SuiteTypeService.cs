using DTOs.SuiteType;
using GuiaMotel.Data;
using Microsoft.EntityFrameworkCore;
using Models.SuiteType;

namespace Services.SuiteTypes
{
    public class SuiteTypeService : ISuiteTypeService
    {
        private readonly ApplicationDbContext _context;
        public SuiteTypeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Suite> CreateSuiteTypeAsync(SuiteTypeDTO suiteTypeDto)
        {
            // Validação: verificar se o Motel existe.
            var motelExists = await _context.Motels.AnyAsync(m => m.Id == suiteTypeDto.MotelId);
            if (!motelExists)
            {
                throw new ArgumentException("Motel não encontrado.");
            }

            var suiteType = new Suite
            {
                Name = suiteTypeDto.Name,
                Price = suiteTypeDto.Price,
                MotelId = suiteTypeDto.MotelId
            };

            _context.SuiteTypes.Add(suiteType);
            await _context.SaveChangesAsync();

            return suiteType;
        }
        public async Task<Suite?> GetSuiteTypeByIdAsync(int id)
        {
            return await _context.SuiteTypes.FindAsync(id);
        }
    }
}