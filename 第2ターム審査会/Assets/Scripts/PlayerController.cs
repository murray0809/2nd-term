using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シューティングゲームの自機を操作するためのコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    /// <summary>弾の発射位置</summary>
    [SerializeField] Transform m_muzzle;
    /// <summary>一画面の最大段数（非連射時）</summary>
    [SerializeField] int m_bulletLimit = 3;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;
    [SerializeField] bool m_godMode;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 自機を移動させる
        float h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する
        float v = Input.GetAxisRaw("Vertical");     // 水平方向の入力を取得する
        Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        m_rb2d.velocity = dir * m_moveSpeed;        // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする

        // 左クリックまたは左 Ctrl で弾を発射する（単発）
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.GetComponentsInChildren<BulletController>().Length < m_bulletLimit)    // 画面内の弾数を制限する
            {
                Fire();
            }
        }

        // 右クリックまたは左 Alt で弾を発射する（連射）
        if (Input.GetButton("Fire2"))
        {
            Fire();
        }
    }

    /// <summary>
    /// 弾を発射して、発射音を鳴らす
    /// </summary>
    void Fire()
    {
        if (m_bulletPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab, m_muzzle.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            m_audio.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (!m_godMode)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// やられた時に呼び出す
    /// </summary>
    void Die()
    {
        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                gameManager.PlayerDead();
            }
        }

        // 爆発エフェクトを置く
        if (m_explosionEffect)
        {
            Instantiate(m_explosionEffect, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);   // オブジェクトを破棄する
    }
}