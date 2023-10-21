using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("Ӧ�ñ��ڷŵ���ȷ��λ��")]
    public Transform correct_trans;//Ӧ�ð�����ȷ���õ�transform
    private Vector2 start_pos;//��ʼʱ��λ��
    public bool is_selected;//�Ƿ�ѡ��
    public bool is_finshed;//�Ƿ��Ѿ���ɷ���
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

        //�ö���λ������ȷ����λ����������
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
