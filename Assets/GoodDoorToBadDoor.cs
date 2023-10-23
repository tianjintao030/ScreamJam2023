using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodDoorToBadDoor : MonoBehaviour
{
    public GameObject gooddoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.current_part >= 6)
        {
            gooddoor.SetActive(false);
        }
    }
}
