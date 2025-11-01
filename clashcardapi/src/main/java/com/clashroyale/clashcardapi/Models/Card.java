package com.clashroyale.clashcardapi.Models;

import java.io.Serializable;
import java.util.Objects;


public class Card implements Serializable {

    private static final long serialVersionUID = 1L;
    
    private Long id;
    private String name;
    private String type;
    private String rarity;
    private Integer elixirCost; 

    public Card() {
    }

    public Card(Long id, String name, String type, String rarity, Integer elixirCost) {
        this.id = id;
        this.name = name;
        this.type = type;
        this.rarity = rarity;
        this.elixirCost = elixirCost; 
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getRarity() {
        return rarity;
    }

    public void setRarity(String rarity) {
        this.rarity = rarity;
    }

    public Integer getElixirCost() {
        return elixirCost;
    }

    public void setElixirCost(Integer elixirCost) {
        this.elixirCost = elixirCost;
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Card card = (Card) o;
        return Objects.equals(id, card.id) &&
                Objects.equals(name, card.name) &&
                Objects.equals(type, card.type) &&
                Objects.equals(rarity, card.rarity) &&
                Objects.equals(elixirCost, card.elixirCost); 
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, name, type, rarity, elixirCost); 
    }

    @Override
    public String toString() {
        return "Card{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", type='" + type + '\'' +
                ", rarity='" + rarity + '\'' +
                ", elixirCost=" + elixirCost +
                '}';
    }
}