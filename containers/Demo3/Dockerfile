FROM ubuntu:20.04 as build
RUN apt-get update
RUN apt-get install -y build-essential
WORKDIR /app
COPY src/hello.c /app/

RUN gcc -o hello hello.c


FROM alpine:3.15
WORKDIR /app
COPY --from=build /app/hello .
ENTRYPOINT [ "/app/hello" ]
