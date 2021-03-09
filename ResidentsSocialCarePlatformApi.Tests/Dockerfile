FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./MosaicResidentInformationApi.sln ./
COPY ./MosaicResidentInformationApi/MosaicResidentInformationApi.csproj ./MosaicResidentInformationApi/
COPY ./MosaicResidentInformationApi.Tests/MosaicResidentInformationApi.Tests.csproj ./MosaicResidentInformationApi.Tests/

RUN dotnet restore ./MosaicResidentInformationApi/MosaicResidentInformationApi.csproj
RUN dotnet restore ./MosaicResidentInformationApi.Tests/MosaicResidentInformationApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out MosaicResidentInformationApi.Tests/MosaicResidentInformationApi.Tests.csproj

CMD dotnet test
