using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("应该被摆放的正确的位置")]
    public Transform correct_trans;//应该把它正确放置的transform
    private Vector2 start_pos;//初始时的位置
    public bool is_selected;//是否被选中
    public bool is_finshed;//是否已经完成放置
    private FloatingObj float_obj;

    void Start()
    {
        start_pos = transform.position;
        float_obj = GetComponent<FloatingObj>();
    }

    private void OnMouseDrag()
    {
        float_obj.is_floation = false;
        is_selected = true;
        FollowMouse();
    }

    private void OnMouseUp()
    {
        is_selected = false;
        float_obj.is_floation = true;
        float_obj.ResetOriginPos();

        //该对象位置与正确放置位置相近则完成
        if(Mathf.Abs(correct_trans.position.x-transform.position.x)<0.5f &&
            Mathf.Abs(correct_trans.position.y-transform.position.y)<0.5f)
        {
            float_obj.is_floation = false;
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
   
}
