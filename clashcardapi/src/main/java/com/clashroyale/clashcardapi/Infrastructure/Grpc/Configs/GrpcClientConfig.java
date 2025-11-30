package com.clashroyale.clashcardapi.Infrastructure.Grpc.Configs;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.clashroyale.clashcardapi.infrastructure.grpc.generated.PlayerServiceGrpc;

@Configuration
public class GrpcClientConfig {

    @Value("${grpc.player.host}")
    private String playerServiceHost;

    @Value("${grpc.player.port}")
    private int playerServicePort;

    @Bean
    public ManagedChannel playerServiceChannel() {
        return ManagedChannelBuilder
                .forAddress(playerServiceHost, playerServicePort)
                .usePlaintext()
                .build();
    }

    @Bean
    public PlayerServiceGrpc.PlayerServiceBlockingStub playerServiceStub(ManagedChannel playerServiceChannel) {
        return PlayerServiceGrpc.newBlockingStub(playerServiceChannel);
    }

    @Bean
    public PlayerServiceGrpc.PlayerServiceStub playerServiceAsyncStub(ManagedChannel playerServiceChannel) {
        return PlayerServiceGrpc.newStub(playerServiceChannel);
    }
}
