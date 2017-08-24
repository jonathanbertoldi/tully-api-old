FROM microsoft/aspnetcore:1.1.1

WORKDIR /app
COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT ASPNETCORE_ENVIRONMENT=Production dotnet Tully.Api.dll