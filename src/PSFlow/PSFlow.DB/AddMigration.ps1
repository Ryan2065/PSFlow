Param($MigrationName, $RemovePrevious)

Push-Location $PSScriptRoot

[System.Environment]::SetEnvironmentVariable("PSFlow_SQLConnectionString", "Server=Lab-CM.Home.Lab;Database=PSFlow;Trusted_Connection=True;")
[System.Environment]::SetEnvironmentVariable("PSFlow_SQLiteConnectionString", "Data Source=SQLDb.sqlite;")

if($RemovePrevious){
    dotnet build
    dotnet ef migrations remove --no-build --context "FlowContextSQL"
    dotnet ef migrations remove --no-build --context "FlowContextSqlite"
}

dotnet build
dotnet ef migrations add "$MigrationName" --no-build --context "FlowContextSQL" --output-dir "Migrations/SQL"
dotnet ef migrations add "$MigrationName" --no-build --context "FlowContextSqlite" --output-dir "Migrations/SQLite"

