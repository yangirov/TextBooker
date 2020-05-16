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
COPY ["TextBooker.Api/TextBooker.Api.csproj", "TextBooker.Api/"]
COPY ["TextBooker.BusinessLogic/TextBooker.BusinessLogic.csproj", "TextBooker.BusinessLogic/"]
COPY ["TextBooker.DataAccess/TextBooker.DataAccess.csproj", "TextBooker.DataAccess/"]
COPY ["TextBooker.Contracts/TextBooker.Contracts.csproj", "TextBooker.Contracts/"]
RUN dotnet restore "TextBooker.Api/TextBooker.Api.csproj"

COPY . .
WORKDIR "/src/TextBooker.Api"
#RUN dotnet build "TextBooker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextBooker.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextBooker.Api.dll"]
