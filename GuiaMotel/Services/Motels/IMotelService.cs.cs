using DTOs.Motels;
using Models.Motels;

namespace Services.Motels
{
    public interface IMotelService
    {
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>

        /// <summary>
        /// Cria um novo motel no banco de dados.
        /// </summary>
        /// <param name="motelDto">DTO contendo os dados do motel.</param>
        /// <returns>O objeto Motel criado.</returns>
        Task<Motel> CreateMotelAsync(MotelDTO motelDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Busca um motel pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do motel.</param>
        /// <returns>O motel encontrado ou null se não existir.</returns>
        Task<Motel?> GetMotelByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
