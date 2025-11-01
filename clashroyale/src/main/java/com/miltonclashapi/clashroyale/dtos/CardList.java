package com.miltonclashapi.clashroyale.dtos;

import java.util.List;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlType;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "cardList", namespace = "http://www.miltonclashapi.com/api/cards")
public class CardList {
    
    @XmlElement(name = "card", namespace = "http://www.miltonclashapi.com/api/cards")
    private List<CardResponseDto> card;
    
    public CardList() {}
    
    public CardList(List<CardResponseDto> cards) {
        this.card = cards;
    }
    
    public List<CardResponseDto> getCard() { return card; }
    public void setCard(List<CardResponseDto> card) { this.card = card; }
}