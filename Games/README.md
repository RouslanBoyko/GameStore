# Game Store API

## Starting SQL SERVER

```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql --name msslq -d mcr.microsoft.com/mssql/server:2022-latest

```

## setting the connection string to secret manager
```
$sa_password = "[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost,1433; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"

```
