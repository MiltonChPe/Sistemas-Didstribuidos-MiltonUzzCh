package com.clashroyale.clashcardapi.Controllers;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

import com.clashroyale.clashcardapi.Dtos.CardResponse;
import com.clashroyale.clashcardapi.Dtos.CreateCardRequest;
import com.clashroyale.clashcardapi.Dtos.PagedCardsResponse;
import com.clashroyale.clashcardapi.Dtos.PatchCardRequest;
import com.clashroyale.clashcardapi.Exceptions.CardAlreadyExistsException;
import com.clashroyale.clashcardapi.Exceptions.CardNotFoundException;
import com.clashroyale.clashcardapi.Exceptions.InvalidCardDataException;
import com.clashroyale.clashcardapi.Exceptions.SoapServiceException;
import com.clashroyale.clashcardapi.Mappers.CardMapper;
import com.clashroyale.clashcardapi.Models.Card;
import com.clashroyale.clashcardapi.Services.ICardService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import jakarta.validation.Valid;

import java.net.URI;
import java.util.Map;


@RestController
@RequestMapping("/api/v1/cards")
public class CardController {

    private final ICardService cardService;
    private final CardMapper cardMapper;
    private static final Logger log = LoggerFactory.getLogger(CardController.class);

    public CardController(ICardService cardService, CardMapper cardMapper) {
        this.cardService = cardService;
        this.cardMapper = cardMapper;
    }

    @PreAuthorize("hasAuthority('read')")
    @GetMapping("/{id}")
    public ResponseEntity<?> getCardById(@PathVariable long id) {
        try {
            Card card = cardService.getCardById(id);
            CardResponse responseDto = cardMapper.toResponse(card);
            
            return ResponseEntity.ok(responseDto);

        } catch (CardNotFoundException ex) {
            return ResponseEntity.notFound().build();
            
        } catch (SoapServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "SOAP Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('write')")
    @PostMapping
    public ResponseEntity<?> createCard(@Valid @RequestBody CreateCardRequest request) {
        try {
            Card card = cardMapper.toModel(request);
            Card createdCard = cardService.createCard(card);
            CardResponse response = cardMapper.toResponse(createdCard);

            URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(createdCard.getId()).toUri();

            return ResponseEntity.created(location).body(response);

        } catch (CardAlreadyExistsException ex) {
            Map<String, String> error = Map.of("message", ex.getMessage());
            return new ResponseEntity<>(error, HttpStatus.CONFLICT);
            
        } catch (InvalidCardDataException ex) {
            Map<String, String> error = Map.of("message", ex.getMessage());
            return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
            
        } catch (SoapServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "SOAP Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }

    @PreAuthorize("hasAuthority('write')")
    @DeleteMapping("/{id}")
    public ResponseEntity<?> deleteCard(@PathVariable long id) {
        try {
            cardService.deleteCard(id);
            return ResponseEntity.noContent().build();

        } catch (CardNotFoundException ex) {
            return ResponseEntity.notFound().build();
            
        } catch (SoapServiceException ex) {
            Map<String, String> error = Map.of(
                "error", "SOAP Service Error",
                "message", ex.getMessage()
            );
            return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        }
    }


    @PreAuthorize("hasAuthority('write')")
    @PutMapping("/{id}")
    public ResponseEntity<?> updateCard(
            @PathVariable long id,
            @Valid @RequestBody CreateCardRequest request) {
        try {
            Card card = cardMapper.toModel(request);

            Card updatedCard = cardService.updateCard(id, card);

            CardResponse response = cardMapper.toResponse(updatedCard);

            return ResponseEntity.ok(response);

    } catch (CardNotFoundException ex) {
        return ResponseEntity.notFound().build();
        
    } catch (CardAlreadyExistsException ex) {
        Map<String, String> error = Map.of("message", ex.getMessage());
        return new ResponseEntity<>(error, HttpStatus.CONFLICT);
        
    } catch (InvalidCardDataException ex) {
        Map<String, String> error = Map.of("message", ex.getMessage());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
        
    } catch (SoapServiceException ex) {
        Map<String, String> error = Map.of(
            "error", "SOAP Service Error",
            "message", ex.getMessage()
        );
        return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
    }
}

    @PreAuthorize("hasAuthority('write')")
    @PatchMapping("/{id}")
    public ResponseEntity<?> patchCard(
        @PathVariable long id,
        @Valid @RequestBody PatchCardRequest patchRequest) {
    try {
        Card updatedCard = cardService.patchCard(id, patchRequest);
        CardResponse responseDto = cardMapper.toResponse(updatedCard);
        return ResponseEntity.ok(responseDto);

    } catch (CardNotFoundException ex) {
        return ResponseEntity.notFound().build();
        
    } catch (CardAlreadyExistsException ex) {
        Map<String, String> error = Map.of("message", ex.getMessage());
        return new ResponseEntity<>(error, HttpStatus.CONFLICT);
        
    } catch (InvalidCardDataException ex) {
        Map<String, String> error = Map.of("message", ex.getMessage());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
        
    } catch (SoapServiceException ex) {
        Map<String, String> error = Map.of(
            "error", "SOAP Service Error",
            "message", ex.getMessage()
        );
        return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
    }
}   

    @PreAuthorize("hasAuthority('read')")
    @GetMapping
    public ResponseEntity<?> getAllCards(
        @RequestParam(defaultValue = "0") int page,
        @RequestParam(defaultValue = "10") int pageSize,
        @RequestParam(defaultValue = "id") String sortBy,
        @RequestParam(defaultValue = "ASC") String sortDirection) {
    
    log.info("Controller: GET /api/v1/cards - page: {}, size: {}, sortBy: {}, direction: {}", 
             page, pageSize, sortBy, sortDirection);
    
    try {
        PagedCardsResponse response = cardService.getAllCards(page, pageSize, sortBy, sortDirection);
        return ResponseEntity.ok(response);

    } catch (SoapServiceException ex) {
        log.error("Error SOAP al obtener cartas: {}", ex.getMessage());
        Map<String, String> error = Map.of(
            "error", "SOAP Service Error",
            "message", ex.getMessage()
        );
        return new ResponseEntity<>(error, HttpStatus.BAD_GATEWAY);
        
    } catch (Exception ex) {
        log.error("Error inesperado al obtener cartas: {}", ex.getMessage());
        return ResponseEntity.internalServerError().build();
    }
}

}