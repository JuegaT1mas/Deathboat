using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initial : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
}
