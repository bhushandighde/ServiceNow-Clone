using ServiceNow.Infrastructure.Persistence;

namespace ServiceNow.ServiceNow.Application.Services
{
    public class JwtService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        public JwtService(ApplicationDbContext db, IConfiguration config) {
            _db=db;
            _config=config;
        }
    }
}
