using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableObject
{
    void OnClicked();
    void OnThrown();
    void OnMouseHover();
    bool IsHeld();
}
