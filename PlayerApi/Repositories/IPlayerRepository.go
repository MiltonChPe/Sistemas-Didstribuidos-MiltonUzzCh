package repositories

import (
	"context"
	models "playerapi/Models"
)

type IPlayerRepository interface {
	CreateAsync(_context context.Context, player *models.Player) (*models.Player, error)
	GetByNameAsync(_context context.Context, name string) ([]*models.Player, error)
	GetByIDAsync(_context context.Context, id string) (*models.Player, error)
	DeleteAsync(_context context.Context, id string) error
	UpdateAsync(_context context.Context, player *models.Player) error
}
