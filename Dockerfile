FROM microsoft/dotnet:2-sdk

COPY ./mongodb-replica-set.csproj /app/
WORKDIR /app/
RUN dotnet --info
RUN dotnet restore
ADD ./ /app/
RUN dotnet publish -c DEBUG -o out
ENTRYPOINT ["dotnet", "out/mongodb-replica-set.dll"]