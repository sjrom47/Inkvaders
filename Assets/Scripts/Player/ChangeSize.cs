using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = !transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled;
            transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = !transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled;
        }
    }
}
