//
// Este archivo ha sido generado por Eclipse Implementation of JAXB v3.0.0 
// Visite https://eclipse-ee4j.github.io/jaxb-ri 
// Todas las modificaciones realizadas en este archivo se perderán si se vuelve a compilar el esquema de origen. 
// Generado el: 2025.11.01 a las 02:36:52 PM CST 
//


package com.clashroyale.clashcardapi.infrastructure.soap.generated;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Clase Java para anonymous complex type.
 * 
 * <p>El siguiente fragmento de esquema especifica el contenido que se espera que haya en esta clase.
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="cards" type="{http://www.miltonclashapi.com/api/cards}cardList"/&gt;
 *         &lt;element name="currentPage" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
 *         &lt;element name="totalPages" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
 *         &lt;element name="totalElements" type="{http://www.w3.org/2001/XMLSchema}long"/&gt;
 *         &lt;element name="pageSize" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "cards",
    "currentPage",
    "totalPages",
    "totalElements",
    "pageSize"
})
@XmlRootElement(name = "getAllCardsResponse")
public class GetAllCardsResponse {

    @XmlElement(required = true)
    protected CardList cards;
    protected int currentPage;
    protected int totalPages;
    protected long totalElements;
    protected int pageSize;

    /**
     * Obtiene el valor de la propiedad cards.
     * 
     * @return
     *     possible object is
     *     {@link CardList }
     *     
     */
    public CardList getCards() {
        return cards;
    }

    /**
     * Define el valor de la propiedad cards.
     * 
     * @param value
     *     allowed object is
     *     {@link CardList }
     *     
     */
    public void setCards(CardList value) {
        this.cards = value;
    }

    /**
     * Obtiene el valor de la propiedad currentPage.
     * 
     */
    public int getCurrentPage() {
        return currentPage;
    }

    /**
     * Define el valor de la propiedad currentPage.
     * 
     */
    public void setCurrentPage(int value) {
        this.currentPage = value;
    }

    /**
     * Obtiene el valor de la propiedad totalPages.
     * 
     */
    public int getTotalPages() {
        return totalPages;
    }

    /**
     * Define el valor de la propiedad totalPages.
     * 
     */
    public void setTotalPages(int value) {
        this.totalPages = value;
    }

    /**
     * Obtiene el valor de la propiedad totalElements.
     * 
     */
    public long getTotalElements() {
        return totalElements;
    }

    /**
     * Define el valor de la propiedad totalElements.
     * 
     */
    public void setTotalElements(long value) {
        this.totalElements = value;
    }

    /**
     * Obtiene el valor de la propiedad pageSize.
     * 
     */
    public int getPageSize() {
        return pageSize;
    }

    /**
     * Define el valor de la propiedad pageSize.
     * 
     */
    public void setPageSize(int value) {
        this.pageSize = value;
    }

}
