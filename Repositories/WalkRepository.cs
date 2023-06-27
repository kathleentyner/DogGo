using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walks.Id AS WalkId, Duration, [Date], WalkerId, Walks.DogId AS DogId, Dog.Name AS DogName, Dog.OwnerId as OwnerId, Breed, Notes, ImageUrl, Owner.Id as OwnerId, Owner.Email as OwnerEmail, Owner.Name AS OwnerName, Owner.Address AS OwnerAddress, NeighborhoodId, Phone, Neighborhood.Name AS NeighborhoodName
                                FROM Walks
                                JOIN Dog ON Walks.DogId = Dog.Id
                                JOIN [Owner] ON Dog.OwnerId = Owner.Id
                                JOIN Neighborhood ON NeighborhoodId = Neighborhood.Id
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("WalkId")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                Owner = new Owner
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                    Email = reader.GetString(reader.GetOrdinal("OwnerEmail")),
                                    Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                    Address = reader.GetString(reader.GetOrdinal("OwnerAddress")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                    NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Neighborhood = new Neighborhood
                                    {
                                        Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                        Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                                    }
                                }
                            }

                        };
                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public List<Walk> GetWalkByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walks.Id AS WalkId, Duration, [Date], WalkerId, Walks.DogId AS DogId, Dog.Name AS DogName, Dog.OwnerId as OwnerId, Breed, Notes, ImageUrl, Owner.Id as OwnerId, Owner.Email as OwnerEmail, Owner.Name AS OwnerName, Owner.Address AS OwnerAddress, NeighborhoodId, Phone, Neighborhood.Name AS NeighborhoodName
                                FROM Walks
                                JOIN Dog ON Walks.DogId = Dog.Id
                                JOIN [Owner] ON Dog.OwnerId = Owner.Id
                                JOIN Neighborhood ON NeighborhoodId = Neighborhood.Id                                
                                WHERE WalkerId = @walkerId
                                ORDER BY Owner.Name
                    ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkId")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                Dog = new Dog
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                    //Name = reader.GetString(reader.GetOrdinal("DogName")),
                                    //Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                    //Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                    //ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                    OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                    Owner = new Owner
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                        Email = reader.GetString(reader.GetOrdinal("OwnerEmail")),
                                        Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                        Address = reader.GetString(reader.GetOrdinal("OwnerAddress")),
                                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                        NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                        Neighborhood = new Neighborhood
                                        {
                                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                            Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                                        }
                                    }
                                }

                            };


                            walks.Add(walk);
                        }
                        reader.Close();

                        return walks;
                    }
                }
            }
        }
    }
}