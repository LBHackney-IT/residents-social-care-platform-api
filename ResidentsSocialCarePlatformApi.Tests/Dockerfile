FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ResidentsSocialCarePlatformApi.sln ./
COPY ./ResidentsSocialCarePlatformApi/ResidentsSocialCarePlatformApi.csproj ./ResidentsSocialCarePlatformApi/
COPY ./ResidentsSocialCarePlatformApi.Tests/ResidentsSocialCarePlatformApi.Tests.csproj ./ResidentsSocialCarePlatformApi.Tests/

RUN dotnet restore ./ResidentsSocialCarePlatformApi/ResidentsSocialCarePlatformApi.csproj
RUN dotnet restore ./ResidentsSocialCarePlatformApi.Tests/ResidentsSocialCarePlatformApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ResidentsSocialCarePlatformApi.Tests/ResidentsSocialCarePlatformApi.Tests.csproj

CMD dotnet test
