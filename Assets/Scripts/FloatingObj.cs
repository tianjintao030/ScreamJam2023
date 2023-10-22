using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [Header("最大偏移量")]
    public Vector2 offset;
    [Header("振动频率")]
    public float frequency;
    private Vector2 originPosition;
    private float tick;
    private float amplitude;//振幅
    [Header("是否漂浮移动")]
    public bool is_floation=true;
    

    void Start()
    {
        // 如果没有设置频率或者设置的频率为0则自动记录成1
        if (Mathf.Approximately(frequency, 0))
            frequency = 1f;

        originPosition = transform.localPosition;
        tick = Random.Range(0f, 2f * Mathf.PI);
        // 计算振幅
        amplitude = 2 * Mathf.PI / frequency;
    }

    void FixedUpdate()
    {
        if(is_floation)
        {
            // 计算下一个时间量
            tick = tick + Time.fixedDeltaTime * amplitude;
            // 计算下一个偏移量
            var amp = new Vector2(Mathf.Cos(tick) * offset.x, Mathf.Sin(tick) * offset.y);
            // 更新坐标
            transform.localPosition = originPosition + amp;
        }
    }

    public void ResetOriginPos()
    {
        originPosition = transform.localPosition;
    }
}
