//
// Este archivo ha sido generado por Eclipse Implementation of JAXB v3.0.0 
// Visite https://eclipse-ee4j.github.io/jaxb-ri 
// Todas las modificaciones realizadas en este archivo se perderán si se vuelve a compilar el esquema de origen. 
// Generado el: 2025.11.01 a las 02:36:52 PM CST 
//


package com.clashroyale.clashcardapi.infrastructure.soap.generated;

import java.util.ArrayList;
import java.util.List;
import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Clase Java para cardList complex type.
 * 
 * <p>El siguiente fragmento de esquema especifica el contenido que se espera que haya en esta clase.
 * 
 * <pre>
 * &lt;complexType name="cardList"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="card" maxOccurs="unbounded" minOccurs="0"&gt;
 *           &lt;complexType&gt;
 *             &lt;complexContent&gt;
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *                 &lt;sequence&gt;
 *                   &lt;element name="Id" type="{http://www.w3.org/2001/XMLSchema}long"/&gt;
 *                   &lt;element name="Name" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
 *                   &lt;element name="Type" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
 *                   &lt;element name="Rarity" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
 *                   &lt;element name="ElixirCost" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
 *                 &lt;/sequence&gt;
 *               &lt;/restriction&gt;
 *             &lt;/complexContent&gt;
 *           &lt;/complexType&gt;
 *         &lt;/element&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "cardList", propOrder = {
    "card"
})
public class CardList {

    protected List<CardList.Card> card;

    /**
     * Gets the value of the card property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the Jakarta XML Binding object.
     * This is why there is not a <CODE>set</CODE> method for the card property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getCard().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link CardList.Card }
     * 
     * 
     */
    public List<CardList.Card> getCard() {
        if (card == null) {
            card = new ArrayList<CardList.Card>();
        }
        return this.card;
    }


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
     *         &lt;element name="Id" type="{http://www.w3.org/2001/XMLSchema}long"/&gt;
     *         &lt;element name="Name" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
     *         &lt;element name="Type" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
     *         &lt;element name="Rarity" type="{http://www.w3.org/2001/XMLSchema}string"/&gt;
     *         &lt;element name="ElixirCost" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
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
        "id",
        "name",
        "type",
        "rarity",
        "elixirCost"
    })
    public static class Card {

        @XmlElement(name = "Id")
        protected long id;
        @XmlElement(name = "Name", required = true)
        protected String name;
        @XmlElement(name = "Type", required = true)
        protected String type;
        @XmlElement(name = "Rarity", required = true)
        protected String rarity;
        @XmlElement(name = "ElixirCost")
        protected int elixirCost;

        /**
         * Obtiene el valor de la propiedad id.
         * 
         */
        public long getId() {
            return id;
        }

        /**
         * Define el valor de la propiedad id.
         * 
         */
        public void setId(long value) {
            this.id = value;
        }

        /**
         * Obtiene el valor de la propiedad name.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getName() {
            return name;
        }

        /**
         * Define el valor de la propiedad name.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setName(String value) {
            this.name = value;
        }

        /**
         * Obtiene el valor de la propiedad type.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getType() {
            return type;
        }

        /**
         * Define el valor de la propiedad type.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setType(String value) {
            this.type = value;
        }

        /**
         * Obtiene el valor de la propiedad rarity.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getRarity() {
            return rarity;
        }

        /**
         * Define el valor de la propiedad rarity.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setRarity(String value) {
            this.rarity = value;
        }

        /**
         * Obtiene el valor de la propiedad elixirCost.
         * 
         */
        public int getElixirCost() {
            return elixirCost;
        }

        /**
         * Define el valor de la propiedad elixirCost.
         * 
         */
        public void setElixirCost(int value) {
            this.elixirCost = value;
        }

    }

}
