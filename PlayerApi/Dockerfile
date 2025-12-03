FROM golang:1.23-alpine AS build

WORKDIR /app

RUN apk add --no-cache protobuf-dev git

COPY go.mod go.sum ./
RUN go mod download

COPY Protos/ ./Protos/
RUN go install google.golang.org/protobuf/cmd/protoc-gen-go@v1.35.2
RUN go install google.golang.org/grpc/cmd/protoc-gen-go-grpc@v1.5.1
RUN protoc --go_out=. --go_opt=paths=source_relative \
    --go-grpc_out=. --go-grpc_opt=paths=source_relative \
    Protos/player.proto

COPY . .

RUN CGO_ENABLED=0 GOOS=linux go build -o /playerapi main.go

FROM alpine:latest

WORKDIR /root/

COPY --from=build /playerapi .

EXPOSE 8080

CMD ["./playerapi"]
