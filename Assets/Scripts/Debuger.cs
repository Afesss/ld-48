using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Debuger : MonoBehaviour
{

    private Money money;
    private Dump dump;

    [Inject]
    private void Construct(Money money, Dump dump)
    {
        this.money = money;
        this.dump = dump;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            //money.AddMoney(100);
            dump.GetResources();
        }
        
    }
}
