using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWords : MonoBehaviour
{
    [SerializeField] SOWords soWords;

    virtual public SOWords GetWordInfo()
    {
        return soWords;
    }


}
