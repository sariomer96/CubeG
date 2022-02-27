using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events _instance;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }
    public event Action<int> onTriggerColllectDiamonds;
    public event Action damageHealth;
    public void TriggerCollectDiamonds(int diamondValue)
    {
        onTriggerColllectDiamonds?.Invoke(diamondValue);
    }

    public void DamageHealth()
    {
        damageHealth?.Invoke();
    }

}
