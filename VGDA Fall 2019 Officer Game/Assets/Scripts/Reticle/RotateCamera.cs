using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void OnEnable()
    {
        GameManager.UpdateOccurred += SetRotation;
    }
    private void OnDisable()
    {
        GameManager.UpdateOccurred -= SetRotation;
    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * 30);
    }
}
