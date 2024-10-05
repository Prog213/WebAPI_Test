using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> CreateAsync(Incident incidentModel);
    }
}
