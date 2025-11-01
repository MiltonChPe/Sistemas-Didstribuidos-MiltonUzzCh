package com.clashroyale.clashcardapi.Mappers;

import com.clashroyale.clashcardapi.Models.Card;
import com.clashroyale.clashcardapi.Dtos.CardResponse;
import com.clashroyale.clashcardapi.Dtos.CreateCardRequest;

import com.clashroyale.clashcardapi.infrastructure.soap.generated.CardResponseDto;

import org.springframework.stereotype.Component;

@Component
public class CardMapper {


    public Card toModel(CardResponseDto soapResponse) {
        if (soapResponse == null) {
            return null;
        }
        Card cardModel = new Card();
        cardModel.setId(soapResponse.getId());
        cardModel.setName(soapResponse.getName());
        cardModel.setType(soapResponse.getType());
        cardModel.setRarity(soapResponse.getRarity());
        cardModel.setElixirCost(soapResponse.getElixirCost());
        return cardModel;
    }

   
    public com.clashroyale.clashcardapi.infrastructure.soap.generated.CreateCardRequest toCreateRequest(Card cardModel) {
        if (cardModel == null) {
            return null;
        }

        var soapRequest = new com.clashroyale.clashcardapi.infrastructure.soap.generated.CreateCardRequest();
        
        soapRequest.setName(cardModel.getName());
        soapRequest.setType(cardModel.getType());
        soapRequest.setRarity(cardModel.getRarity());
        soapRequest.setElixirCost(cardModel.getElixirCost());
        return soapRequest;
    }

    public Card toModel(CreateCardRequest dto) {
        if (dto == null) {
            return null;
        }
        Card cardModel = new Card();
        cardModel.setName(dto.getName());
        cardModel.setType(dto.getType());
        cardModel.setRarity(dto.getRarity());
        cardModel.setElixirCost(dto.getElixirCost());
        return cardModel;
    }

 
    public CardResponse toResponse(Card cardModel) {
        if (cardModel == null) {
            return null;
        }
        CardResponse dto = new CardResponse();
        dto.setId(cardModel.getId());
        dto.setName(cardModel.getName());
        dto.setType(cardModel.getType());
        dto.setRarity(cardModel.getRarity());
        dto.setElixirCost(cardModel.getElixirCost());
        return dto;
    }

    public Card toModel(com.clashroyale.clashcardapi.infrastructure.soap.generated.CardList.Card soapCard) {
        if (soapCard == null) {
            return null;
        }
        Card cardModel = new Card();
        cardModel.setId(soapCard.getId());
        cardModel.setName(soapCard.getName());
        cardModel.setType(soapCard.getType());
        cardModel.setRarity(soapCard.getRarity());
        cardModel.setElixirCost(soapCard.getElixirCost());
        return cardModel;
    }
    
}