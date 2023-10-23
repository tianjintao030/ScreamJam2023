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
    private SpriteRenderer renderer;
    [Header("�����׶�")]
    public int part;
    public AudioSource _audio;
    [Header("���ú��Ƿ���ʧ")]
    public bool is_sleep_finshed;
    [Header("���ú���õ�λ���Ƿ���ʧ")]
    public bool is_pos_sleep_finshed;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        start_pos = transform.position;
        float_obj = GetComponent<FloatingObj>();
        if(GetComponent<AudioSource>()!=null)
        {
            _audio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        CheckPart();
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
            if (is_sleep_finshed)
            {
                renderer.enabled = false;
            }
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

    public void CheckPart()
    {
        if(GameManager.Instance.current_part>=part)
        {
            gameObject.SetActive(true);
        }
    }

    public void PlayAudio()
    {
        _audio.Play();
    }
}
