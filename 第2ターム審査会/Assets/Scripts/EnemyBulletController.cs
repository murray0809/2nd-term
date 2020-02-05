using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の弾を発射するためのコンポーネント
/// Player タグが付いているオブジェクトに向かってまっすぐ飛ぶ
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶスピード</summary>
    [SerializeField] float m_speed = 1f;
    Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        // プレイヤーに向かっていく方向のベクトルを計算する
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)    // プレイヤーがいない時は弾を出さない（４週目で追加）
        {
            Destroy(this.gameObject);
            return;
        }
        Vector2 dir = player.transform.position - this.transform.position;
        dir = dir.normalized;

        // プレイヤーに向かって飛ばす
        m_rb.velocity = dir * m_speed;
    }
}
