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
	Plant,
	Magnet
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

public enum SoundType {
	None = -1,
	MainMenu = 0,
	ButtonClick = 1,
	Jump = 2,
	JumpOnEnemy = 3,
	EnemyDie = 4,
	PlayerToBig = 5,
	Background = 6,
	CollectCoin = 7,
	BrickBreak = 8,
	GameWin = 9,
	Firework = 10,
	PlayerHitBrick = 11,
	Shoot = 12,
	CollectMagnet = 13,
	Water = 14,
	EffectExplosion = 15
}

public enum ParticleType {
	None = -1,
	CollectCoin = 0,
	FireExplosion = 1,
	BrickExplosion = 2,
	Confetti = 3
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
	Revive,
	NextLevel,
	OpenShop,
	PlayerDie,
	SwitchSound,
	SwitchMusic,
}

public enum PrefKey {
	PlayerData
}

public enum SoundCategory {
	SoundFX,
	Music
}