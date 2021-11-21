using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPbutton : MonoBehaviour
{
    public void BuyNoAds()
    {
        IAPManager.instance.BuyRemoveAds();
    }


}
