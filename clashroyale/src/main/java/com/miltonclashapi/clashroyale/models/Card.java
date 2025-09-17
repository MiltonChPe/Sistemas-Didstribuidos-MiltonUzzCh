package com.miltonclashapi.clashroyale.models;


public class Card {
    private Long id;
    private String name;
    private String type;
    private String rarity;
    private Integer elixirCost;
    
    public Card() {}
    
    public Card(Long id, String name, String type, String rarity, Integer elixirCost) {
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
}