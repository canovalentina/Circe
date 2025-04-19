using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(1366, 768, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
