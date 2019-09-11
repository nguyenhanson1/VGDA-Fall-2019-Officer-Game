using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public int damage = 0;
    [SerializeField] private float despawnTime = 5f;

    [SerializeField] private Rigidbody rb = null;



    private void OnEnable()
    {
        rb.velocity = speed * transform.forward;
        StartCoroutine(trackBullet());
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private IEnumerator trackBullet()
    {
        yield return new WaitForSeconds(despawnTime);

        gameObject.SetActive(false);
    }
}
