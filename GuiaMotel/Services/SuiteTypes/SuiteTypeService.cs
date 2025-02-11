using DTOs.SuiteType;
using GuiaMotel.Data;
using Microsoft.EntityFrameworkCore;
using Models.SuiteType;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.SuiteTypes
{
    public class SuiteTypeService : ISuiteTypeService
    {
        private readonly ApplicationDbContext _context;

        public SuiteTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo tipo de suíte no sistema, verificando se o motel associado existe.
        /// </summary>
        /// <param name="suiteTypeDto">DTO com os dados para criação da suíte.</param>
        /// <param name="cancellationToken">
        /// Token para cancelamento da operação assíncrona (opcional).
        /// Permite abortar a operação se necessário.
        /// </param>
        /// <returns>O objeto Suite criado.</returns>
        public async Task<Suite> CreateSuiteTypeAsync(SuiteTypeDTO suiteTypeDto, CancellationToken cancellationToken = default)
        {
            // Validação: verifica se o Motel informado existe, usando AnyAsync para maior eficiência.
            var motelExists = await _context.Motels.AnyAsync(m => m.Id == suiteTypeDto.MotelId, cancellationToken);
            if (!motelExists)
            {
                throw new ArgumentException("Motel não encontrado.");
            }

            // Cria o objeto Suite com base no DTO recebido.
            var suiteType = new Suite
            {
                Name = suiteTypeDto.Name,
                Price = suiteTypeDto.Price,
                MotelId = suiteTypeDto.MotelId
            };

            // Adiciona o novo tipo de suíte ao contexto.
            _context.SuiteTypes.Add(suiteType);
            // Persiste as alterações no banco de dados.
            await _context.SaveChangesAsync(cancellationToken);

            return suiteType;
        }

        /// <summary>
        /// Recupera um tipo de suíte pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da suíte.</param>
        /// <param name="cancellationToken">
        /// Token para cancelamento da operação assíncrona (opcional).
        /// </param>
        /// <returns>O objeto Suite encontrado ou null se não existir.</returns>
        public async Task<Suite?> GetSuiteTypeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            // Utiliza AsNoTracking para melhorar a performance da consulta, já que não há necessidade
            // de rastrear a entidade recuperada para futuras alterações.
            return await _context.SuiteTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
