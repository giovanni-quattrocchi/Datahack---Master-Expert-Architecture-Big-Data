FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000
ARG Entorno="Giovanni Quattrocchi"
ENV Entorno=${Entorno}
ENV ASPNETCORE_URLS=https://+:7007;http://+:7006
# Cambiar valor de MONGOHOST con nombre del servicio donde se encuentre MongoDB.
# El valor host.docker.internal se utiliza para conectar dos contenedores directamente con docker run.
ENV MONGOHOST=database-mongo 

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["PracticaEval01.csproj", "./"]
RUN dotnet restore "PracticaEval01.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PracticaEval01.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PracticaEval01.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https -ep /app/publish/aspnetapp.pfx -p PAssw0rd

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=PAssw0rd
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/aspnetapp.pfx
ENTRYPOINT ["dotnet", "PracticaEval01.dll"]
