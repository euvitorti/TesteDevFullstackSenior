using DTOs.Motels;
using Models.Motels;

namespace Services.Motels
{
    public interface IMotelService
    {
        Task<Motel> CreateMotelAsync(MotelDTO motelDto);
        Task<Motel?> GetMotelByIdAsync(int id);
    }
}