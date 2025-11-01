package com.clashroyale.clashcardapi.Gateways;

import com.clashroyale.clashcardapi.Dtos.CardResponse;
import com.clashroyale.clashcardapi.Dtos.PagedCardsResponse;
import com.clashroyale.clashcardapi.Exceptions.CardAlreadyExistsException;
import com.clashroyale.clashcardapi.Exceptions.CardNotFoundException;
import com.clashroyale.clashcardapi.Exceptions.InvalidCardDataException;
import com.clashroyale.clashcardapi.Exceptions.SoapServiceException;
import com.clashroyale.clashcardapi.infrastructure.soap.generated.*; 
import com.clashroyale.clashcardapi.Mappers.CardMapper;
import com.clashroyale.clashcardapi.Models.Card;
import org.springframework.stereotype.Repository;
import org.springframework.ws.client.core.WebServiceTemplate;
import org.springframework.ws.soap.client.SoapFaultClientException;
import java.util.List;
import java.util.Optional;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


@Repository
public class CardGateway implements ICardGateway {

    private static final Logger log = LoggerFactory.getLogger(CardGateway.class);
    private final WebServiceTemplate webServiceTemplate;
    private final CardMapper cardMapper;

    public CardGateway(WebServiceTemplate webServiceTemplate, CardMapper cardMapper) {
        this.webServiceTemplate = webServiceTemplate;
        this.cardMapper = cardMapper;
    }

    @Override
    public Optional<Card> getCardById(long id) {
        log.info("enviado peticion SOAP para GetCardById con id: {}", id);

        GetCardByIdRequest request = new GetCardByIdRequest();
        request.setId(id);
        try {
            CardResponseDto response = (CardResponseDto) webServiceTemplate
                    .marshalSendAndReceive(request);

            if (response == null || response.getId() <= 0) {
                log.warn("SOAP dio respuesta vacía o sin ID para card: {}", id);
                return Optional.empty();
            }

            Card cardModel = cardMapper.toModel(response);
            return Optional.of(cardModel);

        } catch (SoapFaultClientException ex) {
            log.warn("Error SOAP en getCardById: {}", ex.getFaultStringOrReason());
            
            String errorMessage = ex.getFaultStringOrReason();
            if (errorMessage != null) {
                String lowerError = errorMessage.toLowerCase();
                if (lowerError.contains("no existe") || lowerError.contains("not found")) {
                    return Optional.empty();
                }
            }
            throw ex;
        }
    }

    @Override
    public Card createCard(Card card) {
        log.info("Enviando peticion SOAP para CreateCard con nombre: {}", card.getName());

        CreateCardRequest request = cardMapper.toCreateRequest(card);
        try {
            CardResponseDto response = (CardResponseDto) webServiceTemplate
                    .marshalSendAndReceive(request);
            
            if (response == null || response.getId() <= 0) {
                log.error("SOAP devolvio respuesta vacia o sin ID válido al crear card");
                throw new RuntimeException("No se pudo crear la tarjeta (respuesta SOAP vacia)");
            }
            
            return cardMapper.toModel(response);

        } catch (SoapFaultClientException ex) {
            log.warn("Error SOAP en createCard: {}", ex.getFaultStringOrReason());
            
            String errorMessage = ex.getFaultStringOrReason();
            
            if (errorMessage != null && 
                (errorMessage.contains("already exists") || 
                 errorMessage.contains("ya existe"))) {
                throw new CardAlreadyExistsException(card.getName());
            }
            
            if (errorMessage != null && 
                (errorMessage.contains("must be") || 
                 errorMessage.contains("invalid") ||
                 errorMessage.contains("elixir") ||
                 errorMessage.contains("rarity") ||
                 errorMessage.contains("type"))) {
                throw new InvalidCardDataException(errorMessage);
            }

            throw new SoapServiceException(
                "El servicio SOAP no está disponible o respondió con error: " + errorMessage,
                ex
            );
        }
    }

