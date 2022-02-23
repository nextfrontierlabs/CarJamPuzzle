using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static partial class Events
{
    public static event Action<Screens> OnShowScreen;
    public static void DoFireOnShowScreen(Screens screen) => OnShowScreen?.Invoke(screen);
}
