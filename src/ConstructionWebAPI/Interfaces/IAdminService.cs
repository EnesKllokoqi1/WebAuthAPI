using ConstructionWebAPI.DTOS.WorkerDTOS;
using ConstructionWebAPI.DTOS; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionWebAPI.Interfaces
{
    public interface IAdminService
    {
        Task<List<WorkerResponseDTO>> GetAllWorkers();
        Task<WorkerResponseDTO?> GetWorkerDataById(Guid guid);
        Task<bool> DeleteWorker(Guid guid);
        Task<WorkerResponseDTO?> CreateWorker(WorkerDTO createWorkerDTO);
        Task<WorkerResponseDTO?> UpdateWorkerData(Guid guid, WorkerDTO workerDTO);
    }
}
