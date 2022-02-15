using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable 
{
    AICharacterMove.CharacterState interact(AICharacterMove NPC);

}
