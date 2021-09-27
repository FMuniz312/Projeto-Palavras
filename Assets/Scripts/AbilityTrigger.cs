using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AbilityTrigger : MonoBehaviour
{
    [VerticalGroup]
    public  GameObject PlataformActive ;
    [VerticalGroup]
    public GameObject BreakableWallActive;
    

    private void Start()
    {
        if (PlataformActive == null)
        {
            PlataformActive =  transform.Find("IOBJ_Plataforms").gameObject;
        }
        if (BreakableWallActive == null)
        {
            BreakableWallActive = transform.Find("IOBJ_Wall").gameObject;
        }
         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            AbilityManager._instance.SetAbilityTriggerData(this);
            AbilityManager._instance.allowAbility = true;
        
        }
        }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) AbilityManager._instance.allowAbility = false;
    }

    
    
    
}
