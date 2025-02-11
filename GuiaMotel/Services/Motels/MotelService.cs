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

        /// <summary>
        /// Cria um novo motel no banco de dados.
        /// </summary>
        /// <param name="motelDto">DTO contendo os dados do motel.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns>O objeto Motel criado.</returns>
        public async Task<Motel> CreateMotelAsync(MotelDTO motelDto, CancellationToken cancellationToken = default)
        {
            // Aqui você pode incluir validações de negócio, por exemplo, checar se o CNPJ já existe.
            var motel = new Motel
            {
                Name = motelDto.Name,
                Address = motelDto.Address,
                CNPJ = motelDto.CNPJ
            };

            // Adiciona o novo motel ao contexto do Entity Framework
            _context.Motels.Add(motel);

            // Persiste as alterações no banco de dados
            await _context.SaveChangesAsync(cancellationToken);

            return motel;
        }

        /// <summary>
        /// Busca um motel pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do motel.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns>O motel encontrado ou null se não existir.</returns>
        public async Task<Motel?> GetMotelByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            // Utilizamos o FindAsync, que internamente usa parâmetros para evitar SQL Injection.
            // O método também aceita um CancellationToken para possibilitar o cancelamento da operação.
            return await _context.Motels.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
