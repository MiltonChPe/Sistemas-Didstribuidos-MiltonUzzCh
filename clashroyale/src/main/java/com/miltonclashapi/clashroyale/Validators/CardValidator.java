
package com.miltonclashapi.clashroyale.Validators;

import org.springframework.stereotype.Component;

import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsRequest;
import com.miltonclashapi.clashroyale.dtos.UpdateCardDto;

@Component
public class CardValidator {

    public static CreateCardDto validateName(CreateCardDto card) {
        if (card.getName() == null || card.getName().trim().isEmpty()) {
            throw new IllegalArgumentException("Card name is required");
        }
        return card;
    }

    public static CreateCardDto validateType(CreateCardDto card) {
        if (card.getType() == null || card.getType().trim().isEmpty()) {
            throw new IllegalArgumentException("Card type is required");
        }
        
        String[] validTypes = {"Troop", "Spell", "Building"};
        boolean isValidType = false;
        for (String validType : validTypes) {
            if (validType.toLowerCase().equalsIgnoreCase(card.getType().toLowerCase())) {
                isValidType = true;
                break;
            }
        }
        
        if (!isValidType) {
            throw new IllegalArgumentException("Card type must be: Troop, Spell, or Building");
        }
        
        return card;
    }

    public static CreateCardDto validateRarity(CreateCardDto card) {
        if (card.getRarity() == null || card.getRarity().trim().isEmpty()) {
            throw new IllegalArgumentException("Card rarity is required");
        }
        
        String[] validRarities = {"Common", "Rare", "Epic", "Legendary"};
        boolean isValidRarity = false;
        for (String validRarity : validRarities) {
            if (validRarity.toLowerCase().equalsIgnoreCase(card.getRarity().toLowerCase())) {
                isValidRarity = true;
                break;
            }
        }
        
        if (!isValidRarity) {
            throw new IllegalArgumentException("Card rarity must be: Common, Rare, Epic, or Legendary");
        }
        
        return card;
    }

    public static CreateCardDto validateElixirCost(CreateCardDto card) {
        if (card.getElixirCost() == null || card.getElixirCost() <= 0) {
            throw new IllegalArgumentException("Card elixir cost must be grater than 0");
        }
        
        if (card.getElixirCost() > 10) {
            throw new IllegalArgumentException("Card elixir must be less than 10");
        }
        
        return card;
    }
    public static Long validateId(Long id) {
        if (id == null || id <= 0) {
            throw new IllegalArgumentException("Id required.");
        }
        return id;
    }

    public static UpdateCardDto validateUpdate(UpdateCardDto card) {
 
    if (card.getId() == null || card.getId() <= 0) {
        throw new IllegalArgumentException("Valid card ID is required for update");
    }
    CreateCardDto paraprobar = new CreateCardDto(
        card.getName(),
        card.getType(),
        card.getRarity(),
        card.getElixirCost()
    );

    validateName(paraprobar);
    validateType(paraprobar);
    validateRarity(paraprobar);
    validateElixirCost(paraprobar);

    return card;
}

    public static GetAllCardsRequest PaginationValidate(GetAllCardsRequest request) {
        if (request.getPage() == null || request.getPage() < 0) {
            request.setPage(0);
        }
        if (request.getPageSize() == null || request.getPageSize() < 1) {
            request.setPageSize(10);
        }
        if (request.getPageSize() > 20) {
            request.setPageSize(20); 
        }
        
        String[] sort = {"name", "type", "rarity", "elixirCost", "id"};
        boolean isValidSortBy = false;
        if (request.getSortBy() != null) {
            for (String validField : sort) {
                if (validField.equalsIgnoreCase(request.getSortBy())) {
                    request.setSortBy(validField);
                    isValidSortBy = true;
                    break;
                }
            }
        }
        
        if (!isValidSortBy) {
            request.setSortBy("name");
        }
        
        if (request.getSortDirection() == null || 
            (!request.getSortDirection().equalsIgnoreCase("asc") && 
             !request.getSortDirection().equalsIgnoreCase("desc"))) {
            request.setSortDirection("asc");
        }
        
        return request;
    }

    public static CreateCardDto validateAll(CreateCardDto card) {
        return validateElixirCost(
                validateRarity(
                    validateType(
                        validateName(card))));
    }

}