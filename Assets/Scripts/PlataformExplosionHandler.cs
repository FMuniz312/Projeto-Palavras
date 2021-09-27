using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformExplosionHandler : MonoBehaviour
{
    [Header("Balance")]
    [SerializeField] float explosionForce;
 
    bool playerOnTop;
    Rigidbody2D playersRB;

    public void CreateExplosion()
    {
        if (playerOnTop) playersRB.AddForce(Vector2.up*explosionForce,ForceMode2D.Impulse);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTop = true;
           if(playersRB == null) playersRB = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerOnTop = false;
    }
}
