package com.miltonclashapi.clashroyale.mappers;

import org.springframework.stereotype.Component;

import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.UpdateCardDto;
import com.miltonclashapi.clashroyale.infrastructure.entities.CardEntity;
import com.miltonclashapi.clashroyale.models.Card;

@Component
public class CardMapper {

    public CardResponseDto toResponseDto(CardEntity entity) {
        if (entity == null) return null;
        return new CardResponseDto(
            entity.getId(),
            entity.getName(),
            entity.getType(),
            entity.getRarity(),
            entity.getElixirCost()
        );
    }

    public CardEntity toEntity(CreateCardDto dto) {
        if (dto == null) return null;
        return new CardEntity(
            dto.getName(),
            dto.getType(),
            dto.getRarity(),
            dto.getElixirCost()
        );
    }

    public Card toCard(CardEntity entity) {
        if (entity == null) return null;
        return new Card(
            entity.getId(),
            entity.getName(),
            entity.getType(),
            entity.getRarity(),
            entity.getElixirCost()
        );
    }

    public CardEntity updateEntity(CardEntity existingEntity, UpdateCardDto dto) {
        if (dto == null || existingEntity == null) return existingEntity;
        
        existingEntity.setName(dto.getName());
        existingEntity.setType(dto.getType());
        existingEntity.setRarity(dto.getRarity());
        existingEntity.setElixirCost(dto.getElixirCost());
        
        return existingEntity;
    }
}