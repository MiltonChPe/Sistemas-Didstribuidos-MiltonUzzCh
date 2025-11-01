package com.clashroyale.clashcardapi.Infrastructure.Soap.Configs;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.oxm.jaxb.Jaxb2Marshaller;
import org.springframework.ws.client.core.WebServiceTemplate;

@Configuration
public class SoapClientConfig {

    private static final String GENERATED_CLASSES_PACKAGE = "com.clashroyale.clashcardapi.infrastructure.soap.generated";
    
    @Value("${soap.api.url}")
    private String soapApiUri;

    @Bean
    public Jaxb2Marshaller marshaller() {
        Jaxb2Marshaller marshaller = new Jaxb2Marshaller();
        marshaller.setContextPath(GENERATED_CLASSES_PACKAGE);
        return marshaller;
    }

    @Bean
    public WebServiceTemplate webServiceTemplate(Jaxb2Marshaller marshaller) {
        WebServiceTemplate template = new WebServiceTemplate();
        template.setDefaultUri(soapApiUri); 
        
        template.setMarshaller(marshaller);
        template.setUnmarshaller(marshaller);
        
        return template;
    }
}