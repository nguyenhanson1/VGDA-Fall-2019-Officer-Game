using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Base : MonoBehaviour
{
    [Tooltip("Script where object will get their bullets from.")]
    [SerializeField] private ObjectPooler bulletPool = null;

    //Fire Bullet
    public void Shoot(Quaternion? rotation = null, Vector3? position = null)
    {
        //Get bullet from function in ObjectPooler script
        GameObject bullet = bulletPool.GetGenericBullet();
        if(bullet != null)
        {
            //Set transform/rotation of bullet to the same of the object if it's not preset
            bullet.transform.position = position.HasValue ? position.Value : transform.position;
            bullet.transform.rotation = rotation.HasValue ? rotation.Value : transform.rotation;
            //Activate the bullet
            bullet.SetActive(true);
        }
    }
}
