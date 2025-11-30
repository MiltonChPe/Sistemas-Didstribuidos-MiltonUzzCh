package mappers

import (
	documents "playerapi/Infrastructure/Documents"
	models "playerapi/Models"
	pb "playerapi/Pb"

	"go.mongodb.org/mongo-driver/bson/primitive"
)

func ToModel(req *pb.CreatePlayerRequest) *models.Player {
	return &models.Player{
		Name:          req.Name,
		Level:         req.Level,
		Trophies:      req.Trophies,
		Clan:          req.Clan,
		BattlesPlayed: req.BattlesPlayed,
		Coins:         req.Coins,
		Gems:          req.Gems,
	}
}

func ToResponse(player *models.Player) *pb.Player {
	return &pb.Player{
		Id:            player.ID,
		Name:          player.Name,
		Level:         player.Level,
		Trophies:      player.Trophies,
		Clan:          player.Clan,
		BattlesPlayed: player.BattlesPlayed,
		Coins:         player.Coins,
		Gems:          player.Gems,
	}
}

func ToPlayerResponse(player *models.Player) *pb.PlayerResponse {
	return &pb.PlayerResponse{
		Id:            player.ID,
		Name:          player.Name,
		Level:         player.Level,
		Trophies:      player.Trophies,
		Clan:          player.Clan,
		BattlesPlayed: player.BattlesPlayed,
		Coins:         player.Coins,
		Gems:          player.Gems,
	}
}

func UpdateRequestToModel(req *pb.UpdatePlayerRequest) *models.Player {
	return &models.Player{
		ID:            req.Id,
		Name:          req.Name,
		Level:         req.Level,
		Trophies:      req.Trophies,
		Clan:          req.Clan,
		BattlesPlayed: req.BattlesPlayed,
		Coins:         req.Coins,
		Gems:          req.Gems,
	}
}

func ToDocument(player *models.Player) *documents.PlayerDocument {
	doc := &documents.PlayerDocument{
		Name:          player.Name,
		Level:         player.Level,
		Trophies:      player.Trophies,
		Clan:          player.Clan,
		BattlesPlayed: player.BattlesPlayed,
		Coins:         player.Coins,
		Gems:          player.Gems,
	}

	if player.ID != "" {
		if objectID, err := primitive.ObjectIDFromHex(player.ID); err == nil {
			doc.ID = objectID
		}
	}

	return doc
}

func ToDomain(doc *documents.PlayerDocument) *models.Player {
	return &models.Player{
		ID:            doc.ID.Hex(),
		Name:          doc.Name,
		Level:         doc.Level,
		Trophies:      doc.Trophies,
		Clan:          doc.Clan,
		BattlesPlayed: doc.BattlesPlayed,
		Coins:         doc.Coins,
		Gems:          doc.Gems,
	}
}
