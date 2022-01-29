build:
	dotnet build
clean:
	dotnet clean
restore:
	dotnet restore
watch:
	dotnet watch --project Application/Application.csproj run
start:
	dotnet run --project Application/Application.csproj
