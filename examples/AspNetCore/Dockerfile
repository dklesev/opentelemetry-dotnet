FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the entire example project directory to preserve all files and structure
COPY examples/AspNetCore/ ./

# Create a project file with all required system references
RUN echo '<Project Sdk="Microsoft.NET.Sdk.Web">\n\
  <PropertyGroup>\n\
    <TargetFramework>net9.0</TargetFramework>\n\
    <ImplicitUsings>enable</ImplicitUsings>\n\
    <Nullable>enable</Nullable>\n\
  </PropertyGroup>\n\
\n\
  <ItemGroup>\n\
    <FrameworkReference Include="Microsoft.AspNetCore.App" />\n\
    <PackageReference Include="OpenTelemetry" Version="1.11.2" />\n\
    <PackageReference Include="OpenTelemetry.Extensions.Hosting" Version="1.11.2" />\n\
    <PackageReference Include="OpenTelemetry.Exporter.Console" Version="1.11.2" />\n\
    <PackageReference Include="OpenTelemetry.Exporter.OpenTelemetryProtocol" Version="1.11.2" />\n\
    <PackageReference Include="OpenTelemetry.Exporter.Prometheus.AspNetCore" Version="1.11.2-beta.1" />\n\
    <PackageReference Include="OpenTelemetry.Exporter.Zipkin" Version="1.11.2" />\n\
    <PackageReference Include="OpenTelemetry.Instrumentation.AspNetCore" Version="1.11.1" />\n\
    <PackageReference Include="OpenTelemetry.Instrumentation.Http" Version="1.11.1" />\n\
    <PackageReference Include="OpenTelemetry.Instrumentation.Runtime" Version="1.11.1" />\n\
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />\n\
    <PackageReference Include="Microsoft.Extensions.Logging" Version="9.0.0" />\n\
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />\n\
    <PackageReference Include="Serilog.Expressions" Version="5.0.0" />\n\
    <PackageReference Include="Serilog.AspNetCore" Version="9.0.0" />\n\
    <PackageReference Include="Elastic.CommonSchema.Serilog" Version="8.12.3" />\n\
  </ItemGroup>\n\
</Project>' > Examples.AspNetCore.csproj

# Build and publish the application
RUN dotnet restore && \
    dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configure to use HTTP only (no HTTPS)
ENV ASPNETCORE_URLS=http://+:5000

# Expose port
EXPOSE 5000

ENTRYPOINT ["dotnet", "Examples.AspNetCore.dll"]
