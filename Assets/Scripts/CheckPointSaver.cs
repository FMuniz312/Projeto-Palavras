using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaver : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager._instance.SetRespawnPos(transform.position);
            Destroy(this);
        }
    }
}
