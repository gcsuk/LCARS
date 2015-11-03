# LCARS

The LCARS project is a dashboard system for anyone who needs an interesting looking interface for their metrics.

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

You need a few JSON files in App_Data that aren't in the repo in order to get the project running. If you implement different content screens, you will not need them for the template.

Settings and Environments aren't included in the repo because they contain personal data, but they are required files for certain boards.

The structure is as follows:

Environments.json

[
  {
    "Id": 1,
    "Name": "Web Site Name",
    "Environments": [
      {
        "Name": "Env 01"
        "Status": "ISSUES"
      },
      {
        "Name": "Env 02",
        "Status": "OK"
      },
      {
        "Name": "Env 03",
        "Status": "OK"
      },
      {
        "Name": "Env 04",
        "Status": "DOWN"
      }
    ]
  },
  ...
]

Deployments.json

[
    {
        "Id": "Env-5",
        "OrderId": "1",
        "Name": "Env 01"
    },
    ...
]

Build Set

[
    {
        "Name": "Build Name",
        "TypeId": "Build Type ID"
    },
    ...
]

Settings.json (for Jira, Team City and Octopus Deploy integration)

{
    "BuildServerCredentials": {
        "Username": "",
        "Password": ""
    },
    "DeploymentServerKey": "",
    "DeploymentServerPath": "",
    "IssuesUrl": "",
    "IssuesUsername": "",
    "IssuesPassword": ""
}