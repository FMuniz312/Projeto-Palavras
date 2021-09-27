using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Spawn
   [SerializeField] Transform levelSpawnPos;
    Vector3 respawnPos;
    //

    [SerializeField] GameObject player;

    public static GameManager _instance;

    void Awake()
    {
        if (_instance == null) _instance = this;
    }


    public void RespawnPlayer()
    {
        player.transform.position = respawnPos;
    }
   public void LevelSpawnPlayer()
    {
        player.transform.position = levelSpawnPos.position;
    }

     
    public void SetRespawnPos(Vector3 pos)
    {
        respawnPos = pos;
    }


}
