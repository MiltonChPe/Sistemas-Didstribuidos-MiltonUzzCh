package com.miltonclashapi.clashroyale.dtos;


import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "getCardByIdRequest", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
public class GetCardByIdRequest {
    
    @XmlElement(name = "id", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Long id;
    
    public GetCardByIdRequest() {}
    
    public GetCardByIdRequest(Long id) {
        this.id = id;
    }
    
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
}