package com.clashroyale.clashcardapi.Services;

import com.clashroyale.clashcardapi.Dtos.PagedCardsResponse;
import com.clashroyale.clashcardapi.Dtos.PatchCardRequest;
import com.clashroyale.clashcardapi.Exceptions.CardNotFoundException;
import com.clashroyale.clashcardapi.Gateways.ICardGateway;
import com.clashroyale.clashcardapi.Models.Card;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class CardService implements ICardService {

    private static final Logger log = LoggerFactory.getLogger(CardService.class);
    private final ICardGateway cardGateway;

    public CardService(ICardGateway cardGateway) {
        this.cardGateway = cardGateway;
    }


    @Override
    public Card getCardById(long id) {
        log.info("Service: busca carta con id: {}", id);

        Optional<Card> optionalCard = cardGateway.getCardById(id);

        if (optionalCard.isEmpty()) {
            throw new CardNotFoundException(id);
        }
        return optionalCard.get();
    }

    @Override
    public Card createCard(Card card) {
        log.info("Service: intenta crear carta con nombre: {}", card.getName());
        return cardGateway.createCard(card);
    }

    @Override
    public void deleteCard(long id) {
        log.info("Service: intenta eliminar carta con id: {}", id);
        cardGateway.deleteCard(id);

        log.info("Service: Carta con id {} eliminada correctamente", id);
    }

    @Override
    public Card updateCard(long id, Card card) {
    log.info("Service: Intentando actualizar carta con id: {}", id);
    card.setId(id);
    Card updatedCard = cardGateway.updateCard(id, card);

    log.info("Service: Carta con id {} actualizada correctamente", id);
    return updatedCard;
    
}

    @Override
    public Card patchCard(long id, PatchCardRequest patchRequest) {
    log.info("Service: intentando PATCH en carta con id: {}", id);

    Card card = getCardById(id); 

    if (patchRequest.getName() != null) {
        card.setName(patchRequest.getName());
    }
    if (patchRequest.getType() != null) {
        card.setType(patchRequest.getType());
    }
    if (patchRequest.getRarity() != null) {
        card.setRarity(patchRequest.getRarity());
    }
    if (patchRequest.getElixirCost() != null) {
        card.setElixirCost(patchRequest.getElixirCost());
    }

    Card updatedCard = cardGateway.updateCard(id, card);

    log.info("Service: Carta con id {} actualizada parcialmente correctamente", id);
    return updatedCard;
    }

    @Override
    public PagedCardsResponse getAllCards(int page, int pageSize, String sortBy, String sortDirection) {
    log.info("service:  todas las cartas - page: {}, size: {}", page, pageSize);

    if (page < 0) {
        page = 0;
    }
    if (pageSize <= 0 || pageSize > 100) {
        pageSize = 10; 
    }
    PagedCardsResponse response = cardGateway.getAllCards(page, pageSize, sortBy, sortDirection);

    log.info("service: {} cartas obtenidas de {} totales",
             response.getCards().size(), response.getTotalCards());
    return response;
    } 
}