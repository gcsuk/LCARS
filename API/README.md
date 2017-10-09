# LCARS

The LCARS project is a dashboard system for anyone who needs an interesting looking interface for their metrics.

The project is designed primarily for a software development team, but could easily be adapted for other purposes.

## API Setup

You need Visual Studio 2017 Update 3, and NodeJS (latest stable will do)

There is a folder called "Databases" in the API project. Create a database, and run each SQL file against it. You should run `Sites` before `Environments` as there's a relationship there. The rest you can run in any order, but you will need them all.

Create a file called appsettings.json in the root of the API, and paste this into it:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "<YOUR DATABASE CONNECTION STRING HERE>"
  },
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

The API project should now compile and run, presenting you with the documentation via Swagger.

## Client Setup

Open a shell and navigate to the `Client` directory.
Run `npm install`
Run './node_modules/webpack-dev-server`

This will run the client, open a browser and navigate to the URL displayed in the shell.

## Usage

Every API set has a Settings endpoint, both `GET` and `PUT`. these will need configuring before the main endpoints will work. Hopefully the properties required are self explanatory, if not please raise an Issue and I'll get back to you.
