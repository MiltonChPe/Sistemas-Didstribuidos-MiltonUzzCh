package documents

import "go.mongodb.org/mongo-driver/bson/primitive"

type PlayerDocument struct {
	ID            primitive.ObjectID `bson:"_id,omitempty"`
	Name          string             `bson:"name"`
	Level         int32              `bson:"level"`
	Trophies      int32              `bson:"trophies"`
	Clan          string             `bson:"clan"`
	BattlesPlayed int32              `bson:"battles_played"`
	Coins         int32              `bson:"coins"`
	Gems          int32              `bson:"gems"`
}
