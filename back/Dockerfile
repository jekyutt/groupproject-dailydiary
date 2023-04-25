# Define base image
FROM mcr.microsoft.com/dotnet/sdk:5.0.201-alpine3.12 AS builder


# Copy project files
WORKDIR /source
COPY ./*.sln .
COPY ./DailyDiary/*.csproj ./DailyDiary/

# Restore
RUN dotnet restore

# Copy all source code
COPY . .

# Publish

WORKDIR /source/DailyDiary
RUN dotnet publish "DailyDiary.csproj" -c Release -o /publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.4-alpine3.12
WORKDIR /app
COPY --from=builder /publish .
ENTRYPOINT ["dotnet", "DailyDiary.dll"]
