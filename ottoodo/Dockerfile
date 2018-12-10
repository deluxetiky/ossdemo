FROM microsoft/dotnet:2.1-sdk  AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore OttooDo/OttooDo.csproj
# Copy everything else and build
COPY . ./
RUN dotnet publish OttooDo/OttooDo.csproj -c Release -o /app/out
# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OttooDo.dll"]