    @Override
    public void deleteCard(long id) {
        log.info("Enviando peticion SOAP para DeleteCard con id: {}", id);

        DeleteCardRequest request = new DeleteCardRequest();
        request.setId(id);

        try {
            DeleteCardResponse response = (DeleteCardResponse) webServiceTemplate
                    .marshalSendAndReceive(request);

            if (response == null || !response.isSuccess()) {
                String errorMsg = response != null ? response.getMessage() : "Unknown error";
                log.error("SOAP delete failed: {}", errorMsg);
                throw new CardNotFoundException(id);
            }

            log.info("Card deleted successfully: {}", response.getMessage());

        } catch (SoapFaultClientException ex) {
            log.warn("Error SOAP en deleteCard: {}", ex.getFaultStringOrReason());
            
            String errorMessage = ex.getFaultStringOrReason();

            if (errorMessage != null && 
                (errorMessage.toLowerCase().contains("not found") || 
                 errorMessage.toLowerCase().contains("no existe"))) {
                throw new CardNotFoundException(id);
            }

            throw new SoapServiceException(
                "El servicio SOAP no está disponible o respondió con error: " + errorMessage,
                ex
            );
        }
    }

    @Override
    public Card updateCard(long id, Card card) {
    log.info("Enviando peticion SOAP para UpdateCard con id: {} y nombre: {}", id, card.getName());

    UpdateCardRequest request = new UpdateCardRequest();
    request.setId(id);
    request.setName(card.getName());
    request.setType(card.getType());
    request.setRarity(card.getRarity());
    request.setElixirCost(card.getElixirCost());

    try {
        CardResponseDto response = (CardResponseDto) webServiceTemplate
                .marshalSendAndReceive(request);

        if (response == null || response.getId() <= 0) {
            log.error("SOAP devolcio respuesta vacia o sin ID valido al actualizar card");
            throw new CardNotFoundException(id);
        }

        Card updatedCard = cardMapper.toModel(response);
        
        log.info("Card actualizada exitosamente con id: {}", updatedCard.getId());
        return updatedCard;

    } catch (SoapFaultClientException ex) {
        log.warn("Error SOAP en updateCard: {}", ex.getFaultStringOrReason());
        
        String errorMessage = ex.getFaultStringOrReason();

        if (errorMessage != null && 
            (errorMessage.contains("not found") || 
             errorMessage.contains("no existe"))) {
            throw new CardNotFoundException(id);
        }
        
        if (errorMessage != null && 
            (errorMessage.contains("already exists") || 
             errorMessage.contains("ya existe"))) {
            throw new CardAlreadyExistsException(card.getName());
        }
        
        if (errorMessage != null && 
            (errorMessage.contains("must be") || 
             errorMessage.contains("invalid") ||
             errorMessage.contains("elixir") ||
             errorMessage.contains("rarity") ||
             errorMessage.contains("type"))) {
            throw new InvalidCardDataException(errorMessage);
        }

        throw new SoapServiceException(
            "El servicio SOAP no está disponible o respondió con error: " + errorMessage,
            ex
        );
        }
    }
    
    @Override

    public PagedCardsResponse getAllCards(int page, int pageSize, String sortBy, String sortDirection) {
    log.info("Enviando peticion SOAP para GetAllCards", 
             page, pageSize, sortBy, sortDirection);

    GetAllCardsRequest request = new GetAllCardsRequest();
    request.setPage(page);
    request.setPageSize(pageSize);
    request.setSortBy(sortBy);
    request.setSortDirection(sortDirection);

    try {
        GetAllCardsResponse response = (GetAllCardsResponse) webServiceTemplate
                .marshalSendAndReceive(request);

        if (response == null || response.getCards() == null) {
            log.warn("SOAP devolvio respuesta vacia para getAllCards");
            return new PagedCardsResponse(
                List.of(), 
                page, 
                pageSize, 
                0, 
                0L
            );
        }

        List<CardResponse> cardResponses = response.getCards().getCard().stream()
            .map(soapCard -> cardMapper.toResponse(cardMapper.toModel(soapCard)))
            .toList();

        PagedCardsResponse pagedResponse = new PagedCardsResponse(
            cardResponses,
            response.getCurrentPage(),
            response.getPageSize(),
            response.getTotalPages(),
            response.getTotalElements()
        );

        log.info("GetAllCards exitoso: {} cartas retornadas de {} totales", 
                 cardResponses.size(), response.getTotalElements());
        
        return pagedResponse;

    } catch (SoapFaultClientException ex) {
        log.error("Error SOAP en getAllCards: {}", ex.getFaultStringOrReason());
        
        throw new SoapServiceException(
            "El servicio SOAP no está disponible o respondió con error: " + ex.getFaultStringOrReason(),
            ex
        );
    }
}
}