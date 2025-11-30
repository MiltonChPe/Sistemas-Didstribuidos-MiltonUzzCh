package com.clashroyale.clashcardapi.Gateways;

import com.clashroyale.clashcardapi.Exceptions.GrpcServiceException;
import com.clashroyale.clashcardapi.Exceptions.InvalidPlayerDataException;
import com.clashroyale.clashcardapi.Exceptions.PlayerAlreadyExistsException;
import com.clashroyale.clashcardapi.Exceptions.PlayerNotFoundException;
import com.clashroyale.clashcardapi.Mappers.PlayerMapper;
import com.clashroyale.clashcardapi.Models.Player;
import com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerByIdRequest;
import com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerByNameRequest;
import com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerServiceGrpc;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

@Component
public class PlayerGateway implements IPlayerGateway {

    private final PlayerServiceGrpc.PlayerServiceBlockingStub playerServiceStub;
    private final PlayerServiceGrpc.PlayerServiceStub playerServiceAsyncStub;
    private final PlayerMapper playerMapper;

    public PlayerGateway(PlayerServiceGrpc.PlayerServiceBlockingStub playerServiceStub,
                        PlayerMapper playerMapper) {
        this.playerServiceStub = playerServiceStub;
        this.playerServiceAsyncStub = PlayerServiceGrpc.newStub(playerServiceStub.getChannel());
        this.playerMapper = playerMapper;
    }

    @Override
    public List<Player> createPlayers(List<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest> requests) {
        List<Player> createdPlayers = new ArrayList<>();
        CountDownLatch latch = new CountDownLatch(1);

        StreamObserver<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerResponse> responseObserver = 
            new StreamObserver<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerResponse>() {
                @Override
                public void onNext(com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerResponse response) {
                    response.getPlayersList().forEach(grpcPlayer -> {
                        createdPlayers.add(playerMapper.toPlayer(
                            com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerResponse.newBuilder()
                                .setId(grpcPlayer.getId())
                                .setName(grpcPlayer.getName())
                                .setLevel(grpcPlayer.getLevel())
                                .setTrophies(grpcPlayer.getTrophies())
                                .setClan(grpcPlayer.getClan())
                                .setBattlesPlayed(grpcPlayer.getBattlesPlayed())
                                .setCoins(grpcPlayer.getCoins())
                                .setGems(grpcPlayer.getGems())
                                .build()
                        ));
                    });
                }

                @Override
                public void onError(Throwable t) {
                    latch.countDown();
                }

                @Override
                public void onCompleted() {
                    latch.countDown();
                }
            };

        StreamObserver<com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest> requestObserver;
        
        try {
            requestObserver = playerServiceAsyncStub.createPlayers(responseObserver);
            
            for (com.clashroyale.clashcardapi.infrastructure.grpc.generated.CreatePlayerRequest request : requests) {
                requestObserver.onNext(request);
            }
            requestObserver.onCompleted();
            latch.await(30, TimeUnit.SECONDS);
            
        } catch (StatusRuntimeException ex) {
            throw new GrpcServiceException("No se puede conectar al servicio gRPC de Player: " + ex.getMessage(), ex);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            throw new RuntimeException("Error al crear jugadores", e);
        } catch (Exception ex) {
            throw new GrpcServiceException("Error inesperado al comunicarse con el servicio gRPC", ex);
        }

        return createdPlayers;
    }

