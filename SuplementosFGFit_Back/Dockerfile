#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SuplementosFGFit_Back/SuplementosFGFit_Back.csproj", "SuplementosFGFit_Back/"]
RUN dotnet restore "SuplementosFGFit_Back/SuplementosFGFit_Back.csproj"
COPY . .
WORKDIR "/src/SuplementosFGFit_Back"
RUN dotnet build "SuplementosFGFit_Back.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuplementosFGFit_Back.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN ln -fs /usr/share/zoneinfo/America/Argentina /etc/localtime
RUN dpkg-reconfigure --frontend noninteractive tzdata

ENTRYPOINT ["dotnet", "SuplementosFGFit_Back.dll"]