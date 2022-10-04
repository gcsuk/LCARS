using LCARS.TeamCity.Models;

namespace LCARS.TeamCity
{
    public class MockTeamCityService : ITeamCityService
    {
        public async Task<IEnumerable<Project>> GetProjects() => await Task.FromResult(new List<Project> {
            new Project
            {
                Id = "1",
                Name = "Project 1"
            },
            new Project
            {
                Id = "2",
                Name = "Project 2"
            },
            new Project
            {
                Id = "3",
                Name = "Project 3"
            },
            new Project
            {
                Id = "4",
                Name = "Project 4"
            },
            new Project
            {
                Id = "5",
                Name = "Project 5"
            }
        });

        public async Task<IEnumerable<Build>> GetBuildsComplete() => await Task.FromResult(new List<Build> {
            new Build
            {
                Id = 1,
                Number = "1",
                State = "SUCCESS",
                Status = "Complete",
                Branch = "branch-1",
                PercentageComplete = 100
            }
        });

        public async Task<IEnumerable<Build>> GetBuildsRunning() => await Task.FromResult(new List<Build> {
            new Build
            {
                Id = 1,
                Number = "1",
                State = "BUILDING",
                Status = "Complete",
                Branch = "branch-1",
                PercentageComplete = new Random().Next(100)
            }
        });
    }
}