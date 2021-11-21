using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class StartGA : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }
}
