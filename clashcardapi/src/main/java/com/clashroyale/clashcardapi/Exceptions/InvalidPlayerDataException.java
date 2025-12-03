package com.clashroyale.clashcardapi.Exceptions;

public class InvalidPlayerDataException extends RuntimeException {
    public InvalidPlayerDataException(String message) {
        super(message);
    }
}
