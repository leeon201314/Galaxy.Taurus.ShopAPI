FROM microsoft/dotnet:2.2-aspnetcore-runtime

COPY ./app /app/

RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so

RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app

EXPOSE 8082

ENTRYPOINT ["dotnet", "Galaxy.Taurus.AuthorizationAPI.dll"]

