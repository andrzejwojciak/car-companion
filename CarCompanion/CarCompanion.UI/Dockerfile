FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY CarCompanion.UI/*.csproj ./CarCompanion.UI/
COPY CarCompanion.Shared/*.csproj ./CarCompanion.Shared/
RUN dotnet restore ./CarCompanion.UI/

# copy everything else and build app
COPY CarCompanion.UI/. ./CarCompanion.UI/.
COPY CarCompanion.Shared/. ./CarCompanion.Shared/.


RUN dotnet build ./CarCompanion.UI/. -c Release -o build
RUN dotnet publish ./CarCompanion.UI/. -c Release -o out

RUN ls 
RUN pwd

# We then get the base image for Nginx and set the 
# work directory 
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

RUN ls
# We'll copy all the contents from wwwroot in the publish
# folder into nginx/html for nginx to serve. The destination
# should be the same as what you set in the nginx.conf.x
COPY --from=build-env /app/out/wwwroot /usr/local/webapp/nginx/html
COPY CarCompanion.UI/nginx.conf /etc/nginx/nginx.conf
