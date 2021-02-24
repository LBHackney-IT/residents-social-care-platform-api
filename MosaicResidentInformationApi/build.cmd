dotnet restore
dotnet lambda package --configuration release --framework netcoreapp3.1 --msbuild-parameters "/p:PublishReadyToRun=true /p:TieredCompilation=false /p:TieredCompilationQuickJit=false" --output-package bin/release/netcoreapp3.1/mosaic-resident-information-api.zip
