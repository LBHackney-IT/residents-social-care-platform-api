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

### Running the tests

There are two ways of running the tests against a test database: using the
terminal and using an IDE.

## Using the terminal

To run all tests, use:

```sh
$ make test
```

## Using an IDE

First start up the test database to run in the background, using:

```sh
$ make start-test-db
```

This will allow you to run the tests as normal in your IDE.

## Documentation

See [Docs](./docs/README.md).

## Contributors

- **Matt Bee**, Senior Software Engineer at Made Tech (matt.bee@hackney.gov.uk)
- **Renny Fadoju**, Software Engineer at Made Tech (renny.fadoju@hackney.gov.uk)
- **Wen Ting Wang**, Software Engineer at Made Tech (wenting.wang@hackney.gov.uk)

## License

[MIT License](LICENSE)
