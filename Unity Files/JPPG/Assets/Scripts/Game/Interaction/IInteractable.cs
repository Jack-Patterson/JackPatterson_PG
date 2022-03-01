using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable 
{
    CharacterControl.CharacterState interact(CharacterControl NPC);

}
