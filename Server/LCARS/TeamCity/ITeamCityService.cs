﻿using LCARS.TeamCity.Responses;

namespace LCARS.TeamCity;

public interface ITeamCityService
{
    Task<IEnumerable<Project>> GetProjects();

    Task<IEnumerable<Build>> GetBuildsComplete();

    Task<IEnumerable<Build>> GetBuildsRunning();
}