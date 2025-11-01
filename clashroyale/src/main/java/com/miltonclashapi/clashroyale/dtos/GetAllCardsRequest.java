package com.miltonclashapi.clashroyale.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;

@XmlRootElement(name = "getAllCardsRequest", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(propOrder = {"page", "pageSize", "sortBy", "sortDirection"})
public class GetAllCardsRequest {
    
    @XmlElement(name = "page", namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer page = 0;
    
    @XmlElement(name = "pageSize", namespace = "http://www.miltonclashapi.com/api/cards")
    private Integer pageSize = 10;
    
    @XmlElement(name = "sortBy", namespace = "http://www.miltonclashapi.com/api/cards")
    private String sortBy = "name";
    
    @XmlElement(name = "sortDirection", namespace = "http://www.miltonclashapi.com/api/cards")
    private String sortDirection = "asc";
    
    public GetAllCardsRequest() {}
    
    public GetAllCardsRequest(Integer page, Integer pageSize, String sortBy, String sortDirection) {
        this.page = page;
        this.pageSize = pageSize;
        this.sortBy = sortBy;
        this.sortDirection = sortDirection;
    }
    
    public Integer getPage() { return page; }
    public void setPage(Integer page) { this.page = page; }
    
    public Integer getPageSize() { return pageSize; }
    public void setPageSize(Integer pageSize) { this.pageSize = pageSize; }
    
    public String getSortBy() { return sortBy; }
    public void setSortBy(String sortBy) { this.sortBy = sortBy; }
    
    public String getSortDirection() { return sortDirection; }
    public void setSortDirection(String sortDirection) { this.sortDirection = sortDirection; }
}