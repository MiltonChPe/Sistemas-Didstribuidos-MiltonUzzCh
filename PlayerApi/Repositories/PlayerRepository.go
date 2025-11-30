package repositories

import (
	"context"
	infrastructure "playerapi/Infrastructure"
	documents "playerapi/Infrastructure/Documents"
	mappers "playerapi/Mappers"
	models "playerapi/Models"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/bson/primitive"
	"go.mongodb.org/mongo-driver/mongo"
)

type PlayerRepository struct {
	collection *mongo.Collection
}

func NewPlayerRepository(db *mongo.Database, settings *infrastructure.MongoDBSettings) *PlayerRepository {
	return &PlayerRepository{
		collection: db.Collection(settings.PlayerCollectionName),
	}
}

func (repo *PlayerRepository) CreateAsync(_context context.Context, player *models.Player) (*models.Player, error) {
	playerDoc := mappers.ToDocument(player)
	playertocreate, err := repo.collection.InsertOne(_context, playerDoc)
	if err != nil {
		return nil, err
	}
	playerDoc.ID = playertocreate.InsertedID.(primitive.ObjectID)
	return mappers.ToDomain(playerDoc), nil
}

func (repo *PlayerRepository) GetByNameAsync(_context context.Context, name string) ([]*models.Player, error) {
	filtrar := bson.M{"name": bson.M{"$regex": name, "$options": "i"}}
	recorrido, err := repo.collection.Find(_context, filtrar)
	if err != nil {
		return nil, err
	}
	defer recorrido.Close(_context)
	var playerDocs []*documents.PlayerDocument
	if err = recorrido.All(_context, &playerDocs); err != nil {
		return nil, err
	}

	players := make([]*models.Player, len(playerDocs))
	for i, playerDoc := range playerDocs {
		players[i] = mappers.ToDomain(playerDoc)
	}
	return players, nil
}

func (repo *PlayerRepository) GetByIDAsync(_context context.Context, id string) (*models.Player, error) {
	objectID, err := primitive.ObjectIDFromHex(id)
	if err != nil {
		return nil, err
	}

	var playerDoc documents.PlayerDocument
	err = repo.collection.FindOne(_context, bson.M{"_id": objectID}).Decode(&playerDoc)
	if err != nil {
		return nil, err
	}
	return mappers.ToDomain(&playerDoc), nil
}

func (repo *PlayerRepository) DeleteAsync(_context context.Context, id string) error {
	objectID, err := primitive.ObjectIDFromHex(id)
	if err != nil {
		return err
	}

	_, err = repo.collection.DeleteOne(_context, bson.M{"_id": objectID})
	return err
}

func (repo *PlayerRepository) UpdateAsync(_context context.Context, player *models.Player) error {
	objectID, err := primitive.ObjectIDFromHex(player.ID)
	if err != nil {
		return err
	}

	updates := bson.M{
		"$set": bson.M{
			"name":           player.Name,
			"level":          player.Level,
			"trophies":       player.Trophies,
			"clan":           player.Clan,
			"battles_played": player.BattlesPlayed,
			"coins":          player.Coins,
			"gems":           player.Gems,
		},
	}

	_, err = repo.collection.UpdateOne(_context, bson.M{"_id": objectID}, updates)
	return err
}
