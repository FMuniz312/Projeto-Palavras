using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Interface;

public class WORDActionDamage : ActionWords
{
    
    public   void Action(IAttackTarget attackTarget)
    {
         attackTarget.Attack(100);
    }
}
