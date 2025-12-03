package com.clashroyale.clashcardapi.Dtos;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.Size;


public class PatchCardRequest {

    @Size(min = 1, max = 100, message = "Name must be between 1 and 100 characters")
    private String name;

    @Size(min = 1, max = 50, message = "Type must be between 1 and 50 characters")
    private String type;

    @Size(min = 1, max = 50, message = "Rarity must be between 1 and 50 characters")
    private String rarity;

    @Min(value = 0, message = "Elixir cost must be at least 0")
    private Integer elixirCost;

    public PatchCardRequest() {
    }

    public String getName() {return name;}

    public void setName(String name) {this.name = name;}

    public String getType() {return type;}

    public void setType(String type) {this.type = type;}

    public String getRarity() {return rarity;}

    public void setRarity(String rarity) {this.rarity = rarity;}

    public Integer getElixirCost() {return elixirCost;}

    public void setElixirCost(Integer elixirCost) {this.elixirCost = elixirCost;}
}