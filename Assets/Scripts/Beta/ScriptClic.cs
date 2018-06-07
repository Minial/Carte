using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ScriptClic : MonoBehaviour
{
    public bool Clic = false;
    void OnMouseUpAsButton()
    {
        Clic = true;
    }
}