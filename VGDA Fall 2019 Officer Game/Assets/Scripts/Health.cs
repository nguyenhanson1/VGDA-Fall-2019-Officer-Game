using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    //public Action OnDeath = delegate {};
    public delegate void _OnDeath(Health dead);
    public static event _OnDeath OnDeath;
    
    private int _healthTotal = 100;

    public int HealthTotal
    {
        get => _healthTotal;
        set
        {
            _healthTotal = value;
            if(_healthTotal <= 0)
                if (OnDeath != null)
                {
                    //OnDeath.Invoke();
                    OnDeath.Invoke(this);
                }
            
        }
    }

    public void subtractHealth(int damage)
    {
        HealthTotal -= damage;
    }
    public void addHealth(int damage)
    {
        HealthTotal += damage;
    }

}
