FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /source
COPY PaymentService/. .
RUN dotnet build -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "/app/PaymentService.WebApi.dll"]
