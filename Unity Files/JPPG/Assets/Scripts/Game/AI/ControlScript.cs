using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ControlScript : MonoBehaviour
{


    void Start()
    {

    }

    
    void Update()
    {
        
    }

    internal abstract void setTarget(Vector3 position);

    internal abstract void CharacterStates();


}
