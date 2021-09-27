using System.Collections;
using System.Collections.Generic;
using MunizCodeKit.Systems;
using MunizCodeKit.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
     PointsSystem healthSystem;

    public void Attack()
    {
        Debug.Log("Player foi atacado");
    }

    public void SpikeDamage(float damage, float pushforce, Vector2 normal)
    {
        Debug.Log($"Player recebeu {damage} de dano");
        GetComponent<Rigidbody2D>().AddForce( -normal.normalized * pushforce, ForceMode2D.Impulse);
    }
     

    [Button("Setup Player")]
    private void SetupPlayer()
    {
        healthSystem = new PointsSystem(3, 3);


        
    }

}
