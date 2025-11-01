package com.clashroyale.clashcardapi.Gateways;

import com.clashroyale.clashcardapi.Models.Card;
import java.util.Optional;
import com.clashroyale.clashcardapi.Dtos.PagedCardsResponse;

public interface ICardGateway {

    Optional<Card> getCardById(long id);

    Card createCard(Card card);

    void deleteCard(long id);

    Card updateCard(long id, Card card);

    PagedCardsResponse getAllCards(int page, int pageSize, String sortBy, String sortDirection);
}