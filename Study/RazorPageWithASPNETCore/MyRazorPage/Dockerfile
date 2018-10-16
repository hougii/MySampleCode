FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 1079
EXPOSE 44385

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY MyRazorPage/MyRazorPage.csproj MyRazorPage/
RUN dotnet restore MyRazorPage/MyRazorPage.csproj
COPY . .
WORKDIR /src/MyRazorPage
RUN dotnet build MyRazorPage.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MyRazorPage.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyRazorPage.dll"]
