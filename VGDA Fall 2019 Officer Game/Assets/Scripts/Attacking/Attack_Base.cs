using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Base : MonoBehaviour
{
    [SerializeField] private ObjectPooler bulletPool = null;

    public void Shoot(Vector3? position = null, Quaternion? rotation = null)
    {
        GameObject bullet = bulletPool.GetGenericBullet();
        if(bullet != null)
        {
            bullet.transform.position = position.HasValue ? position.Value : transform.position;
            bullet.transform.rotation = rotation.HasValue ? rotation.Value : transform.rotation;
            bullet.SetActive(true);
        }
    }
}
