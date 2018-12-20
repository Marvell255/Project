using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalBank : MonoBehaviour
{
    public Transform Container;
    public GameObject ItemPrefab;

    private void Start()
    {
        // todo clean

        for (var i = 0; i < 6; i++)
        {
            var item = Instantiate(ItemPrefab, Container);
            var itemScript = item.GetComponent<Item>();
            itemScript.Setup(" value = " + i);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}