using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPiece : CombatPiece {

	[SerializeField] int health = 2;
	[SerializeField] int goldValue = 2;
	[SerializeField] int expValue = 5;
	[SerializeField] Sprite neutralSprite;
	[SerializeField] Sprite deathSprite;

	int currentSumAttack = 0;

	void Start() {
		base.Start();
		
		int playerLevel = player.Level;
		damage = damage + (int)(playerLevel * 0.5);
		health = health + (int)(playerLevel * 0.5);

	}

	public override void SetAttributes() {
		name = "Enemy";
		actionType = "Combat";
	}

	override public void UpdateState(string currentActionType, bool currentlySelected, int sumAttack) {
		this.currentActionType = currentActionType;
		currentSumAttack = sumAttack;


		if (currentlySelected) {
			gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
			gameObject.GetComponent<SpriteRenderer>().color = activeColor;
			if (sumAttack >= health) {
				gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
				gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;
			}
		}
		else if (currentActionType == "") {
			gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
			gameObject.GetComponent<SpriteRenderer>().color = neutralColor;
		}
		else if (actionType != currentActionType) {
			gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
			gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
		}
		else {
			gameObject.GetComponent<SpriteRenderer>().sprite = neutralSprite;
			gameObject.GetComponent<SpriteRenderer>().color = neutralColor;
		}
	}


	override public void OnUse() {

		health -= currentSumAttack;

		if (health <= 0) {

			player.AddEXP(expValue);
			player.AddGold(goldValue);

			gameObject.name = "Deleted";
			Destroy(gameObject);
		}

	}
}
