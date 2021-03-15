# Residents Social Care Platform API

The Residents Social Care Platform API allows for services to retrieve
social care data of residents i.e. information formally managed by
Mosaic. This repository is based off the
[Mosaic Resident Information API](https://github.com/LBHackney-IT/mosaic-resident-information-api).

It uses [.NET Core](https://dotnet.microsoft.com) as a web framework and
[nUnit](https://nunit.org) for testing.

## Contents

- [Getting started](#getting-started)
- [Usage](#usage)
  - [Running the application](#running-the-application)
  - [Running the tests](#running-the-tests)
- [Documentation](#documentation)
- [Contributors](#contributors)
- [License](#license)

## Getting started

### Prerequisites

- [AWS CLI](https://aws.amazon.com/cli/)
- [Docker](https://www.docker.com/products/docker-desktop)
- [.NET Core](https://dotnet.microsoft.com/download)

### Installation

1. Clone this repository

```sh
$ git clone git@github.com:LBHackney-IT/residents-social-care-platform-api.git
```

## Usage

### Running the application

To serve the API using Docker, use:

```sh
$ make serve
```

The application will be served at http://localhost:3000.

To serve the API locally, use:

```sh
$ cd ResidentsSocialCarePlatformApi && dotnet run
```

The application will be served at http://localhost:5000.

### Running the tests

There are two ways of running the tests against a test database: using the
terminal and using an IDE.

#### Using the terminal

To run all tests, use:

```sh
$ make test
```

#### Using an IDE

First start up the test database to run in the background, using:

```sh
$ make start-test-db
```

This will allow you to run the tests as normal in your IDE.

## Migrations

### Adding a migration

We are using EF Core Code first migrations to manage the schema for the test database.
To make changes to the test database structure follow these steps.

1. You need have the [dotnet ef cli tool](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) installed.
   To install run:

```sh
$ dotnet tool install --global dotnet-ef
```

2. Make the necessary changes to the database model i.e `SocialCareContext` and/or any of the DbSet's listed in the file.

3. In your terminal, navigate to the root folder of the repo and run:

```sh
$ dotnet ef migrations add -o ./V1/Infrastructure/Migrations -p ResidentsSocialCarePlatformApi NameOfThisMigration
```

`NameOfThisMigration` should be replaced with your migration name e.g. AddColumnNameToPeopleTable.
This will create a Migrations folder in the Infrastructure (if it doesn't exist)
and creates a migration for the changes you've made for the database model.

4. Go to the folder /ResidentContactApi/V1/Infrastructure/Migrations and you should see two new files for the migration.
   In the one which doesn't end in `.Designer` you can check through the migration script to make sure the migration file has the changes you expect.

### Editing a Generated Migration

If the migration file looks wrong or you have missed something, you can do either of the following:

1. While the test database is running in the background, run:
   ```sh
   $ CONNECTION_STRING="Host=127.0.0.1;Database=testsocialcare;Username=postgres;Password=mypassword;" dotnet ef migrations remove -p ResidentsSocialCarePlatformApi
   ```

2. Or delete the migration files and revert the changes to `SocialCareContextModelSnapshot.cs`. Make the necessary changes to the context, then create the migration files again.

Note: Any changes made to a `DbSet` or within `SocialCareContext` should have an associated migration generated for it.

### Applying a Migration

While running the test database locally (i.e you are able to run tests from within your IDE), run any new migrations with:
```sh
$ make migrate-local-test-database
```

This will run migrations on the `testsocialcare` database on your local machine.
(Your IDE connects to the `testsocialcare` database hosted on your localhost to run tests and not to the docker container).

### Troubleshooting Migrations
If you encounter errors about tables already existing after running the `migrate-local-test-database` make command,
you can try using [psql](https://www.postgresql.org/docs/current/app-psql.html)
to delete all the existing tables in `dbo` schema and the `__EFMigrationsHistory` table from the `testsocialcare` database hosted on your localhost.

1. In your terminal, connect to the localhost instance of testsocialcare using psql:
```sh
$ psql -h 127.0.0.1 -p 5432 -d testsocialcare -U postgres
```

2. Delete all the tables in the testsocialcare `dbo` schema:
```sh
DROP SCHEMA IF EXISTS dbo CASCADE;
```
N.B: When running the migrations, it will check if the `dbo` schema exists first and create it if doesn't, so no need to recreate it here.


3. Delete the EF Migrations History table:
```sh
DROP TABLE __EFMigrationsHistory CASCADE;
```

N.B: Do not delete the public schema, if you do, you will need to recreate it using `CREATE SCHEMA`.

4. Exit the psql. You can use `\q`

5. Run the `make migrate-local-test-database` command again.
   This should run the migrations on the database, create a new `__EFMigrationsHistory` table and save the new migration history there.

## Documentation

See [Docs](./docs/README.md).

## Contributors

- **Matt Bee**, Senior Software Engineer at Made Tech (matt.bee@hackney.gov.uk)
- **Renny Fadoju**, Software Engineer at Made Tech (renny.fadoju@hackney.gov.uk)
- **Wen Ting Wang**, Software Engineer at Made Tech (wenting.wang@hackney.gov.uk)

## License

[MIT License](LICENSE)
