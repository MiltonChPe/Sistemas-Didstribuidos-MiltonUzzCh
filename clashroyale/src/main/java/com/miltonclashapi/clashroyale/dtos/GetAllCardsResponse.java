package com.miltonclashapi.clashroyale.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;

@XmlRootElement(name = "getAllCardsResponse", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(propOrder = {"cards", "currentPage", "totalPages", "totalElements", "pageSize"})
public class GetAllCardsResponse {
    
    @XmlElement(name = "cards", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private CardList cards;
    
    @XmlElement(name = "currentPage", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer currentPage;
    
    @XmlElement(name = "totalPages", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer totalPages;
    
    @XmlElement(name = "totalElements", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Long totalElements;
    
    @XmlElement(name = "pageSize", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer pageSize;
    
    public GetAllCardsResponse() {}
    
    public GetAllCardsResponse(CardList cards, Integer currentPage, Integer totalPages, 
                               Long totalElements, Integer pageSize) {
        this.cards = cards;
        this.currentPage = currentPage;
        this.totalPages = totalPages;
        this.totalElements = totalElements;
        this.pageSize = pageSize;
    }
    
    public CardList getCards() { return cards; }
    public void setCards(CardList cards) { this.cards = cards; }
    
    public Integer getCurrentPage() { return currentPage; }
    public void setCurrentPage(Integer currentPage) { this.currentPage = currentPage; }
    
    public Integer getTotalPages() { return totalPages; }
    public void setTotalPages(Integer totalPages) { this.totalPages = totalPages; }
    
    public Long getTotalElements() { return totalElements; }
    public void setTotalElements(Long totalElements) { this.totalElements = totalElements; }
    
    public Integer getPageSize() { return pageSize; }
    public void setPageSize(Integer pageSize) { this.pageSize = pageSize; }
}