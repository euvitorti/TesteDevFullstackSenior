using DTOs.SuiteType;
using Models.SuiteType;
using System.Threading;
using System.Threading.Tasks;

namespace Services.SuiteTypes
{
    public interface ISuiteTypeService
    {
        /// <summary>
        /// Cria um novo tipo de suíte.
        /// </summary>
        /// <param name="suiteTypeDto">DTO com os dados para criação da suíte.</param>
        /// <param name="cancellationToken">
        /// Token para cancelamento da operação assíncrona (opcional).
        /// </param>
        /// <returns>O objeto Suite criado.</returns>
        Task<Suite> CreateSuiteTypeAsync(SuiteTypeDTO suiteTypeDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Recupera um tipo de suíte pelo identificador.
        /// </summary>
        /// <param name="id">Identificador da suíte.</param>
        /// <param name="cancellationToken">
        /// Token para cancelamento da operação assíncrona (opcional).
        /// </param>
        /// <returns>O objeto Suite encontrado ou null se não existir.</returns>
        Task<Suite?> GetSuiteTypeByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
