using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Debuger : MonoBehaviour
{

    private Money money;

    [Inject]
    private void Construct(Money money)
    {
        this.money = money;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
            money.AddMoney(100);
        
    }
}
