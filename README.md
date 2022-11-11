[![Build Status](https://dev.azure.com/darlorob/LCARS/_apis/build/status/LCARS?branchName=master)](https://dev.azure.com/darlorob/LCARS/_build/latest?definitionId=4&branchName=master)

# LCARS Dashboard

A dashboard for many systems themed with a Library Computer Access/Retrieval System (LCARS) interface

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

Technologies used include dotNet7 Minimal APIs and Blazor

Available API integrations:
| Integration | Data Available |
| ------ | ------ |
| GitHub | Branches & Pull Requests |
| BitBucket | Branches & Pull Requests |
| Team City | Builds |
| Octopus Deploy | Deployments |
| Jira | Tickets |

> Note: There is also a `Red Alert` function that is designed to override all screens on the dashboard in the client with an alert. More on that in the Client section

## Getting Started

You will need Visual Studio 2022.4+ (as it is a dotNet7 app)

### API

You can simply hit F5 in Visual Studio and the application will run with mock data without any configuration. Swagger will load by default.
 
Only a few steps are needed to get the API working with real data:

- [Configure an Azure Table Storage account](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal)
- [Create a Table called LCARS](https://learn.microsoft.com/en-us/azure/storage/tables/table-storage-quickstart-portal)
- Get the Connection String for your table storage account from the Access Keys blade in the Azure Portal
- In appsetitngs.json
    - Set `ConnectionString` to the value retrieved above
    - Set `EnableMocks` to `false`
    - Set the `BaseUrl` properties for any APIs you intend to integrate with. BitBucket and GitHub are already set.
        - You can just leave anything you don't intend to use as-is, they will be ignored - but they cannot be blank or removed
- Run the app!
- You will be presented with a Swagger interface to the API
- Click on `Get All Settings`
    - Click `Try It Out`
    - Click `Execute`
- You will be presented with all the settings needed for the system to operate. Everything will currently be disabled and have null values.
- Use the POST methods for each set of settings related to APIs you want to integrate with
- Run `Get All Settings` again to review everything to make sure it is in order

### Client

The client is written in Blazor; it connects to the API and renders any enabled screens on a timer.

> If `Red Alert` is enabled, the client will route to the Red Alert screen regardless of what else is enabled.

To configure the client, you need only set the URL of the API in appsettings.json. You can get the UTL from the Overview blade of the Azure Portal where your API is hosted.
