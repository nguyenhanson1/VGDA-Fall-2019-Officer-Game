using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTest
{
    private int damageValue = 50;

    public BeamTest()
    {
    }
    public BeamTest(int damageValueSet)
    {
        damageValue = damageValueSet;
    }
    

    public void DealDamage(Health playerHealth)
    {
        playerHealth.HealthTotal -= damageValue;
    }
}
