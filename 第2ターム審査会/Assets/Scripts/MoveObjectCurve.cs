using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// まっすぐ下に移動し、プレイヤーに近づいたらその方向に曲がるスクリプト
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveObjectCurve : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float m_speed = 1f;
    /// <summary>プレイヤーよりどれくらい上で動きを変化するか</summary>
    [SerializeField] float m_playerOffsetY = 0f;
    /// <summary>カーブする時にかける力</summary>
    [SerializeField] float m_chasingPower = 1f;
    /// <summary>L字に曲がるか、徐々に曲がるか</summary>
    [SerializeField] bool m_isLShape;
    Rigidbody2D m_rb;
    Transform m_player;
    /// <summary>曲がる方向</summary>
    float m_dir = 0f;

    void Start()
    {
        // まずまっすぐ下に移動させる
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = Vector2.left * m_speed;

        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Player がいなくなった時のための処理（４週目）
        if (!m_player) return;

        // プレイヤーとある程度近づいたら
        if (this.transform.position.x - m_player.position.x < m_playerOffsetY)
        {
            // 左右どちらに曲がるか判定する
            if (m_dir == 0)
            {
                m_dir = (m_player.position.x > this.transform.position.x) ? 1 : -1;

                if (m_isLShape)
                {
                    // L 字に曲がる
                    m_rb.velocity = m_dir * Vector2.up * m_speed;
                }
            }

            if(!m_isLShape)
            {
                // カーブする
                m_rb.AddForce(m_dir * Vector2.up * m_chasingPower);
            }
        }
    }
}