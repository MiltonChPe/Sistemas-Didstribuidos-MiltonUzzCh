package com.miltonclashapi.clashroyale.endpoints;


import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;

import com.miltonclashapi.clashroyale.Validators.CardValidator;
import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.DeleteCardRequest;
import com.miltonclashapi.clashroyale.dtos.DeleteCardResponse;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsRequest;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsResponse; 
import com.miltonclashapi.clashroyale.dtos.GetCardByIdRequest;
import com.miltonclashapi.clashroyale.dtos.UpdateCardDto;
import com.miltonclashapi.clashroyale.services.CardService;

@Endpoint
public class CardEndpoint {

    private static final String NAMESPACE_URI = "http://www.miltonclashapi.com/api/cards";
    
    private final CardService cardService;
    
    public CardEndpoint(CardService cardService) {
        this.cardService = cardService;
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "getCardByIdRequest")
    @ResponsePayload
    public CardResponseDto GetCardById(@RequestPayload GetCardByIdRequest request) {
        Long validatedId = CardValidator.validateId(request.getId());
        return cardService.GetCardById(validatedId);
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "createCardRequest")
    @ResponsePayload
    public CardResponseDto CreateCard(@RequestPayload CreateCardDto request) {
        System.out.println("- Name: " + request.getName());
        System.out.println("- Type: " + request.getType());
        System.out.println("- Rarity: " + request.getRarity());
        System.out.println("- ElixirCost: " + request.getElixirCost());
        
        CreateCardDto validatedRequest = CardValidator.validateAll(request);
        return cardService.CreateCard(validatedRequest);
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "deleteCardRequest")
    @ResponsePayload
    public DeleteCardResponse DeleteCard(@RequestPayload DeleteCardRequest request) {
        Long validatedId = CardValidator.validateId(request.getId());
        return cardService.DeleteCard(validatedId);
    }
    
    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "updateCardRequest")
    @ResponsePayload
    public CardResponseDto UpdateCard(@RequestPayload UpdateCardDto request) {
        UpdateCardDto cardToUpdate = CardValidator.validateUpdate(request);
        return cardService.UpdateCard(cardToUpdate);
    }   

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "getAllCardsRequest")
    @ResponsePayload
    public GetAllCardsResponse GetAllCards(@RequestPayload GetAllCardsRequest request) {
        GetAllCardsRequest validatedRequest = CardValidator.PaginationValidate(request);
        return cardService.GetAllCards(validatedRequest);
    }


}