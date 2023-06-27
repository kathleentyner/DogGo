using DogGo.Models;

namespace DogGo.Repositories
    
{
    public interface IWalkRepository
    {

        List<Walk> GetAll();
        List<Walk> GetWalkByWalkerId(int walkerId);

    }
}
