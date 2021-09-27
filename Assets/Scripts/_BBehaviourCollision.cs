using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;
public class _BBehaviourCollision : BaseBulletBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    public void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, .1f, (int)groundLayerMask)!=null)
        {
            bullet.Die();
        }
    }
}
