package com.miltonclashapi.clashroyale.contracts;


import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.DeleteCardResponse;
import com.miltonclashapi.clashroyale.dtos.UpdateCardDto;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsRequest;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsResponse;


public interface ICardService {
    CardResponseDto GetCardById(Long id);
    CardResponseDto CreateCard(CreateCardDto createCardDto);
    DeleteCardResponse DeleteCard(Long id);
    CardResponseDto UpdateCard(UpdateCardDto updateCardDto);
    GetAllCardsResponse GetAllCards(GetAllCardsRequest request); 
}