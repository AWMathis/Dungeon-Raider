using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorClass : GamePiece {

    [SerializeField] int armorValue = 1;

    void Start() {
        base.Start();
    }

    public override void SetAttributes() {
        name = "Armor";
        actionType = "Armor";
    }

    override public void OnUse() {

        player.AddArmor(armorValue);

        gameObject.name = "Deleted";
        Destroy(gameObject);
    }
}
