# Linux based .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the C# project file and restore dependencies
COPY ./MultiThreadFileAccess.csproj ./MultiThreadFileAccess.csproj
RUN dotnet restore

#Copy rest of files and build
COPY . ./
RUN dotnet publish MultiThreadFileAccess.csproj -c Release -o /app

# Step 3: Linux runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Linux package manager install timezone package
RUN apt-get update && apt-get install -y tzdata

# Sets to local timezone
ENV TZ=America/New_York
RUN ln -fs /usr/share/zoneinfo/$TZ /etc/localtime && dpkg-reconfigure -f noninteractive tzdata

#set environment variables in run time container
ENV ENVIRONMENT=DOCKER

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build container
COPY --from=build /app .

# Expose port 5000 (if needed, but optional for this case)
EXPOSE 5000

# Step 4: Define the entry point for the container
CMD ["dotnet", "MultiThreadFileAccess.dll"]
