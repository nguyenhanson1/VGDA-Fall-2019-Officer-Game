using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrentLook : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 2f;
    public ObjectPooler pool;
    public float accuracy;
    private bool attacking = true;
    private Attack attack = new Attack();

    [SerializeField] private Vector3 offset = new Vector3();
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - this.transform.position;

        if (direction.z > 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z)),
                                                Time.deltaTime * rotSpeed);
        }
        else if (direction.z <= 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z)),
                                                Time.deltaTime * rotSpeed);
        }
        Debug.DrawLine(transform.position, target.transform.position, Color.red);

        if (attacking)
        {
            GameObject bullet = attack.Shoot(gameObject, pool,new Quaternion(Random.Range(gameObject.transform.rotation.x - accuracy, gameObject.transform.rotation.x + accuracy),
                                                                             Random.Range(gameObject.transform.rotation.y - accuracy, gameObject.transform.rotation.y + accuracy),
                                                                             Random.Range(gameObject.transform.rotation.z - accuracy, gameObject.transform.rotation.z + accuracy),
                                                                             gameObject.transform.rotation.w));
            bullet.transform.rotation = Quaternion.Euler(offset.x + transform.rotation.x, offset.y + transform.rotation.y, offset.z + transform.rotation.z);
        }
    }
}
