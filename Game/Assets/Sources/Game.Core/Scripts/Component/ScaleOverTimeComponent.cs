using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;
using X.Common.Know;

public class ScaleOverTimeComponent : MonoBehaviour
{
    public SpriteRenderer spr = default;
    public float speed = default;
    private Color c = default;
    private float countDelta = 0f;
    void Update()
    {
        this.c = spr.color;
        countDelta += speed * Time.deltaTime;
        this.c.a = Mathf.PingPong(countDelta, 1).Min(.1f).Max(1);
        spr.color = this.c;
    }
}
