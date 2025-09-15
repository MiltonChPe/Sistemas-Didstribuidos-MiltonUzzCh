package com.miltonclashapi.clashroyale.endpoints;


import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;

import com.miltonclashapi.clashroyale.Validators.CardValidator;
import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.GetCardByIdRequest;
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

 
}