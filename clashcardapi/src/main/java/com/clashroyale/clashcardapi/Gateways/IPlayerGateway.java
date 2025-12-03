package com.clashroyale.clashcardapi.Gateways;

import com.clashroyale.clashcardapi.Models.Player;
import java.util.List;

public interface IPlayerGateway {
    List<Player> createPlayers(List<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest> requests);
    Player getPlayerById(String id);
    void deletePlayer(String id);
    void updatePlayer(com.clashroyale.clashcardapi.infrastructure.grpc.generated.UpdatePlayerRequest request);
    List<Player> getPlayersByName(String name);
}
