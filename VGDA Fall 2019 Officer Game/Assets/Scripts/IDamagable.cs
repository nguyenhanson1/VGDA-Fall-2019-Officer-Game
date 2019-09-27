using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    Factions.Faction myFaction
    {
        get;
    }

    Health health { get; }
}
