package com.miltonclashapi.clashroyale.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;

@XmlRootElement(name = "createCardRequest", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(propOrder = {"name", "type", "rarity", "elixirCost"})
public class CreateCardDto {
    
    @XmlElement(name = "name", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String name;
    
    @XmlElement(name = "type", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String type;
    
    @XmlElement(name = "rarity", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String rarity;
    
    @XmlElement(name = "elixirCost", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer elixirCost;
    
    public CreateCardDto() {}
    
    public CreateCardDto(String name, String type, String rarity, Integer elixirCost) {
        this.name = name;
        this.type = type;
        this.rarity = rarity;
        this.elixirCost = elixirCost;
    }
    
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    
    public String getType() { return type; }
    public void setType(String type) { this.type = type; }
    
    public String getRarity() { return rarity; }
    public void setRarity(String rarity) { this.rarity = rarity; }
    
    public Integer getElixirCost() { return elixirCost; }
    public void setElixirCost(Integer elixirCost) { this.elixirCost = elixirCost; }
}