using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPositionSgow : MonoBehaviour
{
    public Drag button;
    public GameObject white;
    private bool once;

    private void Update()
    {
        if(!button.is_finshed && !once)
        {
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash()
    {
        once = true;
        white.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        white.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        once = false;
    }
}
