using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残機表示を行うコンポーネント
/// 管理オブジェクトにアタッチして使う
/// </summary>
public class PlayerCounter : MonoBehaviour
{
    /// <summary>残機として表示するオブジェクト</summary>
    [SerializeField] GameObject m_playerUiPrefab;
    /// <summary>残機表示をするパネル</summary>
    [SerializeField] RectTransform m_playerCounterPanel;

    /// <summary>
    /// 残機表示を更新する
    /// </summary>
    /// <param name="playerCount">残機数</param>
    public void Refresh(int playerCount)
    {
        // 子オブジェクトをすべて削除する
        foreach (Transform t in m_playerCounterPanel.transform)
        {
            Destroy(t.gameObject);
        }

        // 残機数だけプレハブをパネルの子オブジェクトとして生成する
        for (int i = 0; i < playerCount - 1; i++)
        {
            GameObject go = Instantiate(m_playerUiPrefab);
            go.transform.SetParent(m_playerCounterPanel.transform);
        }
    }
}
