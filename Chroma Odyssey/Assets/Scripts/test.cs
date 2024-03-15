using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fixed main branch

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed - Test");
        }
    }
}
