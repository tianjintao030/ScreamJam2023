using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public CameraChange cc;

    private void OnMouseDown()
    {
        cc.Enlarge();
    }
}
