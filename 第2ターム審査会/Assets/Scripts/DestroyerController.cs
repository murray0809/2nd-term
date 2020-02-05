using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面外に出たオブジェクトを破棄するためのコンポーネント
/// 画面の範囲を設定したトリガーを設定したオブジェクトに追加して使う
/// </summary>
[RequireComponent(typeof(Collision2D))]
public class DestroyerController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
