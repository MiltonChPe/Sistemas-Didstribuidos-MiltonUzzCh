package com.miltonclashapi.clashroyale.contracts;


import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;

public interface ICardService {
    CardResponseDto GetCardById(Long id);
    CardResponseDto CreateCard(CreateCardDto createCardDto);
}