package main

import (
	"context"
	"fmt"
	"log"
	"net"
	"os"
	infrastructure "playerapi/Infrastructure"
	pb "playerapi/Pb"
	repositories "playerapi/Repositories"
	services "playerapi/Services"
	"time"

	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
	"google.golang.org/grpc"
)

func main() {
	settings := infrastructure.NewMongoDBSettings()

	if conexion := os.Getenv("MONGODB_CONNECTION_STRING"); conexion != "" {
		settings.ConnectionString = conexion
	}

	contexto, cancelar := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancelar()

	cliente, err := mongo.Connect(contexto, options.Client().ApplyURI(settings.ConnectionString))
	if err != nil {
		log.Fatalf("Failed to connect to MongoDB: %v", err)
	}
	defer func() {
		if err = cliente.Disconnect(context.Background()); err != nil {
			log.Printf("Error disconnecting from MongoDB: %v", err)
		}
	}()

	db := cliente.Database(settings.DatabaseName)

	repository := repositories.NewPlayerRepository(db, settings)
	service := services.NewPlayerService(repository)

	servidordelgrpc := grpc.NewServer()
	pb.RegisterPlayerServiceServer(servidordelgrpc, service)

	puerto := os.Getenv("PORT")
	if puerto == "" {
		puerto = "8080"
	}

	listener, err := net.Listen("tcp", fmt.Sprintf(":%s", puerto))
	if err != nil {
		log.Fatalf("Failed to listen: %v", err)
	}

	log.Printf("Server listening at %v", listener.Addr())
	if err := servidordelgrpc.Serve(listener); err != nil {
		log.Fatalf("Failed to serve: %v", err)
	}
}
