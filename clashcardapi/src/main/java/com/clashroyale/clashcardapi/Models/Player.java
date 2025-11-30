package com.clashroyale.clashcardapi.Models;

public class Player {
    private String id;
    private String name;
    private int level;
    private int trophies;
    private String clan;
    private int battlesPlayed;
    private int coins;
    private int gems;

    public Player() {
    }

    public Player(String id, String name, int level, int trophies, String clan, 
                  int battlesPlayed, int coins, int gems) {
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

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public int getTrophies() {
        return trophies;
    }

    public void setTrophies(int trophies) {
        this.trophies = trophies;
    }

    public String getClan() {
        return clan;
    }

    public void setClan(String clan) {
        this.clan = clan;
    }

    public int getBattlesPlayed() {
        return battlesPlayed;
    }

    public void setBattlesPlayed(int battlesPlayed) {
        this.battlesPlayed = battlesPlayed;
    }

    public int getCoins() {
        return coins;
    }

    public void setCoins(int coins) {
        this.coins = coins;
    }

    public int getGems() {
        return gems;
    }

    public void setGems(int gems) {
        this.gems = gems;
    }
}
