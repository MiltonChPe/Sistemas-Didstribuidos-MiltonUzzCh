package com.clashroyale.clashcardapi.Dtos;

import java.io.Serializable;
import java.util.List;

public class PagedCardsResponse implements Serializable {

    private static final long serialVersionUID = 1L;
    
    private List<CardResponse> cards;
    private int currentPage;
    private int pageSize;
    private int totalPages;
    private long totalCards;

    public PagedCardsResponse() {
    }

    public PagedCardsResponse(List<CardResponse> cards, int currentPage, int pageSize, 
                              int totalPages, long totalCards) {
        this.cards = cards;
        this.currentPage = currentPage;
        this.pageSize = pageSize;
        this.totalPages = totalPages;
        this.totalCards = totalCards;
    }

    public List<CardResponse> getCards() {return cards;}

    public void setCards(List<CardResponse> cards) {this.cards = cards;}

    public int getCurrentPage() {return currentPage;}

    public void setCurrentPage(int currentPage) {this.currentPage = currentPage;}

    public int getPageSize() {return pageSize;}

    public void setPageSize(int pageSize) {this.pageSize = pageSize;}

    public int getTotalPages() {return totalPages;}

    public void setTotalPages(int totalPages) {this.totalPages = totalPages;}

    public long getTotalCards() {return totalCards;}

    public void setTotalCards(long totalCards) {this.totalCards = totalCards;}
}