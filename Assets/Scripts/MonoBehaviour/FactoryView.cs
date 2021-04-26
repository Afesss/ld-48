using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryView : MonoBehaviour
{
    [SerializeField]
    private GameObject prepareParts;

    [SerializeField]
    private GameObject[] factoryParts;

    [SerializeField]
    private UIStorageView storageView;

    private int nextLevel = 0;

    /// <summary>
    /// Обновляет процент наполнения склада
    /// </summary>
    /// <param name="rate">Процент наполнения</param>
    public void UpdateStorage(float rate)
    {
        storageView.SetValueRate(rate);
    }

    public void Upgrade()
    {
        if (nextLevel == 0)
        {
            storageView.gameObject.SetActive(true);
            if (prepareParts != null)
                prepareParts.SetActive(false);
        }

        if (nextLevel < factoryParts.Length)
        {
            factoryParts[nextLevel].SetActive(true);
            nextLevel++;
        }
    }

}
