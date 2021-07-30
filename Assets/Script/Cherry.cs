using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    void Death()
    {
        FindObjectOfType<PlayerController>().CherryCount();
        Destroy(gameObject);
    }
}
