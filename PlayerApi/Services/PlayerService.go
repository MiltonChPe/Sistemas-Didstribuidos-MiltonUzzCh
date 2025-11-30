package services

import (
	"context"
	"io"
	mappers "playerapi/Mappers"
	pb "playerapi/Pb"
	repositories "playerapi/Repositories"
	"strings"
	"time"

	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"
	"google.golang.org/protobuf/types/known/emptypb"
)

type PlayerService struct {
	pb.UnimplementedPlayerServiceServer
	_playerrepository repositories.IPlayerRepository
}

func NewPlayerService(repo repositories.IPlayerRepository) *PlayerService {
	return &PlayerService{
		_playerrepository: repo,
	}
}

func (serv *PlayerService) CreatePlayers(requestStream pb.PlayerService_CreatePlayersServer) error {
	var createdPlayers []*pb.Player

	for {
		playereq, err := requestStream.Recv()
		if err == io.EOF {
			return requestStream.SendAndClose(&pb.CreatePlayerResponse{
				Players: createdPlayers,
			})
		}
		if err != nil {
			return status.Errorf(codes.Internal, "Fail to receive player: %v", err)
		}

		player := mappers.ToModel(playereq)

		playerexists, err := serv._playerrepository.GetByNameAsync(requestStream.Context(), player.Name)
		if err == nil && len(playerexists) > 0 {
			continue
		}

		createdPlayer, err := serv._playerrepository.CreateAsync(requestStream.Context(), player)
		if err != nil {
			return status.Errorf(codes.Internal, "fail creating player: %v", err)
		}

		createdPlayers = append(createdPlayers, mappers.ToResponse(createdPlayer))
	}
}

func (serv *PlayerService) GetPlayerById(_context context.Context, req *pb.PlayerByIdRequest) (*pb.PlayerResponse, error) {
	if !isValidID(req.Id) {
		return nil, status.Errorf(codes.InvalidArgument, "the ID %s is not a valid", req.Id)
	}
	if strings.TrimSpace(req.Id) == "" {
		return nil, status.Error(codes.InvalidArgument, "the ID cannot be empty")
	}
	player, err := serv._playerrepository.GetByIDAsync(_context, req.Id)
	if err != nil {
		return nil, status.Errorf(codes.NotFound, "player with ID %s not found", req.Id)
	}

	return mappers.ToPlayerResponse(player), nil
}

func (serv *PlayerService) DeletePlayer(_context context.Context, req *pb.PlayerByIdRequest) (*emptypb.Empty, error) {
	if !isValidID(req.Id) {
		return nil, status.Errorf(codes.InvalidArgument, "the ID %s is not a valid", req.Id)
	}

	_, err := serv._playerrepository.GetByIDAsync(_context, req.Id)
	if err != nil {
		return nil, status.Errorf(codes.NotFound, "player with ID %s not found", req.Id)
	}

	err = serv._playerrepository.DeleteAsync(_context, req.Id)
	if err != nil {
		return nil, status.Errorf(codes.Internal, "cant delete player: %v", err)
	}

	return &emptypb.Empty{}, nil
}

func (serv *PlayerService) UpdatePlayer(_context context.Context, req *pb.UpdatePlayerRequest) (*emptypb.Empty, error) {
	if !isValidID(req.Id) {
		return nil, status.Errorf(codes.InvalidArgument, "the ID %s is not a valid", req.Id)
	}
	if strings.TrimSpace(req.Name) == "" || len(req.Name) < 2 {
		return nil, status.Error(codes.InvalidArgument, "the player need to have 2 letters minimun or not be empty")
	}
	if req.Level < 1 {
		return nil, status.Error(codes.InvalidArgument, "level must be at least 1")
	}
	if req.Trophies < 0 {
		return nil, status.Error(codes.InvalidArgument, "invalid trophies value")
	}
	if strings.TrimSpace(req.Clan) == "" {
		return nil, status.Error(codes.InvalidArgument, "clan cannot be empty")
	}
	if req.BattlesPlayed < 0 {
		return nil, status.Error(codes.InvalidArgument, "the battles must not be less than 0")
	}
	if req.Coins < 0 {
		return nil, status.Error(codes.InvalidArgument, "the coins cannot be less than 0")
	}
	if req.Gems < 0 {
		return nil, status.Error(codes.InvalidArgument, "the gems cannot be less than 0")
	}

	_, err := serv._playerrepository.GetByIDAsync(_context, req.Id)
	if err != nil {
		return nil, status.Errorf(codes.NotFound, "player with ID %s not found", req.Id)
	}

	players, err := serv._playerrepository.GetByNameAsync(_context, req.Name)
	if err == nil {
		for _, p := range players {
			if p.ID != req.Id {
				return nil, status.Errorf(codes.AlreadyExists, "a player with the name %s already exists", req.Name)
			}
		}
	}

	player := mappers.UpdateRequestToModel(req)
	err = serv._playerrepository.UpdateAsync(_context, player)
	if err != nil {
		return nil, status.Errorf(codes.Internal, "failed to update player: %v", err)
	}

	return &emptypb.Empty{}, nil
}

func (s *PlayerService) GetAllPlayersByName(req *pb.PlayerByNameRequest, stream pb.PlayerService_GetAllPlayersByNameServer) error {
	players, err := s._playerrepository.GetByNameAsync(stream.Context(), req.Name)
	if err != nil {
		return status.Errorf(codes.Internal, "failed to get players: %v", err)
	}

	for _, player := range players {
		select {
		case <-stream.Context().Done():
			return stream.Context().Err()
		default:
			if err := stream.Send(mappers.ToPlayerResponse(player)); err != nil {
				return err
			}
			time.Sleep(5 * time.Second)
		}
	}

	return nil
}

func isValidID(id string) bool {
	return strings.TrimSpace(id) != "" && len(id) > 20
}
