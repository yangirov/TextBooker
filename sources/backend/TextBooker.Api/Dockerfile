FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

ARG DB_CONNECTION_STRING
ARG ASPNETCORE_ENVIRONMENT
ARG ASPNETCORE_URLS

ENV DB_CONNECTION_STRING=$DB_CONNECTION_STRING
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_URLS=$ASPNETCORE_URLS

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .
RUN dotnet restore

WORKDIR /src/TextBooker.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
HEALTHCHECK --interval=6s --timeout=10s --retries=3 CMD curl -sS 127.0.0.1/health || exit 1

ENTRYPOINT ["dotnet", "TextBooker.Api.dll"]
