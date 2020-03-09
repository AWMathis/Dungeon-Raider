using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldClass : GamePiece {

    [SerializeField] int goldValue = 1;

    void Start() {
        base.Start();
    }

    public override void SetAttributes() {
        name = "Gold Piece";
        actionType = "Gold";
    }

    override public void OnUse() {

        player.AddGold(goldValue);

        gameObject.name = "Deleted";
        Destroy(gameObject);
    }
}