using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player : Bullet_Base
{
    private void OnTriggerEnter(Collider col)
    {

        if(col.GetComponent<Enemy>() != null)
        {
            //Do damage to enemy ---------------------------------------------------
            gameObject.SetActive(false);
        }
    }
}
