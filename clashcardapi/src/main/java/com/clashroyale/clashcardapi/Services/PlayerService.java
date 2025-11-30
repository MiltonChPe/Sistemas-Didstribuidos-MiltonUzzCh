package com.clashroyale.clashcardapi.Services;

import com.clashroyale.clashcardapi.Dtos.CreatePlayerRequest;
import com.clashroyale.clashcardapi.Dtos.PlayerResponse;
import com.clashroyale.clashcardapi.Dtos.UpdatePlayerRequest;
import com.clashroyale.clashcardapi.Gateways.IPlayerGateway;
import com.clashroyale.clashcardapi.Mappers.PlayerMapper;
import com.clashroyale.clashcardapi.Models.Player;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class PlayerService implements IPlayerService {

    private final IPlayerGateway playerGateway;
    private final PlayerMapper playerMapper;

    public PlayerService(IPlayerGateway playerGateway, PlayerMapper playerMapper) {
        this.playerGateway = playerGateway;
        this.playerMapper = playerMapper;
    }

    @Override
    public List<PlayerResponse> createPlayers(List<CreatePlayerRequest> requests) {

        List<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest> grpcRequests = 
            requests.stream()
                .map(playerMapper::toGrpcCreateRequest)
                .collect(Collectors.toList());

        List<Player> createdPlayers = playerGateway.createPlayers(grpcRequests);

        return createdPlayers.stream()
                .map(player -> new PlayerResponse(
                    player.getId(),
                    player.getName(),
                    player.getLevel(),
                    player.getTrophies(),
                    player.getClan(),
                    player.getBattlesPlayed(),
                    player.getCoins(),
                    player.getGems()
                ))
                .collect(Collectors.toList());
    }

    @Override
    public PlayerResponse getPlayerById(String id) {
        Player player = playerGateway.getPlayerById(id);
        
        return new PlayerResponse(
            player.getId(),
            player.getName(),
            player.getLevel(),
            player.getTrophies(),
            player.getClan(),
            player.getBattlesPlayed(),
            player.getCoins(),
            player.getGems()
        );
    }

    @Override
    public void deletePlayer(String id) {
        playerGateway.deletePlayer(id);
    }

    @Override
    public void updatePlayer(UpdatePlayerRequest request) {
        com.clashroyale.clashcardapi.infrastructure.grpc.generated.UpdatePlayerRequest grpcRequest = 
            playerMapper.toGrpcUpdateRequest(request);
        
        playerGateway.updatePlayer(grpcRequest);
    }

    @Override
    public List<PlayerResponse> getPlayersByName(String name) {
        List<Player> players = playerGateway.getPlayersByName(name);
        
        return players.stream()
                .map(player -> new PlayerResponse(
                    player.getId(),
                    player.getName(),
                    player.getLevel(),
                    player.getTrophies(),
                    player.getClan(),
                    player.getBattlesPlayed(),
                    player.getCoins(),
                    player.getGems()
                ))
                .collect(Collectors.toList());
    }
}
