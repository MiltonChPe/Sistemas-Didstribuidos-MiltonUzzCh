package com.clashroyale.clashcardapi.Exceptions;

public class CardNotFoundException extends RuntimeException {

    public CardNotFoundException(long id) {
        super("Card not found with id: " + id);
    }

    public CardNotFoundException(String message) {
        super(message);
    }
}