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
    public int one_num;
    public int two_num;
    public int three_num;
    public int four_num;
    public int five_num;
    public int[] nums = { 0, 0, 0, 0, 0, 0 };

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

    [Header("管道")]
    public Drag blue;
    public Drag red;
    public GameObject blue_fix_before_1;
    public GameObject blue_fix_before_2;
    public GameObject red_fix_before_1;
    public GameObject red_fix_before_2;
    public GameObject blue_fix_after;
    public GameObject red_fix_after;
    [Header("植物血迹")]
    public GameObject plant_blood;

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
        Crack();
        Piple();
        PlantBlood();

        #region 语音，字幕，阶段切换
        for (int i=0;i<dragObjConfigs.Count;i++)
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
        #endregion
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

    public void PlantBlood()
    {
        if(current_part>=4)
        {
            plant_blood.SetActive(true);
        }
    }

    public void Piple()
    {
        if(blue.is_finshed)
        {
            blue_fix_before_1.SetActive(false);
            blue_fix_before_2.SetActive(false);
            blue_fix_after.SetActive(true);
        }
        if (red.is_finshed)
        {
            red_fix_before_1.SetActive(false);
            red_fix_before_2.SetActive(false);
            red_fix_after.SetActive(true);
        }
    }
}
