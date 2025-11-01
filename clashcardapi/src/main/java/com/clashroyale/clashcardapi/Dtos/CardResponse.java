package com.clashroyale.clashcardapi.Dtos;

import java.io.Serializable;
import java.util.Objects;

public class CardResponse implements Serializable {

    private static final long serialVersionUID = 1L;
    
    private Long id;
    private String name;
    private String type;
    private String rarity;
    private Integer elixirCost;

    public CardResponse() {}

    public CardResponse(Long id, String name, String type, String rarity, Integer elixirCost) {
        this.id = id;
        this.name = name;
        this.type = type;
        this.rarity = rarity;
        this.elixirCost = elixirCost;
    }
    
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    public String getType() { return type; }
    public void setType(String type) { this.type = type; }
    public String getRarity() { return rarity; }
    public void setRarity(String rarity) { this.rarity = rarity; }
    public Integer getElixirCost() { return elixirCost; }
    public void setElixirCost(Integer elixirCost) { this.elixirCost = elixirCost; }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        CardResponse that = (CardResponse) o;
        return Objects.equals(id, that.id) && Objects.equals(name, that.name) && Objects.equals(type, that.type) && Objects.equals(rarity, that.rarity) && Objects.equals(elixirCost, that.elixirCost);
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, name, type, rarity, elixirCost);
    }
}