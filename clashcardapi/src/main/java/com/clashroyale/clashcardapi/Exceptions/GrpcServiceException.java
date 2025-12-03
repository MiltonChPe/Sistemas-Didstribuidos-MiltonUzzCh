package com.clashroyale.clashcardapi.Exceptions;

public class GrpcServiceException extends RuntimeException {
    public GrpcServiceException(String message) {
        super(message);
    }
    
    public GrpcServiceException(String message, Throwable cause) {
        super(message, cause);
    }
}
