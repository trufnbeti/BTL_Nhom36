using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnim {
	idle,
	run,
	jump1,
	falldown1
}

public enum GameTag {
	Player,
	PlayerCollider,
	PlayerHit,
	Enemy,
	Platform
}

public enum PoolType {
	None = -1,
	Coin = 0,
	Bullet = 1
}

public enum EventID {
	AddCoin
}

public enum PrefKey {
	Coin
}

public enum ParticleType {
	None = -1,
	CollectCoin = 0,
	FireExplosion = 1,
}
