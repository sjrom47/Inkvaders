using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GeneralResetSquidTransformation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GeneralFlipSquidTransormation();
        }
    }

    // Reset the logic of the Player state
    void GeneralResetSquidTransformation()
    {
        ResetSizeSquidTrasformation();
    }

    // Flip the logic between Player and Squid state
    void GeneralFlipSquidTransormation()
    {
        FlipSizeSquidTrasformation();
    }

    // Reset the size to the one of the Player state
    void ResetSizeSquidTrasformation()
    {
        transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = false;
    }

    // Flip the size between Player and Squid state
    void FlipSizeSquidTrasformation()
    {
        transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = !transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled;
        transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = !transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled;
    }
}
