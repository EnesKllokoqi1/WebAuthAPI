using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.Enums;

namespace ConstructionWebAPI.Interfaces
{
    public interface IAssignmentService
    {
        Task<AssignmentResponseDTO?> CreateAssignment(AssignmentDTO assignmentDTO);
        Task<bool> DeleteAssignment(Guid guid);
        Task<AssignmentResponseDTO?> GetAssignmentDetails(Guid guid);
        Task<AssignmentResponseDTO?> UpdateAssignmentData(Guid guid,AssignmentDTO dto);
        Task<List<AssignmentResponseDTO?>> GetAllAssignments();
        Task<AssignmentsWithUserResponseDTO?> ConnectUserToAssignment(Guid userId,AssignmentUserConnectionDTO assignmentUserConnectionDTO);
        Task<AssignmentWithBuildingResponseDTO?> ConnectBuildingToAssignment(AssignmentBuildingConnectionDTO assignmentWithBuilding);
        Task<AssignmentWithWorkerResponseDTO?> ConnectWorkerToAssignment(AssignmentWorkerConnectionDTO assignmentWorkerConnectionDTO);
        Task<AssignmentResponseDTO?> StartAssignment(Guid assignmentId);
        Task<AssignmentResponseDTO?> CompleteAssignment(Guid assignmentId);
        Task<AssignmentResponseDTO?> CancelAssignment(Guid assignmentId);
        Task<AssignmentResponseDTO?> RejectAssignment(Guid assignmentId);
        Task<AssignmentResponseDTO?> ConfirmAssignment(Guid assignmentId);

    }
}
