dotnet restore
dotnet lambda package --configuration release --framework netcoreapp3.1 --msbuild-parameters "/p:PublishReadyToRun=true /p:TieredCompilation=false /p:TieredCompilationQuickJit=false" --output-package bin/release/netcoreapp3.1/residents-social-care-platform-api.zip
