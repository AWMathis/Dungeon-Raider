using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPiece : GamePiece
{
	[SerializeField] protected int attackPower = 1;
	[SerializeField] protected int damage = 1;

	bool canAttack = false;

	public int AttackPower { get { return attackPower; } }

	void Start() {
		base.Start();
	}

	public override void SetAttributes() {
		name = "CombatPiece";
		actionType = "Combat";
	}

	override public void OnUse () {

		gameObject.name = "Deleted";
		Destroy(gameObject);

	}

	override public void EachTurnEffect() {

		if (canAttack && damage != 0) {
			Pulse(pulseScale, pulseTime);
			player.DealDamage(damage);
			gameObject.GetComponentInChildren<ParticleSystem>().Play();
		}
		else {
			canAttack = true;
		}
			
	}

}
