package models

type Player struct {
	ID            string `json:"id"`
	Name          string `json:"name"`
	Level         int32  `json:"level"`
	Trophies      int32  `json:"trophies"`
	Clan          string `json:"clan"`
	BattlesPlayed int32  `json:"battles_played"`
	Coins         int32  `json:"coins"`
	Gems          int32  `json:"gems"`
}
