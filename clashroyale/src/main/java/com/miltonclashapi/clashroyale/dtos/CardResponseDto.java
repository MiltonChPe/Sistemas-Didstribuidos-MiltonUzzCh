package com.miltonclashapi.clashroyale.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;

@XmlRootElement(name = "CardResponseDto", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(propOrder = {"id", "name", "type", "rarity", "elixirCost"})
public class CardResponseDto {
    
    @XmlElement(name = "Id", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Long id;
    
    @XmlElement(name = "Name", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String name;
    
    @XmlElement(name = "Type", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String type;
    
    @XmlElement(name = "Rarity", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String rarity;
    
    @XmlElement(name = "ElixirCost", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer elixirCost;
    
    public CardResponseDto() {}
    
    public CardResponseDto(Long id, String name, String type, String rarity, Integer elixirCost) {
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