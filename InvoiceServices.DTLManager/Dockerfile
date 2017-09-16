FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80 8080
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "InvoiceServices.DTLManager.dll"]
