package com.clashroyale.clashcardapi;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cache.annotation.EnableCaching;
@EnableCaching
@SpringBootApplication
public class ClashcardapiApplication {

	public static void main(String[] args) {
		SpringApplication.run(ClashcardapiApplication.class, args);
	}

}
