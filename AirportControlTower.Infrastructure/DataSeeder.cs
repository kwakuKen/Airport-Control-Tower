﻿using AirportControlTower.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AirportControlTower.Infrastructure;

public static class DataSeeder
{

    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>().HasData(
            new Aircraft
            {
                Id = 1,
                CallSign = "NC9574",
                Type = "AIRLINER",
                PublicKey = "AAAAB3NzaC1yc2E"
            },
            new Aircraft
            {
                Id = 2,
                CallSign = "NC9222",
                Type = "PRIVATE",
                PublicKey = "AAAAB3NzaC1yc2E"
            }
        );

        modelBuilder.Entity<Users>().HasData(
           new Users
           {
               Id = 1,
               Username = "test@example.com",
               Password = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("password"))),
           }
       );

    }
}
