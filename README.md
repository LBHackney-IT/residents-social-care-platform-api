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

2. In the `residents-social-care-platform-api` directory, install the dependencies

```sh
$ cd residents-social-care-platform-api
$ dotnet restore
```

3. Build the C# projects

```sh
$ dotnet build
```

## Usage

### Running the application

To serve the application, run it using your IDE of choice, we use Visual Studio CE and JetBrains Rider on Mac.

The application can also be served locally using docker:
1.  Add you security credentials to AWS CLI.
```sh
$ aws configure
```
2. Log into AWS ECR.
```sh
$ aws ecr get-login --no-include-email
```
3. Build and serve the application. It will be available in the port 3000.
```sh
$ make build && make serve
```

### Running the tests

```sh
$ make test
```

To run database tests locally (e.g. via Visual Studio) the `CONNECTION_STRING` environment variable will need to be populated with:

`Host=localhost;Database=testsocialcare;Username=postgres;Password=mypassword"`

Note: The Host name needs to be the name of the stub database docker-compose service, in order to run tests via Docker.

## Documentation

See [Docs](./docs/README.md).

## Contributors

- **Matt Bee**, Senior Software Engineer at Made Tech (matt.bee@hackney.gov.uk)
- **Renny Fadoju**, Software Engineer at Made Tech (renny.fadoju@hackney.gov.uk)
- **Wen Ting Wang**, Software Engineer at Made Tech (wenting.wang@hackney.gov.uk)

## License

[MIT License](LICENSE)
