using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragObjConfig
{
    public Drag drag;
    public string block_name;
    public int part;
    public bool once;
}


public class GameManager: MonoSingleton<GameManager>
{
    public int current_part;
    private int one_num;
    private int two_num;
    private int three_num;
    private int four_num;
    private int five_num;
    private List<int> nums = new List<int>(6);

    [Header("电池，用于控制手电灯光效果")]
    public Drag battery;
    public GameObject FlashLight2D;
    [Header("按钮控制怪话姐")]
    public Drag button;

    public List<DragObjConfig> dragObjConfigs = new List<DragObjConfig>();

    public AudioSource start_audio;
    public AudioSource end_audio;
    private bool part_6_once;

    [Header("门的碎片")]
    public Drag door_suipian_a;
    public Drag door_suipian_b;

    [Header("怪话姐")]
    public GameObject holography;
    [Header("窗户破口")]
    public GameObject crack;
    [Header("结尾图片")]
    public GameObject end_Image;

    private void Start()
    {
        ReadPartConfig();
        GameStart();
    }

    void Update()
    {
        SetDrapActiveByPart();
        IsHaveFlashLight();
        PartSix();
        CheckEnd();
        HolographyActive();

        for(int i=0;i<dragObjConfigs.Count;i++)
        {
            if(dragObjConfigs[i].drag.is_finshed && !dragObjConfigs[i].once)
            {
                nums[dragObjConfigs[i].part]++;
                CheckIsFinshPart(dragObjConfigs[i].part, nums[dragObjConfigs[i].part]);

                if (dragObjConfigs[i].block_name != null)
                {
                    FlowchartManager.Instance.ExecuteBlockByName(dragObjConfigs[i].block_name);
                    if (dragObjConfigs[i].drag._audio != null)
                    {
                        dragObjConfigs[i].drag.PlayAudio();
                        
                    }
                }
                dragObjConfigs[i].once = true;
            }
        }
    }


    public void ReadPartConfig()
    {
        for(int i=0;i<dragObjConfigs.Count;i++)
        {
            if(dragObjConfigs[i].part==1)
            {
                one_num++;
            }
            if(dragObjConfigs[i].part ==2)
            {
                two_num++;
            }
            if(dragObjConfigs[i].part ==3)
            {
                three_num++;
            }
            if(dragObjConfigs[i].part ==4)
            {
                four_num++;
            }
            if(dragObjConfigs[i].part ==5)
            {
                five_num++;
            }
        }
    }

    public void CheckIsFinshPart(int part,int current_finsh_num)
    {
        if(part==1 && current_finsh_num==one_num)
        {
            current_part = 2;
        }
        if(part==2 && current_finsh_num==two_num)
        {
            current_part = 3;
        }
        if(part==3 && current_finsh_num==three_num)
        {
            current_part = 4;
        }
        if(part==4 && current_finsh_num==four_num)
        {
            current_part = 5;
        }
        if(part==5 && current_finsh_num==five_num)
        {
            current_part = 6;
        }
    }

    public void SetDrapActiveByPart()
    {
        for (int i = 0; i < dragObjConfigs.Count; i++)
        {
            if (current_part >= dragObjConfigs[i].drag.part && !dragObjConfigs[i].drag.is_finshed)
            {
                dragObjConfigs[i].drag.gameObject.SetActive(true);
            }
        }
    }

    public void IsHaveFlashLight()
    {
        if(battery.is_finshed)
        {
            FlashLight2D.SetActive(true);
        }
    }

    public void GameStart()
    {
        FlowchartManager.Instance.ExecuteBlockByName("Start");
        start_audio.Play();
    }

    public void HolographyActive()
    {
        if(button.is_finshed)
        {
            holography.SetActive(true);
        }
    }

    public void Crack()
    {
        if(current_part>=5)
        {
            crack.SetActive(true);
        }
    }

    public void PartSix()
    {
        if(current_part>=6 && !part_6_once)
        {
            part_6_once = true;
            FlowchartManager.Instance.ExecuteBlockByName("End");
            end_audio.Play();
            start_audio.Play();
        }
    }

    public void CheckEnd()
    {
        if(door_suipian_a.is_finshed && door_suipian_b.is_finshed)
        {
            end_Image.SetActive(true);
            StartCoroutine(CheckEndIE());
        }
    }
    private IEnumerator CheckEndIE()
    {
        yield return new WaitForSecondsRealtime(5f);
        Application.Quit();
    }
}
