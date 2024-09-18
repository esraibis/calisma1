using calisma1.Interfaces;
using calisma1.Models;

namespace calisma1.Services
{
    public class PointService : GenericService<Point>, IPointService
    {
        public PointService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}