FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base
WORKDIR /app
ENTRYPOINT [ "/app/TechChallenge.Api" ]
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy as build
WORKDIR /app
COPY ./ ./
RUN dotnet restore -r linux-x64 --property:Configuration=Release
RUN dotnet publish  -r linux-x64 --no-restore -c Release --property:PublishDir=/app/out --property:ErrorOnDuplicatePublishOutputFiles=false

FROM base
COPY --from=build /app/out .
