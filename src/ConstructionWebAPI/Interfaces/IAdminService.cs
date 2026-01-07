using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionWebAPI.Interfaces
{
    public interface IAdminService
    {
       
        Task<List<Worker>> GetAllWorkers();
        Task<Worker?> GetWorkerDataById(Guid guid);
        Task<bool> DeleteWorker(Guid guid);
        Task<Worker?> CreateWorker(WorkerDTO createWorkerDTO);
        Task<Worker?> UpdateWorkerData(Guid guid, WorkerDTO workerDTO);


    }
}
