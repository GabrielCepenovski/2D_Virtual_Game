using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFruit : Fruit
{
    public override void AdditionalAct(GameObject collector)
    {
        if (collector.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
            playerMover.ResetJumpIndex();
    }
}
