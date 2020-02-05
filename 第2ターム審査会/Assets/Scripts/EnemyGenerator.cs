using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するコンポーネント
/// 適当なオブジェクトに追加して使う
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>敵として生成するプレハブの配列</summary>
    [SerializeField] GameObject[] m_enemyPrefabs;
    /// <summary>敵のプレハブの配列に使うインデックス</summary>
    int m_index;
    /// <summary>オンにしておくと、実行と同時に生成を始める</summary>
    [SerializeField] bool m_playOnStart = true;
    /// <summary>動作中フラグ</summary>
    bool m_isWorking;

    void Start()
    {
        // フラグがオンなら生成を始める
        if (m_playOnStart)
        {
            StartGeneration();
        }
    }

    void Update()
    {
        // 動作中でない時は何もしない
        if (!m_isWorking) return;

        // 敵が一体もいない時は
        if (GameObject.FindObjectsOfType<EnemyController>().Length < 1)
        {
            DestroyChildren();  // 生成したオブジェクトをクリアする
            GameObject go = Instantiate(m_enemyPrefabs[m_index], gameObject.transform.position, m_enemyPrefabs[m_index].transform.rotation);    // 配列からプレハブを一つインスタンス化する
            go.transform.SetParent(this.transform); // インスタンス化したオブジェクトを自分の子供にする
            m_index = (m_index + 1) % m_enemyPrefabs.Length;    // インデックスを一つ進める
        }
    }

    /// <summary>
    /// 敵の生成を開始する
    /// </summary>
    public void StartGeneration()
    {
        // フラグを立てる
        m_isWorking = true;
    }

    /// <summary>
    /// 子オブジェクトを全て破棄する
    /// </summary>
    void DestroyChildren()
    {
        Transform[] txes = transform.GetComponentsInChildren<Transform>();
        foreach(var tx in txes)
        {
            if (tx != this.transform)
            {
                Destroy(tx.gameObject);
            }
        }
    }

    /// <summary>
    /// 敵の生成を停止する
    /// </summary>
    public void StopGeneration()
    {
        // フラグを倒す
        m_isWorking = false;
    }
}
