package com.miltonclashapi.clashroyale.services;
import java.util.stream.Collectors;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;
import com.miltonclashapi.clashroyale.contracts.ICardService;
import com.miltonclashapi.clashroyale.dtos.CardList;
import com.miltonclashapi.clashroyale.dtos.CardResponseDto;
import com.miltonclashapi.clashroyale.dtos.CreateCardDto;
import com.miltonclashapi.clashroyale.dtos.DeleteCardResponse;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsRequest;
import com.miltonclashapi.clashroyale.dtos.GetAllCardsResponse;
import com.miltonclashapi.clashroyale.dtos.UpdateCardDto;
import com.miltonclashapi.clashroyale.infrastructure.entities.CardEntity;
import com.miltonclashapi.clashroyale.mappers.CardMapper;
import com.miltonclashapi.clashroyale.repositories.ICardRepository;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import java.util.List;

@Service
public class CardService implements ICardService {

    private final ICardRepository cardRepository;
    private final CardMapper cardMapper;
    
    @Autowired
    public CardService(ICardRepository cardRepository, CardMapper cardMapper) {
        this.cardRepository = cardRepository;
        this.cardMapper = cardMapper;
    }

    @Override
    public CardResponseDto GetCardById(Long id) {
    CardEntity foundCard = cardRepository.findById(id)
            .orElseThrow(() -> new IllegalArgumentException("Not found with id: " + id));

    return cardMapper.toResponseDto(foundCard);
    }

    @Override
    public CardResponseDto CreateCard(CreateCardDto createCardDto) {
        System.out.println("- Name: " + createCardDto.getName());
        System.out.println("- Type: " + createCardDto.getType());
        System.out.println("- Rarity: " + createCardDto.getRarity());
        System.out.println("- ElixirCost: " + createCardDto.getElixirCost());

        if (cardRepository.existsByNameIgnoreCase(createCardDto.getName())) {
            throw new IllegalArgumentException("A card with the name '" + createCardDto.getName() + "' already exists.");
        }

        CardEntity newCard = cardMapper.toEntity(createCardDto);
        CardEntity savedCard = cardRepository.save(newCard); 
        return cardMapper.toResponseDto(savedCard);
    }

    @Override
    public DeleteCardResponse DeleteCard(Long id) {
    try {
        CardEntity entity = cardRepository.findById(id)
            .orElseThrow(() -> new IllegalArgumentException("Card with ID " + id + " not found"));
        
        cardRepository.deleteById(id);
        
        return new DeleteCardResponse(true, "Card '" + entity.getName() + "' deleted yeiy!");
        
    } catch (IllegalArgumentException e) {
        return new DeleteCardResponse(false, e.getMessage());
    } catch (Exception e) {
        return new DeleteCardResponse(false, "Error: " + e.getMessage());
    }
}

    @Override
    public CardResponseDto UpdateCard(UpdateCardDto updateCardDto) {
        CardEntity card = cardRepository.findById(updateCardDto.getId())
            .orElseThrow(() -> new IllegalArgumentException("Card with ID " + updateCardDto.getId() + " not found"));
        
        if (!card.getName().equalsIgnoreCase(updateCardDto.getName())) {
            if (cardRepository.existsByNameIgnoreCase(updateCardDto.getName())) {
                throw new IllegalArgumentException("A card with the name '" + updateCardDto.getName() + "' already exists.");
            }
        }
  
        CardEntity updatedEntity = cardMapper.updateEntity(card, updateCardDto);
        CardEntity savedCard = cardRepository.save(updatedEntity);
        
        return cardMapper.toResponseDto(savedCard);
    }

    @Override
    public GetAllCardsResponse GetAllCards(GetAllCardsRequest request) {
        Sort sort = request.getSortDirection().equalsIgnoreCase("desc") 
            ? Sort.by(request.getSortBy()).descending()
            : Sort.by(request.getSortBy()).ascending();
   
        Pageable pageable = PageRequest.of(request.getPage(), request.getPageSize(), sort);
        Page<CardEntity> cardPage = cardRepository.findAll(pageable);
        
        List<CardResponseDto> cardDtos = cardPage.getContent()
            .stream()
            .map(cardMapper::toResponseDto)
            .collect(Collectors.toList());
        
        CardList cardList = new CardList(cardDtos);
        
        return new GetAllCardsResponse(
            cardList,
            cardPage.getNumber(),
            cardPage.getTotalPages(),
            cardPage.getTotalElements(),
            cardPage.getSize()
        );
    }


}