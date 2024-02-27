
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ToDoTask SchedulerAppTest.csproj", "./"]
RUN dotnet restore "ToDoTask SchedulerAppTest.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "ToDoTask SchedulerAppTest.csproj" -c Release -o /app/build
# Install EF Core CLI and add migrations in one step
RUN dotnet tool install --global dotnet-ef && \
    export PATH="$PATH:/root/.dotnet/tools" && \
    dotnet ef migrations add InitialCreate && \
    dotnet ef database update


FROM build AS publish
RUN dotnet publish "ToDoTask SchedulerAppTest.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoTask SchedulerAppTest.dll"]
