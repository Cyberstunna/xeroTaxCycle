FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ./xeroTax/*.csproj ./xeroTaxApp/
WORKDIR /app/xeroTaxApp
RUN dotnet restore

# Copy everything else and build
COPY xeroTax/. ./
RUN dotnet publish -c release -o /web --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /web
COPY --from=build-env /web ./
#ENTRYPOINT [ "dotnet", "xeroTax.dll" ]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet xeroTax.dll
