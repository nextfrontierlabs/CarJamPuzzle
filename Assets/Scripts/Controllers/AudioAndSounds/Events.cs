using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static partial class Events  
{
    public static event Action OnChangeMusicState;
    public static void DoFireOnChangeMusicState() => OnChangeMusicState?.Invoke();

    public static event Action OnPlayClickBtn;
    public static void DoFireOnPlayClickBtn() => OnPlayClickBtn?.Invoke();
}
