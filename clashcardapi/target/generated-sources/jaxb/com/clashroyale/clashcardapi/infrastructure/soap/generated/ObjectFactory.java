//
// Este archivo ha sido generado por Eclipse Implementation of JAXB v3.0.0 
// Visite https://eclipse-ee4j.github.io/jaxb-ri 
// Todas las modificaciones realizadas en este archivo se perderán si se vuelve a compilar el esquema de origen. 
// Generado el: 2025.11.01 a las 02:36:52 PM CST 
//


package com.clashroyale.clashcardapi.infrastructure.soap.generated;

import jakarta.xml.bind.annotation.XmlRegistry;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.clashroyale.clashcardapi.infrastructure.soap.generated package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.clashroyale.clashcardapi.infrastructure.soap.generated
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link CardList }
     * 
     */
    public CardList createCardList() {
        return new CardList();
    }

    /**
     * Create an instance of {@link CardResponseDto }
     * 
     */
    public CardResponseDto createCardResponseDto() {
        return new CardResponseDto();
    }

    /**
     * Create an instance of {@link GetCardByIdRequest }
     * 
     */
    public GetCardByIdRequest createGetCardByIdRequest() {
        return new GetCardByIdRequest();
    }

    /**
     * Create an instance of {@link CreateCardRequest }
     * 
     */
    public CreateCardRequest createCreateCardRequest() {
        return new CreateCardRequest();
    }

    /**
     * Create an instance of {@link UpdateCardRequest }
     * 
     */
    public UpdateCardRequest createUpdateCardRequest() {
        return new UpdateCardRequest();
    }

    /**
     * Create an instance of {@link DeleteCardRequest }
     * 
     */
    public DeleteCardRequest createDeleteCardRequest() {
        return new DeleteCardRequest();
    }

    /**
     * Create an instance of {@link DeleteCardResponse }
     * 
     */
    public DeleteCardResponse createDeleteCardResponse() {
        return new DeleteCardResponse();
    }

    /**
     * Create an instance of {@link GetAllCardsRequest }
     * 
     */
    public GetAllCardsRequest createGetAllCardsRequest() {
        return new GetAllCardsRequest();
    }

    /**
     * Create an instance of {@link GetAllCardsResponse }
     * 
     */
    public GetAllCardsResponse createGetAllCardsResponse() {
        return new GetAllCardsResponse();
    }

    /**
     * Create an instance of {@link CardList.Card }
     * 
     */
    public CardList.Card createCardListCard() {
        return new CardList.Card();
    }

}
