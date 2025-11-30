package infrastructure

type MongoDBSettings struct {
	ConnectionString     string
	DatabaseName         string
	PlayerCollectionName string
}

func NewMongoDBSettings() *MongoDBSettings {
	return &MongoDBSettings{
		ConnectionString:     "mongodb://player:playerpassword@mongodb:27017",
		DatabaseName:         "PlayerDB",
		PlayerCollectionName: "Players",
	}
}
