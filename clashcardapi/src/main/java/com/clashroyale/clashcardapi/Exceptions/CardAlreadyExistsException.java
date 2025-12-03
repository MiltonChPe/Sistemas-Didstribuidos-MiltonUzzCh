package com.clashroyale.clashcardapi.Exceptions;


public class CardAlreadyExistsException extends RuntimeException {
  
    public CardAlreadyExistsException(String name) {
        super("Card already exists with name: " + name);
    }
    
}