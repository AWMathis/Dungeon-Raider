using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionClass : GamePiece
{
    [SerializeField] int healthValue = 1;

    void Start() {
        base.Start();
    }

    public override void SetAttributes() {
        name = "Health Potion";
        actionType = "Potion";
    }

    override public void OnUse() {

        player.AddHealth(healthValue);

        gameObject.name = "Deleted";
        Destroy(gameObject);
    }
}
