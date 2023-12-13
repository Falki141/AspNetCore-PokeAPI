FROM ubuntu:22.04 AS builder

RUN apt-get update && apt-get install -y dotnet7 ca-certificates
WORKDIR /source
COPY . .
RUN dotnet publish -c Release -o /app --self-contained false

FROM ubuntu/dotnet-aspnet:7.0_edge
WORKDIR /app
COPY --from=builder /app ./

ENV PORT 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/AspNetPokeAPi.dll"]