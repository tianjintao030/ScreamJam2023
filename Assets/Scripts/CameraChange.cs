using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    public List<CinemachineVirtualCamera> camera_list = new List<CinemachineVirtualCamera>();
    private int index;

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
}
