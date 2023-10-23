using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragObjConfig
{
    public Drag drag;
    public string block_name;
    public bool is_part_change;
    public bool once;
}


public class GameManager: MonoSingleton<GameManager>
{
    public int current_part;
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
                if (dragObjConfigs[i].is_part_change)
                {
                    AudioFinshNextPart(dragObjConfigs[i].drag._audio);
                }
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

    public void AudioFinshNextPart(AudioSource _audio)
    {
        StartCoroutine(ChangeToNextPart(_audio.clip.length));
    }

    public IEnumerator ChangeToNextPart(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        current_part++;
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
