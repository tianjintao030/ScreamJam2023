using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
    public List<CinemachineVirtualCamera> camera_list = new List<CinemachineVirtualCamera>();
    private int index;
    public CinemachineVirtualCamera large_camera;
    private bool is_larging;
    public GameObject left;
    public GameObject right;

    void Update()
    {
        CameraChangeByIndex();
    }

    public void IndexAdd()
    {
        index++;
        if(index>=camera_list.Count)
        {
            index = 0;
        }
    }

    public void IndexSub()
    {
        index--;
        if(index<0)
        {
            index = camera_list.Count - 1;
        }
    }

    private void CameraChangeByIndex()
    {
        for(int i=0;i<camera_list.Count;i++)
        {
            if(i==index)
            {
                camera_list[i].Priority = 10;
                break;
            }
            else
            {
                camera_list[i].Priority = 1;
            }
        }
    }

    public void Enlarge()
    {
        if(!is_larging)
        {
            is_larging = true;
            large_camera.Priority = 11;
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
        }
        else if(is_larging)
        {
            is_larging = false;
            large_camera.Priority = 1;
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
        }
        
    }
}
