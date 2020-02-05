using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ObjectDestroyer 以外のオブジェクトが接触したら破棄する
        if (!collision.gameObject.GetComponent<ObjectDestroyer>())
        {
            Destroy(collision.gameObject);
        }
    }
}
