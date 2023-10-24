using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [Header("���ƫ����")]
    public Vector2 offset;
    [Header("��Ƶ��")]
    public float frequency;
    private Vector2 originPosition;
    private float tick;
    private float amplitude;//���
    [Header("�Ƿ�Ư���ƶ�")]
    public bool is_floation=true;
    private Drag drag;
    
    void Start()
    {
        drag = GetComponent<Drag>();
        // ���û������Ƶ�ʻ������õ�Ƶ��Ϊ0���Զ���¼��1
        if (Mathf.Approximately(frequency, 0))
            frequency = 1f;

        originPosition = transform.localPosition;
        tick = Random.Range(0f, 2f * Mathf.PI);
        // �������
        amplitude = 2 * Mathf.PI / frequency;
    }

    void FixedUpdate()
    {
        if(is_floation)
        {
            // ������һ��ʱ����
            tick = tick + Time.fixedDeltaTime * amplitude;
            // ������һ��ƫ����
            var amp = new Vector2(Mathf.Cos(tick) * offset.x, Mathf.Sin(tick) * offset.y);
            // ��������
            transform.localPosition = originPosition + amp;
        }
    }

    public void ResetOriginPos()
    {
        originPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag=="bond")
        {
            is_floation = false;
            drag.can_follow_mouse = false;
            transform.position = coll.GetComponent<SceneBond>().transmit_pos.position;
            StartCoroutine(RecoverFloating());
        }
    }

    IEnumerator RecoverFloating()
    {
        yield return new WaitForSeconds(1f);
        is_floation = true;
        drag.can_follow_mouse = true;
    }
}
