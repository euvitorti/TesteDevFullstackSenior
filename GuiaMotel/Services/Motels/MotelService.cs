using DTOs.Motels;
using GuiaMotel.Data;
using Models.Motels;

namespace Services.Motels
{
    public class MotelService : IMotelService
    {
        private readonly ApplicationDbContext _context;

        public MotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Motel> CreateMotelAsync(MotelDTO motelDto)
        {
            // Aqui você pode incluir validações de negócio, por exemplo, checar se o CNPJ já existe.
            var motel = new Motel
            {
                Name = motelDto.Name,
                Address = motelDto.Address,
                CNPJ = motelDto.CNPJ
            };

            _context.Motels.Add(motel);
            await _context.SaveChangesAsync();

            return motel;
        }

        public async Task<Motel?> GetMotelByIdAsync(int id)
        {
            return await _context.Motels.FindAsync(id);
        }
    }
}