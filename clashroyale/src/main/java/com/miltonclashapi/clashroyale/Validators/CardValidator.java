
package com.miltonclashapi.clashroyale.Validators;

import org.springframework.stereotype.Component;

import com.miltonclashapi.clashroyale.dtos.CreateCardDto;

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

    public static CreateCardDto validateAll(CreateCardDto card) {
        return validateElixirCost(
                validateRarity(
                    validateType(
                        validateName(card))));
    }

}