package com.clashroyale.clashcardapi.Mappers;

import org.springframework.stereotype.Component;

import com.clashroyale.clashcardapi.Dtos.CreatePlayerRequest;
import com.clashroyale.clashcardapi.Dtos.PlayerResponse;
import com.clashroyale.clashcardapi.Dtos.UpdatePlayerRequest;
import com.clashroyale.clashcardapi.Models.Player;

@Component
public class PlayerMapper {

    public com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest 
            toGrpcCreateRequest(CreatePlayerRequest dto) {
        return com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest.newBuilder()
                .setName(dto.getName())
                .setLevel(dto.getLevel())
                .setTrophies(dto.getTrophies())
                .setClan(dto.getClan() != null ? dto.getClan() : "")
                .setBattlesPlayed(dto.getBattlesPlayed() != null ? dto.getBattlesPlayed() : 0)
                .setCoins(dto.getCoins() != null ? dto.getCoins() : 0)
                .setGems(dto.getGems() != null ? dto.getGems() : 0)
                .build();
    }

    public com.clashroyale.clashcardapi.infrastructure.grpc.generated.UpdatePlayerRequest 
            toGrpcUpdateRequest(UpdatePlayerRequest dto) {
        return com.clashroyale.clashcardapi.infrastructure.grpc.generated.UpdatePlayerRequest.newBuilder()
                .setId(dto.getId())
                .setName(dto.getName())
                .setLevel(dto.getLevel())
                .setTrophies(dto.getTrophies())
                .setClan(dto.getClan() != null ? dto.getClan() : "")
                .setBattlesPlayed(dto.getBattlesPlayed() != null ? dto.getBattlesPlayed() : 0)
                .setCoins(dto.getCoins() != null ? dto.getCoins() : 0)
                .setGems(dto.getGems() != null ? dto.getGems() : 0)
                .build();
    }

    public PlayerResponse toPlayerResponse(
            com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerResponse grpcPlayer) {
        return new PlayerResponse(
                grpcPlayer.getId(),
                grpcPlayer.getName(),
                grpcPlayer.getLevel(),
                grpcPlayer.getTrophies(),
                grpcPlayer.getClan(),
                grpcPlayer.getBattlesPlayed(),
                grpcPlayer.getCoins(),
                grpcPlayer.getGems()
        );
    }
    public Player toPlayer(
            com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerResponse grpcPlayer) {
        return new Player(
                grpcPlayer.getId(),
                grpcPlayer.getName(),
                grpcPlayer.getLevel(),
                grpcPlayer.getTrophies(),
                grpcPlayer.getClan(),
                grpcPlayer.getBattlesPlayed(),
                grpcPlayer.getCoins(),
                grpcPlayer.getGems()
        );
    }
}
