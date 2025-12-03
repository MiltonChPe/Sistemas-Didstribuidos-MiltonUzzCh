package com.clashroyale.clashcardapi.Services;

import java.util.List;

import com.clashroyale.clashcardapi.Dtos.CreatePlayerRequest;
import com.clashroyale.clashcardapi.Dtos.PlayerResponse;
import com.clashroyale.clashcardapi.Dtos.UpdatePlayerRequest;

public interface IPlayerService {
    List<PlayerResponse> createPlayers(List<CreatePlayerRequest> requests);
    PlayerResponse getPlayerById(String id);
    void deletePlayer(String id);
    void updatePlayer(UpdatePlayerRequest request);
    List<PlayerResponse> getPlayersByName(String name);
}
