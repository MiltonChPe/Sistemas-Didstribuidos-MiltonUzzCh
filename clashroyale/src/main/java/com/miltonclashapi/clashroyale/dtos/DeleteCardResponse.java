package com.miltonclashapi.clashroyale.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "deleteCardResponse", namespace = "http://www.miltonclashapi.com/api/cards")
@XmlAccessorType(XmlAccessType.FIELD)
public class DeleteCardResponse {
    
    @XmlElement(name = "success", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private Boolean success;
    
    @XmlElement(name = "message", required = true, namespace = "http://www.miltonclashapi.com/api/cards")
    private String message;
    
    public DeleteCardResponse() {}
    
    public DeleteCardResponse(Boolean success, String message) {
        this.success = success;
        this.message = message;
    }
    
    public Boolean getSuccess() { return success; }
    public void setSuccess(Boolean success) { this.success = success; }
    
    public String getMessage() { return message; }
    public void setMessage(String message) { this.message = message; }
}