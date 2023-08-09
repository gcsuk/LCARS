using LCARS.Configuration.Models;
using LCARS.TeamCity.Responses;

namespace LCARS.TeamCity
{
    public class MockTeamCityService : ITeamCityService
    {
        public async Task<IEnumerable<Project>> GetProjects(TeamCitySettings settings) => await Task.FromResult(new List<Project> {
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

        public async Task<IEnumerable<Build>> GetBuilds(TeamCitySettings settings)
        {
            var builds = new List<Build>();
            var random = new Random();

            for (var i = 1; i <= 50; i++)
            {
                var isSuccess = random.Next(0, 2) == 1;
                var isRunning = random.Next(0, 2) == 1;
                var major = random.Next(0, 6);
                var minor = random.Next(0, 100);
                var patch = random.Next(0, 30);

                builds.Add(new Build
                {
                    DisplayName = $"Build {i}",
                    BuildTypeId = $"BuildType{i}",
                    BuildNumber = $"{major}.{minor}.{patch}.0",
                    State = isSuccess ? "SUCCESS" : "FAILURE",
                    Status = isRunning ? "running" : "finished",
                    Branch = $"branch-{i}",
                    PercentageComplete = isRunning ? random.Next(0, 100) : 100,
                    EstimatedTotalSeconds = random.Next(1, 120),
                    ElapsedSeconds = random.Next(1, 60),
                    ProbablyHanging = false,
                    CurrentStageText = "Some stage text",
                    WebUrl = $"https://teamcity/builds/id:{i}"
                });
            }

            return await Task.FromResult(builds);
        }
    }
}