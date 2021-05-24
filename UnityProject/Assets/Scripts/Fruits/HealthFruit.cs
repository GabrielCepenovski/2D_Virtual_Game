using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFruit : Fruit
{
    public override void AdditionalAct(GameObject collector)
    {
        if(collector.TryGetComponent<Player>(out Player player))
            player.ResetHealth();
    }
}
