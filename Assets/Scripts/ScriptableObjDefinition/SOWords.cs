using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Words")]
public class  SOWords : ScriptableObject
{
    public AbilityWords words;


}

public enum AbilityWords
{
    NONE,
    Door,
    Wall,
    Plataform,
    Damage,
    Move,
    Now,
    Later
}


