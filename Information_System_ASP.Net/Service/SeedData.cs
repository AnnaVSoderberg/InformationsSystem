using Information_System_ASP.Net.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Information_System_ASP.Net.Service
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            // Seed data for Drivers
            builder.Entity<Driver>().HasData(
                new Driver
                {
                    DriverID = 1,
                    DriverName = "John Doe",
                    CarReg = "ABC123",
                    NoteDate = new DateTime(2024, 1, 10),
                    ResponsibleEmployee = "Jane Smith",
                    NoteDescription = "General maintenance check"
                },
                new Driver
                {
                    DriverID = 2,
                    DriverName = "Emily Johnson",
                    CarReg = "XYZ789",
                    NoteDate = new DateTime(2024, 1, 12),
                    ResponsibleEmployee = "Michael Brown",
                    NoteDescription = "Brake and tire replacement"
                },
                new Driver
                {
                    DriverID = 3,
                    DriverName = "Robert Wilson",
                    CarReg = "LMN456",
                    NoteDate = new DateTime(2024, 1, 15),
                    ResponsibleEmployee = "Alice Green",
                    NoteDescription = "Air filter and tire pressure check"
                },
                new Driver
                {
                    DriverID = 4,
                    DriverName = "Sarah Miller",
                    CarReg = "GHI123",
                    NoteDate = new DateTime(2024, 1, 18),
                    ResponsibleEmployee = "David Clark",
                    NoteDescription = "Windshield wiper repair"
                },
                new Driver
                {
                    DriverID = 5,
                    DriverName = "James Davis",
                    CarReg = "OPQ789",
                    NoteDate = new DateTime(2024, 1, 20),
                    ResponsibleEmployee = "Emma White",
                    NoteDescription = "Headlight and interior cleaning"
                }
            );

            // Seed data for Events
            builder.Entity<Event>().HasData(
                new Event
                {
                    EventID = 1,
                    DriverID = 1,
                    EventDescription = "Checked tire pressure",
                    EventDate = new DateTime(2024, 1, 11),
                    LoggedByEmployee = "Jane Smith",
                    BeloppIn = 500.00M,
                    BeloppUt = 200.00M
                },
                new Event
                {
                    EventID = 2,
                    DriverID = 1,
                    EventDescription = "Changed oil",
                    EventDate = new DateTime(2024, 1, 15),
                    LoggedByEmployee = "Jane Smith",
                    BeloppIn = 0.00M,
                    BeloppUt = 100.00M
                },
                new Event
                {
                    EventID = 3,
                    DriverID = 2,
                    EventDescription = "Washed car",
                    EventDate = new DateTime(2024, 1, 13),
                    LoggedByEmployee = "Michael Brown",
                    BeloppIn = 600.00M,
                    BeloppUt = 150.00M
                },
                new Event
                {
                    EventID = 4,
                    DriverID = 2,
                    EventDescription = "Replaced brake pads",
                    EventDate = new DateTime(2024, 1, 17),
                    LoggedByEmployee = "Michael Brown",
                    BeloppIn = 0.00M,
                    BeloppUt = 300.00M
                },
                new Event
                {
                    EventID = 5,
                    DriverID = 3,
                    EventDescription = "Cleaned interior",
                    EventDate = new DateTime(2024, 1, 16),
                    LoggedByEmployee = "Alice Green",
                    BeloppIn = 550.00M,
                    BeloppUt = 180.00M
                },
                new Event
                {
                    EventID = 6,
                    DriverID = 3,
                    EventDescription = "Changed air filter",
                    EventDate = new DateTime(2024, 1, 19),
                    LoggedByEmployee = "Alice Green",
                    BeloppIn = 0.00M,
                    BeloppUt = 50.00M
                },
                new Event
                {
                    EventID = 7,
                    DriverID = 4,
                    EventDescription = "Fixed windshield wipers",
                    EventDate = new DateTime(2024, 1, 18),
                    LoggedByEmployee = "David Clark",
                    BeloppIn = 750.00M,
                    BeloppUt = 220.00M
                },
                new Event
                {
                    EventID = 8,
                    DriverID = 5,
                    EventDescription = "Repaired headlights",
                    EventDate = new DateTime(2024, 1, 21),
                    LoggedByEmployee = "Emma White",
                    BeloppIn = 670.00M,
                    BeloppUt = 170.00M
                }
            );
        }
    }
}
