﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorer : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Color color;

    private void OnEnable()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    public void ChangeColor()
    {
        sprite.color = color;
    }
}