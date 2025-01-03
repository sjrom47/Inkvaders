using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverPaint : MonoBehaviour
{
    // Start is called before the first frame update
    PaintManager manager;
    void Start()
    {
        manager = PaintManager.Instance();
        StartCoroutine(ShowColorPaintBelow());
    }

    IEnumerator ShowColorPaintBelow()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(transform.position);
            Debug.Log(manager.GetColorOfFloor(transform.position));
            yield return new WaitForSeconds(3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
