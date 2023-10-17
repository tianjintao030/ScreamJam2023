using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoSingleton<Flashlight>
{
    public GameObject lightObj;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        LightFollowMouse();
    }

    public void EnableLight()
    {
        lightObj.SetActive(true);
    }
    public void UnableLight()
    {
        lightObj.SetActive(false);
    }

    private void LightFollowMouse()
    {
        Vector2 current_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lightObj.transform.position = new Vector2(current_pos.x, current_pos.y);
    }
}
