using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.DTOS.WorkerDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConstructionWebAPI.Services
{
    public class AdminService : IAdminService
    {
        public AppDbContext _dbContext;
        public AdminService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<WorkerResponseDTO?> CreateWorker(WorkerDTO createWorkerDTO)
        {
            var check = await _dbContext.Workers.FirstOrDefaultAsync(e => e.FirstName == createWorkerDTO.FirstName && e.LastName == createWorkerDTO.LastName);
            if (check is not null)
            {
                return null;
            }
            var worker = new Worker
            {
                FirstName = createWorkerDTO.FirstName,
                LastName = createWorkerDTO.LastName,
                Gender = createWorkerDTO.Gender,
                Salary = createWorkerDTO.Salary
            };
            await _dbContext.Workers.AddAsync(worker);
            await _dbContext.SaveChangesAsync();
            return MapToDTO(worker);
        }

        public async Task<bool> DeleteWorker(Guid guid)
        {
            var worker = await _dbContext.Workers.FindAsync(guid);
            if (worker is null)
            {
                return false;
            }
            _dbContext.Workers.Remove(worker);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<WorkerResponseDTO>> GetAllWorkers()
        {
            var workers = await _dbContext.Workers.ToListAsync();
            return workers.Select(MapToDTO).ToList();
        }

        public async Task<WorkerResponseDTO?> GetWorkerDataById(Guid guid)
        {

            var worker = await _dbContext.Workers.FindAsync(guid);
            if (worker is null) return null;
            return MapToDTO(worker);
        }
        public async Task<WorkerResponseDTO?> UpdateWorkerData(Guid guid, WorkerDTO workerDTO)
        {
            var worker = await _dbContext.Workers.FindAsync(guid);
            if (worker is null) return null;

            worker.FirstName = workerDTO.FirstName;
            worker.LastName = workerDTO.LastName;
            worker.Gender = workerDTO.Gender;
            worker.Salary = workerDTO.Salary;

            await _dbContext.SaveChangesAsync();
            return MapToDTO(worker);

        }
        private static WorkerResponseDTO MapToDTO(Worker worker)
        {
            return new WorkerResponseDTO
            {
                Id = worker.Id,
                FullName = $"{worker.FirstName} {worker.LastName}",
                Gender = worker.Gender,
                Salary = worker.Salary
            };
        }
    }
}