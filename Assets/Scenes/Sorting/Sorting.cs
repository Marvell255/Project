using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    public GameObject Prefab;
    private List<GameObject> _list;

    private void Start()
    {
        _list = new List<GameObject>();
        Generate();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 36), "Generate"))
        {
            Generate();
        }

        if (GUI.Button(new Rect(10, 50, 150, 36), "Sort"))
        {
            Sort();
        }

        if (GUI.Button(new Rect(10, 100, 150, 36), "Action"))
        {
            Generate();
            Sort();
        }

        if (GUI.Button(new Rect(10, 150, 150, 36), "Alternative"))
        {
            Generate();
            StartCoroutine(Sort2());
        }

//        if (GUI.Button(new Rect(10, 150, 150, 36), "Radix"))
//        {
//            Generate();
//            StartCoroutine(Radix());
//        }
    }

    private void Generate()
    {
        foreach (var o in _list)
            Destroy(o);

        _list.Clear();

        for (var i = 0; i < 10; i++)
        {
            var capsule = Instantiate(Prefab, GetPosition(i), transform.rotation, transform);
            capsule.transform.localScale = new Vector3(1, Random.Range(.5f, 3.5f), 1);
            _list.Add(capsule);
        }
    }

    private void Sort()
    {
        for (var i = 0; i < _list.Count; i++)
        for (var j = 0; j < _list.Count - 1 - i; j++)
        {
            var capsule1 = _list[j];
            var capsule2 = _list[j + 1];

            if (capsule1.transform.localScale.y > capsule2.transform.localScale.y)
            {
                var tmpPos = capsule1.transform.localPosition;

                capsule1.transform.localPosition = capsule2.transform.localPosition;

                capsule2.transform.localPosition = tmpPos;

                var tmpGameObject = capsule1;
                _list[j] = capsule2;
                _list[j + 1] = tmpGameObject;
            }
        }
    }

    private IEnumerator Sort2()
    {
        for (var i = 0; i < transform.childCount; i++)
        for (var j = 0; j < transform.childCount - 1 - i; j++)
        {
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();

            var capsule1 = transform.GetChild(j);
            var capsule2 = transform.GetChild(j + 1);

            if (capsule1.transform.localScale.y > capsule2.transform.localScale.y)
            {
                var tmpPos = capsule1.transform.localPosition;

                capsule1.transform.localPosition = capsule2.transform.localPosition;

                capsule2.transform.localPosition = tmpPos;

                capsule1.SetSiblingIndex(j + 1);
            }
        }
    }

//    private IEnumerator Radix()
//    {
//        int[] arr = _list
//            int i, j;
//            int[] tmp = new int[arr.Length];
//            for (int shift = 31; shift > -1; --shift)
//            {
//                j = 0;
//                for (i = 0; i < arr.Length; ++i)
//                {
//                    bool move = (arr[i] << shift) >= 0;
//                    if (shift == 0 ? !move : move)   
//                        arr[i-j] = arr[i];
//                    else                             
//                        tmp[j++] = arr[i];
//                }
//                Array.Copy(tmp, 0, arr, arr.Length-j, j);
//            }
//        
//    }

    private Vector3 GetPosition(int index)
    {
        return transform.position + Vector3.right * index;
    }
}