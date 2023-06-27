using DogGo.Models;
using DogGo.Repositories;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
namespace DogGo.Interfaces
{
    public interface IDogRepository
    {
        
            List<Dog> GetAllDogs();
            Dog GetDogById(int id);
            List<Dog> GetDogsByOwnerId(int ownerId);
        void AddDog(Dog dog);
             void UpdateDog(Dog dog);
             void DeleteDog(int id);



    }
}
