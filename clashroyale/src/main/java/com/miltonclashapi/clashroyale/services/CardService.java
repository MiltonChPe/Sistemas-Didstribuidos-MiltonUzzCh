package com.miltonclashapi.clashroyale.services;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.miltonclashapi.clashroyale.contracts.ICardService;
import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.infrastructure.entities.CardEntity;
import com.miltonclashapi.clashroyale.mappers.CardMapper;
import com.miltonclashapi.clashroyale.repositories.ICardRepository;

@Service
public class CardService implements ICardService {

    private final ICardRepository cardRepository;
    private final CardMapper cardMapper;
    
    @Autowired
    public CardService(ICardRepository cardRepository, CardMapper cardMapper) {
        this.cardRepository = cardRepository;
        this.cardMapper = cardMapper;
    }

    @Override
    public CardResponseDto GetCardById(Long id) {
    CardEntity foundCard = cardRepository.findById(id)
            .orElseThrow(() -> new IllegalArgumentException("Not found with id: " + id));

    return cardMapper.toResponseDto(foundCard);
    }

    @Override
    public CardResponseDto CreateCard(CreateCardDto createCardDto) {
        System.out.println("- Name: " + createCardDto.getName());
        System.out.println("- Type: " + createCardDto.getType());
        System.out.println("- Rarity: " + createCardDto.getRarity());
        System.out.println("- ElixirCost: " + createCardDto.getElixirCost());

        if (cardRepository.existsByNameIgnoreCase(createCardDto.getName())) {
            throw new IllegalArgumentException("A card with the name '" + createCardDto.getName() + "' already exists.");
        }

        CardEntity newCard = cardMapper.toEntity(createCardDto);
        CardEntity savedCard = cardRepository.save(newCard); 
        return cardMapper.toResponseDto(savedCard);
    }


}