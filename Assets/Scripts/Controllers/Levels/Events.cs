using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static partial class Events 
{
    public static event Action OnSetLevel;
    public static void DoFireOnSetLevel() => OnSetLevel?.Invoke();

    public static event Action OnSkipLevel;
    public static void DoFireOnOnSkipLevel() => OnSkipLevel?.Invoke();
}
