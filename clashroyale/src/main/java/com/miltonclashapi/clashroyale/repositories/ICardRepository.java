package com.miltonclashapi.clashroyale.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.miltonclashapi.clashroyale.infrastructure.entities.CardEntity;

@Repository
public interface ICardRepository extends JpaRepository<CardEntity, Long> {
    boolean existsByNameIgnoreCase(String name);
}
