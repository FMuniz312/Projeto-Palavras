using System.Collections;
using System  ;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IOBJBreakableWall : MonoBehaviour,IInteractableObjects 
{
    [SerializeField] Transform movePos2;
    [SerializeField, Min(1)] float moveSpeed;
    public event EventHandler actionEnded;


    Vector3 movePosOrigin;
    bool moved;
     private void Awake()
    {
        movePosOrigin = transform.position;
        if(movePos2 == null)
        {
            Debug.LogError("Posição secundária da parede não foi definida");
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
        Debug.Log("parede movida");

         if (!moved)
        {
            moved = !moved;
            //mover para segunda posição
            transform.DOMove(movePos2.position, 100/moveSpeed).OnComplete(() => actionEnded?.Invoke(this, EventArgs.Empty));
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
        Debug.Log("parede quebrada");
        //Quebrar plataforma e impulsionar o jogador

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
    }
}
