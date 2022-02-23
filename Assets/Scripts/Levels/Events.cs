using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static partial class Events
{
    public static event Action OnFingerSwipe;
    public static void DoFireOnFingerSwipe() => OnFingerSwipe?.Invoke();
}
