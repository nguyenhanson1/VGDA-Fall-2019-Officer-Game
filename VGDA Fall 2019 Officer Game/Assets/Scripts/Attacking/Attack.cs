using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    //Fire Bullet
    public GameObject Shoot(GameObject parent, ObjectPooler bulletPool, Quaternion? rotation = null, Vector3? position = null)
    {
        //Get bullet from function in ObjectPooler script
        GameObject bullet = bulletPool.GetGenericBullet();
        if(bullet != null)
        {
            if (rotation.HasValue)
            {
                bullet.GetComponent<Bullet>().lockOn = true;
            }
            //Set transform/rotation of bullet to the same of the object if it's not preset
            bullet.transform.position = position.HasValue ? position.Value : parent.transform.position;
            bullet.transform.rotation = rotation.HasValue ? rotation.Value : parent.transform.rotation;
            //Activate the bullet
            bullet.SetActive(true);
            
        }
        return bullet;
    }
}
