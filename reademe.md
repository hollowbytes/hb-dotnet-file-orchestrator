## EF Core
### Generating Migrations
`dotnet ef migrations add initial --startup-project src/HbDotnetFileOrchestrator --project src/HbDotnetFileOrchestrator.Infrastructure --output-dir Sql/Migrations`

### Apply
`dotnet ef database update --startup-project src/HbDotnetFileOrchestrator --project src/HbDotnetFileOrchestrator.Infrastructure`