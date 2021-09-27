using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

public class IOBJPlataform : MonoBehaviour,IInteractableObjects 
{
    [SerializeField] Transform movePos2;
    [SerializeField, Min(1)] float moveSpeed;

    [SerializeField,InlineEditor] PlataformExplosionHandler plataformExplosionHandler;
    public event EventHandler actionEnded;
   
    Vector3 movePosOrigin;
    bool moved;
     
     private void Awake()
    {
        movePosOrigin = transform.position;
        if(movePos2 == null)
        {
            Debug.LogError("Posição secundária da plataforma não foi definida");
        }
    }

    public void CallWord(AbilityWords abilityWords)
    {
        if(abilityWords == AbilityWords.Move)
        {
            Move();
        }
        else if (abilityWords == AbilityWords.Damage)
        {
            Break();
        }
        else
        {
            Debug.Log("Ação não aceita por esse objeto");
        }
    } 
     public void Move()
    {
        Debug.Log("Plataforma movida");

         if (!moved)
        {
            moved = !moved;
            //mover para segunda posição
            transform.DOMove(movePos2.position, 100 / moveSpeed).OnComplete(()=>actionEnded?.Invoke(this, EventArgs.Empty));
        }
        else
        {
            moved = !moved;

            //mover para posição de origem
            transform.DOMove(movePosOrigin, 100 / moveSpeed).OnComplete(() => actionEnded?.Invoke(this, EventArgs.Empty));

        }
    }

    public void Break()
    {
        Debug.Log("Plataforma quebrada");
        //Quebrar plataforma e impulsionar o jogador
        plataformExplosionHandler.CreateExplosion();
          Destroy(gameObject);
    }
     
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);

        }
    }



    private void OnDestroy()
    {
    }
}
