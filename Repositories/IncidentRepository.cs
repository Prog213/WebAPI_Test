using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly MyDbContext _context;
        public IncidentRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Incident> CreateAsync(Incident incidentModel)
        {
            await _context.Incidents.AddAsync(incidentModel);
            await _context.SaveChangesAsync();

            return await _context.Incidents.Include(i => i.Account)
                .ThenInclude(a => a.Contact)
                .FirstAsync(i => i.IncidentName == incidentModel.IncidentName);
        }
    }
}
