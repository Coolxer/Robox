﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public void onAnimationFinished()
    {
        Application.Quit();
    }
}
