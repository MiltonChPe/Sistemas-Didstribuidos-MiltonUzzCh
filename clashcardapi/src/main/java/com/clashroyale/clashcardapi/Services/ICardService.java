package com.clashroyale.clashcardapi.Services;

import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;

import com.clashroyale.clashcardapi.Dtos.PagedCardsResponse;
import com.clashroyale.clashcardapi.Dtos.PatchCardRequest;
import com.clashroyale.clashcardapi.Models.Card;

public interface ICardService {

    @Cacheable(value = "cards", key = "#id")
    Card getCardById(long id);

    Card createCard(Card card);

    @CacheEvict(value = "cards", key = "#id")
    void deleteCard(long id);

    @CacheEvict(value = "cards", key = "#id")
    Card updateCard(long id, Card card);

    @CacheEvict(value = "cards", key = "#id")
    Card patchCard(long id, PatchCardRequest patchRequest);

    @Cacheable(value = "cardsList", key = "#page + '-' + #pageSize + '-' + #sortBy + '-' + #sortDirection")
    PagedCardsResponse getAllCards(int page, int pageSize, String sortBy, String sortDirection);


}