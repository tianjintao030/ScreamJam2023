using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Transform correct_trans;//应该把它正确放置的transform
    private Vector2 start_pos;//初始时的位置
    public bool is_selected;//是否被选中
    public bool is_finshed;//是否已经完成放置
    public bool is_floation;//是否是漂浮物
    public Vector2 floatiion_offsetXY;//漂浮的范围
    public float floation_amplitude;//漂浮的幅度
    public float Timer = 0;//漂浮效果的计时器
    public float floation_move_interval;//漂浮时的随机变化时间间隔

    void Start()
    {
        start_pos = transform.position;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        Floation();
    }

    private void OnMouseDrag()
    {
        is_selected = true;
        FollowMouse();
    }

    private void OnMouseUp()
    {
        is_selected = false;

        //该对象位置与正确放置位置相近则完成
        if(Mathf.Abs(correct_trans.position.x-transform.position.x)<0.5f &&
            Mathf.Abs(correct_trans.position.y-transform.position.y)<0.5f)
        {
            transform.position = new Vector2(correct_trans.position.x, correct_trans.position.y);
            transform.parent = correct_trans;
            is_finshed = true;
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.07f;
    }

    private void FollowMouse()
    {
        if (!is_finshed)
        {
            Vector2 current_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(current_pos.x, current_pos.y);
        }
    }

    private void Floation()
    {
        if(is_floation && !is_selected && Timer>=floation_move_interval)
        {
            int x;
            x = Random.Range(0, 4);

            if(x==0)
            {
                transform.position = start_pos + new Vector2(
                    floatiion_offsetXY.x * Mathf.Sin(Time.time * floation_amplitude),
                    floatiion_offsetXY.y * Mathf.Sin(Time.time * floation_amplitude));

                Timer = 0;
            }
            if(x==1)
            {
                transform.position = start_pos + new Vector2(
                    floatiion_offsetXY.x * Mathf.Cos(Time.time * floation_amplitude),
                    floatiion_offsetXY.y * Mathf.Sin(Time.time * floation_amplitude));

                Timer = 0;
            }
            if(x==2)
            {
                transform.position = start_pos + new Vector2(
                    floatiion_offsetXY.x * Mathf.Sin(Time.time * floation_amplitude),
                    floatiion_offsetXY.y * Mathf.Cos(Time.time * floation_amplitude));

                Timer = 0;
            }
            if(x==3)
            {
                transform.position = start_pos + new Vector2(
                    floatiion_offsetXY.x * Mathf.Cos(Time.time * floation_amplitude),
                    floatiion_offsetXY.y * Mathf.Cos(Time.time * floation_amplitude));

                Timer = 0;
            }
        }
    }
   
}
