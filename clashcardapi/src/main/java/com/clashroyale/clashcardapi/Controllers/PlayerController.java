package com.clashroyale.clashcardapi.Controllers;

import com.clashroyale.clashcardapi.Dtos.CreatePlayerRequest;
import com.clashroyale.clashcardapi.Dtos.PlayerResponse;
import com.clashroyale.clashcardapi.Dtos.UpdatePlayerRequest;
import com.clashroyale.clashcardapi.Exceptions.GrpcServiceException;
import com.clashroyale.clashcardapi.Exceptions.InvalidPlayerDataException;
import com.clashroyale.clashcardapi.Exceptions.PlayerAlreadyExistsException;
import com.clashroyale.clashcardapi.Exceptions.PlayerNotFoundException;
import com.clashroyale.clashcardapi.Services.IPlayerService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/players")
public class PlayerController {

    private final IPlayerService playerService;

    public PlayerController(IPlayerService playerService) {
        this.playerService = playerService;
    }

    @PreAuthorize("hasAuthority('write')")
    @PostMapping
    public ResponseEntity<?> createPlayers(@Valid @RequestBody List<CreatePlayerRequest> requests) {
        try {
            List<PlayerResponse> createdPlayers = playerService.createPlayers(requests);
            return new ResponseEntity<>(createdPlayers, HttpStatus.CREATED);
            
        } catch (InvalidPlayerDataException ex) {
            Map<String, String> error = Map.of(
                "error", "Invalid Data",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (PlayerAlreadyExistsException ex) {
            Map<String, String> error = Map.of(
                "error", "Player Already Exists",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.CONFLICT);
            
        } catch (GrpcServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "gRPC Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('read')")
    @GetMapping("/{id}")
    public ResponseEntity<?> getPlayerById(@PathVariable String id) {
        try {
            PlayerResponse player = playerService.getPlayerById(id);
            return ResponseEntity.ok(player);
            
        } catch (PlayerNotFoundException ex) {
            return ResponseEntity.notFound().build();
            
        } catch (InvalidPlayerDataException ex) {
            Map<String, String> error = Map.of(
                "error", "Invalid Data",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (GrpcServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "gRPC Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('read')")
    @GetMapping("/search")
    public ResponseEntity<?> getPlayersByName(@RequestParam String name) {
        try {
            List<PlayerResponse> players = playerService.getPlayersByName(name);
            return ResponseEntity.ok(players);
            
        } catch (InvalidPlayerDataException ex) {
            Map<String, String> error = Map.of(
                "error", "Invalid Data",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (GrpcServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "gRPC Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('write')")
    @PutMapping
    public ResponseEntity<?> updatePlayer(@Valid @RequestBody UpdatePlayerRequest request) {
        try {
            playerService.updatePlayer(request);
            return ResponseEntity.noContent().build();
            
        } catch (PlayerNotFoundException ex) {
            return ResponseEntity.notFound().build();
            
        } catch (InvalidPlayerDataException ex) {
            Map<String, String> error = Map.of(
                "error", "Invalid Data",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (PlayerAlreadyExistsException ex) {
            Map<String, String> error = Map.of(
                "error", "Player Already Exists",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.CONFLICT);
            
        } catch (GrpcServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "gRPC Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('write')")
    @DeleteMapping("/{id}")
    public ResponseEntity<?> deletePlayer(@PathVariable String id) {
        try {
            playerService.deletePlayer(id);
            return ResponseEntity.noContent().build();
            
        } catch (PlayerNotFoundException ex) {
            return ResponseEntity.notFound().build();
            
        } catch (InvalidPlayerDataException ex) {
            Map<String, String> error = Map.of(
                "error", "Invalid Data",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (GrpcServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "gRPC Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }
}
