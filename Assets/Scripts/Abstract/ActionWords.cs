using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Interface;


public class ActionWords : AbstractWords
{
      public void DamageAction(IAttackTarget attackTarget)
    {
        //add visual and sound effects for choosing a action
        attackTarget.Attack(100);
    }
}
