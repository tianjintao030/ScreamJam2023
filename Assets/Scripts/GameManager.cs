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
    public int current_part=1;
    public List<DragObjConfig> dragObjConfigs = new List<DragObjConfig>();

    void Update()
    {
        SetDrapActiveByPart();

        for(int i=0;i<dragObjConfigs.Count;i++)
        {
            if(dragObjConfigs[i].drag.is_finshed && !dragObjConfigs[i].once)
            {
                if (dragObjConfigs[i].block_name != null)
                {
                    FlowchartManager.Instance.ExecuteBlockByName(dragObjConfigs[i].block_name);
                    if (dragObjConfigs[i].drag._audio != null)
                    {
                        dragObjConfigs[i].drag.PlayAudio();
                        if (dragObjConfigs[i].is_part_change)
                        {
                            AudioFinshNextPart(dragObjConfigs[i].drag._audio);
                        }
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
            if (current_part >= dragObjConfigs[i].drag.part)
            {
                dragObjConfigs[i].drag.gameObject.SetActive(true);
            }
        }
    }
}