    @Override
    public Player getPlayerById(String id) {
        try {
            PlayerByIdRequest request = PlayerByIdRequest.newBuilder()
                    .setId(id)
                    .build();
            
            com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerResponse response = 
                playerServiceStub.getPlayerById(request);
            
            return playerMapper.toPlayer(response);
            
        } catch (StatusRuntimeException ex) {
            Status.Code code = ex.getStatus().getCode();
            switch (code) {
                case NOT_FOUND:
                    throw new PlayerNotFoundException("Jugador con ID " + id + " no encontrado");
                case INVALID_ARGUMENT:
                    throw new InvalidPlayerDataException("ID de jugador inválido: " + id);
                case UNAVAILABLE:
                case INTERNAL:
                    throw new GrpcServiceException("No se puede conectar al servicio gRPC de Player: " + ex.getMessage(), ex);
                default:
                    throw new GrpcServiceException("Error en servicio gRPC: " + ex.getMessage(), ex);
            }
        } catch (Exception ex) {
            throw new GrpcServiceException("Error inesperado al comunicarse con el servicio gRPC", ex);
        }
    }

    @Override
    public void deletePlayer(String id) {
        try {
            PlayerByIdRequest request = PlayerByIdRequest.newBuilder()
                    .setId(id)
                    .build();
            
            playerServiceStub.deletePlayer(request);
            
        } catch (StatusRuntimeException ex) {
            Status.Code code = ex.getStatus().getCode();
            switch (code) {
                case NOT_FOUND:
                    throw new PlayerNotFoundException("Jugador con ID " + id + " no encontrado");
                case INVALID_ARGUMENT:
                    throw new InvalidPlayerDataException("ID de jugador inválido: " + id);
                case UNAVAILABLE:
                case INTERNAL:
                    throw new GrpcServiceException("No se puede conectar al servicio gRPC de Player: " + ex.getMessage(), ex);
                default:
                    throw new GrpcServiceException("Error en servicio gRPC: " + ex.getMessage(), ex);
            }
        } catch (Exception ex) {
            throw new GrpcServiceException("Error inesperado al comunicarse con el servicio gRPC", ex);
        }
    }

    @Override
    public void updatePlayer(com.clashroyale.clashcardapi.infrastructure.grpc.generated.UpdatePlayerRequest request) {
        try {
            playerServiceStub.updatePlayer(request);
            
        } catch (StatusRuntimeException ex) {
            Status.Code code = ex.getStatus().getCode();
            switch (code) {
                case NOT_FOUND:
                    throw new PlayerNotFoundException("Jugador no encontrado");
                case INVALID_ARGUMENT:
                    throw new InvalidPlayerDataException("Datos de jugador inválidos");
                case ALREADY_EXISTS:
                    throw new PlayerAlreadyExistsException("El jugador ya existe");
                case UNAVAILABLE:
                case INTERNAL:
                    throw new GrpcServiceException("No se puede conectar al servicio gRPC de Player: " + ex.getMessage(), ex);
                default:
                    throw new GrpcServiceException("Error en servicio gRPC: " + ex.getMessage(), ex);
            }
        } catch (Exception ex) {
            throw new GrpcServiceException("Error inesperado al comunicarse con el servicio gRPC", ex);
        }
    }

    @Override
    public List<Player> getPlayersByName(String name) {
        try {
            List<Player> players = new ArrayList<>();
            
            PlayerByNameRequest request = PlayerByNameRequest.newBuilder()
                    .setName(name)
                    .build();
            
            Iterator<com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerResponse> responseIterator = 
                playerServiceStub.getAllPlayersByName(request);
            
            responseIterator.forEachRemaining(grpcPlayer -> 
                players.add(playerMapper.toPlayer(grpcPlayer))
            );
            
            return players;
            
        } catch (StatusRuntimeException ex) {
            Status.Code code = ex.getStatus().getCode();
            switch (code) {
                case INVALID_ARGUMENT:
                    throw new InvalidPlayerDataException("Nombre de jugador inválido: " + name);
                case UNAVAILABLE:
                case INTERNAL:
                    throw new GrpcServiceException("No se puede conectar al servicio gRPC de Player: " + ex.getMessage(), ex);
                default:
                    throw new GrpcServiceException("Error en servicio gRPC: " + ex.getMessage(), ex);
            }
        } catch (Exception ex) {
            throw new GrpcServiceException("Error inesperado al comunicarse con el servicio gRPC", ex);
        }
    }
}
