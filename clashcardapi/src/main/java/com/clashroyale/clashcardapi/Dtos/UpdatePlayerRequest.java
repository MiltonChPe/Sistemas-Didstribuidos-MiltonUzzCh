package com.clashroyale.clashcardapi.Dtos;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public class UpdatePlayerRequest {
    
    @NotBlank(message = "El ID del jugador es obligatorio")
    private String id;
    
    @NotBlank(message = "El nombre del jugador es obligatorio")
    private String name;
    
    @NotNull(message = "El nivel es obligatorio")
    @Min(value = 1, message = "El nivel debe ser al menos 1")
    private Integer level;
    
    @NotNull(message = "Los trofeos son obligatorios")
    @Min(value = 0, message = "Los trofeos no pueden ser negativos")
    private Integer trophies;
    
    private String clan;
    
    @Min(value = 0, message = "Las batallas jugadas no pueden ser negativas")
    private Integer battlesPlayed;
    
    @Min(value = 0, message = "Las monedas no pueden ser negativas")
    private Integer coins;
    
    @Min(value = 0, message = "Las gemas no pueden ser negativas")
    private Integer gems;

    public UpdatePlayerRequest() {
    }

    public UpdatePlayerRequest(String id, String name, Integer level, Integer trophies, 
                               String clan, Integer battlesPlayed, Integer coins, Integer gems) {
        this.id = id;
        this.name = name;
        this.level = level;
        this.trophies = trophies;
        this.clan = clan;
        this.battlesPlayed = battlesPlayed;
        this.coins = coins;
        this.gems = gems;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getLevel() {
        return level;
    }

    public void setLevel(Integer level) {
        this.level = level;
    }

    public Integer getTrophies() {
        return trophies;
    }

    public void setTrophies(Integer trophies) {
        this.trophies = trophies;
    }

    public String getClan() {
        return clan;
    }

    public void setClan(String clan) {
        this.clan = clan;
    }

    public Integer getBattlesPlayed() {
        return battlesPlayed;
    }

    public void setBattlesPlayed(Integer battlesPlayed) {
        this.battlesPlayed = battlesPlayed;
    }

    public Integer getCoins() {
        return coins;
    }

    public void setCoins(Integer coins) {
        this.coins = coins;
    }

    public Integer getGems() {
        return gems;
    }

    public void setGems(Integer gems) {
        this.gems = gems;
    }
}
