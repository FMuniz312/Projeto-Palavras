 using UnityEngine;
 using System.Collections;
 using System;
 
 public class AICompanion2D : MonoBehaviour
 {
     public Transform target = null;                   // This is the Player. In editor drag the Player here for now. Will script how to search for the Player later on.                     
    public float minDistance = 5;

    public TargetRecord[] _recordsArray = null;    // keeps target data in time // type[] nameOfArray = lengthOfArray 
     public int _a = 0;                             // keeps current index for recorder
     public int _b = 1;                             // keeps current index for follower
     public TargetRecord _record = null;            // current record
     public int _arraySize = 15;                     // set to two so the array always has a value.
     public bool recordData = false;                // Start or stop recording data
 
     public bool isActivated = true;                // Will be used to check if helper is Activated to follow player.
 
     public void Start()
     {
         _recordsArray = new TargetRecord[_arraySize];
     }
 
     // update Follower transform data
     public void FixedUpdate()
     {
         // Player has either touched the helper or the helper was already following Player from last level.
         if (isActivated)
         {
             recordData = true; //Start recording player's positions
         }
         else
         {
             recordData = false; //Stop recording player's positions
         }
         if (recordData)
         {
             RecordData(Time.deltaTime);
         }
 
         // Apply movement.
         if(Vector3.Distance(target.position,transform.position) < minDistance)
        {
            Vector3 savePos = transform.position;
            savePos.y = Vector3.Lerp(_recordsArray[_a].position1, _recordsArray[_b].position1, Time.deltaTime).y; // Lerps from one recorded position to the next recorded position in the _recordsArray[].
            transform.position = savePos;
            transform.localScale = _recordsArray[_a].scale1; // Flips sprite or animation when the Player's scale was (-1,1,1).
        }
        else
        {
            transform.position = Vector3.Lerp(_recordsArray[_a].position1, _recordsArray[_b].position1, Time.deltaTime); // Lerps from one recorded position to the next recorded position in the _recordsArray[].
            transform.localScale = _recordsArray[_a].scale1; // Flips sprite or animation when the Player's scale was (-1,1,1).
        }
         
     }
 
     [Serializable] //Used to show array values in the inspector window while playing/testing game in Unity. 
 
     public class TargetRecord
     {
         public Vector3 position1; // World position
         public Vector3 scale1; // Scale of player. Used to flip sprite/animation.
 
         public TargetRecord(Vector3 position2, Vector3 scale2)
         {
             position1 = position2;
             scale1 = scale2;
         }
     }
 
     // Fill array list with Player positions.
     public void RecordData(float deltaTime)
     {
         // record target data
         _recordsArray[_a] = new TargetRecord(target.position, target.localScale);
 
         // set next record index
         if (_a < _recordsArray.Length - 1)
             _a++;
         else
             _a = 0;
 
         // set next follow index
         if (_b < _recordsArray.Length - 1)
             _b++;
         else
             _b = 0;
 
         // handle current record
         _record = _recordsArray[_b];
     }
 }