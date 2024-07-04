using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [System.Serializable]
    public class ItemDropInfo  
    {
        public GameObject item;
        public int minDrop;
        public int maxDrop;
    }

    [SerializeField] private ItemDropInfo[] itemDropInfo;
    [SerializeField] private int minTotalDrops = 1;
    [SerializeField] private int maxTotalDrops = 3;

    /*public void ItemDrop()
    {
        int totalDrops = Random.Range(minTotalDrops, maxTotalDrops + 1);

        for (int i = 0; i < itemDropInfo.Length; i++)
        {
            int numDrops = Random.Range(itemDropInfo[i].minDrop, itemDropInfo[i].maxDrop + 1);

            for (int j = 0; j < numDrops; j++)
            {
                Instantiate(itemDropInfo[i].item, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }*/

    public void ItemDrop()
    {
        int totalDrops = 0;
        int[] numDropsPerItem = new int[itemDropInfo.Length];

        for (int i = 0; i < itemDropInfo.Length; i++)
        {
            numDropsPerItem[i] = Random.Range(itemDropInfo[i].minDrop, itemDropInfo[i].maxDrop + 1);
            totalDrops += numDropsPerItem[i];
        }

        if (totalDrops > maxTotalDrops)
        {
            // Ajustar el número de ítems a dropar para que no exceda el máximo
            for (int i = 0; i < itemDropInfo.Length; i++)
            {
                numDropsPerItem[i] = Mathf.Min(numDropsPerItem[i], maxTotalDrops - (totalDrops - numDropsPerItem[i]));
                totalDrops = Mathf.Min(totalDrops, maxTotalDrops);
            }
        }

        for (int i = 0; i < itemDropInfo.Length; i++)
        {
            for (int j = 0; j < numDropsPerItem[i]; j++)
            {
                Instantiate(itemDropInfo[i].item, transform.position , Quaternion.identity);
            }
        }
    }


}
