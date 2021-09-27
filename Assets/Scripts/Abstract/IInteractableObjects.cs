using System  ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableObjects 
{
     event EventHandler actionEnded;
    void CallWord(AbilityWords abilitywords);
}
