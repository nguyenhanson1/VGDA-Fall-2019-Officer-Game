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

    public GameObject ScatterShoot(GameObject parent, ObjectPooler bulletPool, float scatterMagnitude, Quaternion? rotation = null, Vector3? position = null)
    {
        Quaternion randRotation = (rotation != null) ? rotation.Value : parent.transform.rotation;
        
        randRotation = Quaternion.RotateTowards(randRotation, Random.rotation, Random.Range(0f, scatterMagnitude));

        return Shoot(parent, bulletPool, randRotation, position);
    }

    public static Vector3 leadShotPos(Vector3 selfPos, float bulletSpeed, Vector3 targetPos, Vector3 targetVel)
    {
        //Get relative position
        Vector3 targetRelativePos = targetPos - selfPos;

        //Get time
        float time;

        float acceleration = targetVel.sqrMagnitude - Mathf.Pow(bulletSpeed, 2);
        if (Mathf.Abs(acceleration) < 0.001f)
        {
            float t = -targetRelativePos.sqrMagnitude / (2f * Vector3.Dot(targetVel, targetRelativePos));
            time = Mathf.Max(t, 0f); //don't shoot back in time
        }
        else
        {
            float b = 2f * Vector3.Dot(targetVel, targetRelativePos);
            float c = targetRelativePos.sqrMagnitude;
            float determinant = b * b - 4f * acceleration * c;

            if (determinant > 0f)
            { //determinant > 0; two intercept paths (most common)
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * acceleration),
                        t2 = (-b - Mathf.Sqrt(determinant)) / (2f * acceleration);
                if (t1 > 0f)
                {
                    if (t2 > 0f)
                        time = Mathf.Min(t1, t2); //both are positive
                    else
                        time = t1; //only t1 is positive
                }
                else
                    time = Mathf.Max(t2, 0f); //don't shoot back in time
            }
            else if (determinant < 0f) //determinant < 0; no intercept path
                time = 0f;
            else //determinant = 0; one intercept path, pretty much never happens
                time = Mathf.Max(-b / (2f * acceleration), 0f); //don't shoot back in time
        }

        //Return results
        return targetPos + (time * targetVel);
    }

    /*
    public IEnumerator LerpFire(Vector3 travel, GameObject parent, ObjectPooler bulletPool, Vector3? lookAt = null, Vector3? position = null)
    {
        //Get bullet from function in ObjectPooler script
        GameObject bullet = bulletPool.GetGenericBullet();
        if (bullet != null)
        {
            bullet.transform.LookAt(lookAt.Value);
            bullet.GetComponent<Bullet>().Lerping(true, travel);

            //Set transform/rotation of bullet to the same of the object if it's not preset
            bullet.transform.position = position.HasValue ? position.Value : parent.transform.position;
            bullet.transform.rotation = lookAt.HasValue ? bullet.transform.rotation : parent.transform.rotation;

            //Activate the bullet
            bullet.SetActive(true);
            yield return new WaitUntil(() => !bullet.GetComponent<Bullet>().Lerping());
            Reallign(bullet, lookAt.Value);
        }
    }
    //Fire Bullet
    public void Reallign(GameObject bullet, Vector3 lookAt)
    {
        if (bullet != null)
        {
            //Set transform/rotation of bullet to the same of the object if it's not preset
            bullet.transform.LookAt(lookAt);

        }
    }
    */
}
