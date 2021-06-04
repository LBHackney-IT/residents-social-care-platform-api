# Residents Social Care Platform API

The Residents Social Care Platform API allows for services to retrieve
social care data of residents i.e. information formally managed by
Mosaic.

It is a part of the Social Care system (see [Social Care System Architecture](https://github.com/LBHackney-IT/social-care-architecture/tree/main) for more details and for the [process of tooling for diagram creation](https://github.com/LBHackney-IT/social-care-architecture/blob/main/process.md)).

![C4 Component Diagram](docs/component-diagram.svg)

## Table of contents

- [Getting started](#getting-started)
- [Usage](#usage)
  - [Running the application](#running-the-application)
  - [Running the tests](#running-the-tests)
- [Documentation](#documentation)
  - [Database](#migrations)
    - [Migrations](#migrations)
- [Active contributors](#active-contributors)
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

#### With Docker

To serve the API using Docker, use:

```sh
$ make serve
```

The application will be served at http://localhost:3000 and expose the database
at port `7654`.

#### Without Docker

To serve the API locally without Docker, use:

```sh
$ cd ResidentsSocialCarePlatformApi && dotnet run
```

The application will be served at http://localhost:5000.

To serve the API locally without Docker and in watch mode (application will automatically recompile and run on saving changes to code), use:

```sh
$ cd ResidentsSocialCarePlatformApi && dotnet watch run
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

### Running migrations

While running the test database locally (i.e you are able to run tests from within your IDE), run any new migrations with:

```sh
$ make migrate-test-db
```

This will run migrations on the `socialcare` database on your local machine.
(Your IDE connects to the `socialcare` database hosted on your localhost to run tests and not to the docker container).

## Documentation

### Database

#### Migrations

For our database when developing locally and testing, we have migrations set up (see `/ResidentsSocialCarePlatformApi/V1/Infrastructure/Migrations`) which uses [EF Core Code](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli), see our documentation on:

- [Adding a migration](./docs/adding-a-migration.md)
- [Editing a migration](./docs/editing-a-migration.md)
- [Troubleshooting migrations](./docs/troubleshooting-migrations.md)

## Active contributors

- **John Farrell**, Senior Software Engineer at Made Tech (john.farrell@hackney.gov.uk)
- **Renny Fadoju**, Software Engineer at Made Tech (renny.fadoju@hackney.gov.uk)
- **Neil Kidd**, Lead Software Engineer at Made Tech (neil.kidd@hackney.gov.uk)
- **Wen Ting Wang**, Software Engineer at Made Tech (wenting.wang@hackney.gov.uk)

## License

[MIT License](LICENSE)
