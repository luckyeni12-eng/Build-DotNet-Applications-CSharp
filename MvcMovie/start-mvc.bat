@echo off
echo Starting SQL Server LocalDB...
sqllocaldb start MSSQLLocalDB

echo.
echo Starting MvcMovie...
dotnet run