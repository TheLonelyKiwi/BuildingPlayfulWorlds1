using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayercarry
{
    bool IsMoving { get; set; }
    bool IsCarrying { get; set; }
    
    void SetIsMoving(bool isMoving);
    void SetIsCarrying(bool isCarrying);
}
