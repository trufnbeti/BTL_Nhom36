using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnim {
	idle,
	run,
	jump1,
	falldown1,
	climb_down,
	die
}

public enum GameTag {
	Player,
	PlayerCollider,
	Enemy,
	Platform,
	Plant
}

public enum GameState
{
	MainMenu = 1,
	GamePlay = 2,
	Pause = 3,
	GameEnd = 4
}

public enum PoolType {
	None = -1,
	Coin = 0,
	Bullet = 1
}

public enum EventID {
	AddCoin,
	SavePoint,
	LowerFlag,
	Win,
	Lose,
	StartGame,
	MainMenu,
	Resume,
	Pause,
	Replay,
	Revive
}

public enum SkinType {
	Mario1,
	Mario2,
	Naruto1,
	Naruto2,
	Dino1,
	Dino2,
}

public enum PrefKey {
	Coin,
	Level,
	CurrentSkin,
	SkinsPurchased
}

public enum ParticleType {
	None = -1,
	CollectCoin = 0,
	FireExplosion = 1,
	BrickExplosion = 2,
	Confetti = 3
}
