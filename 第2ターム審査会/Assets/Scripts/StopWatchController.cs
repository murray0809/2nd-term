using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ストップウォッチを構成するコンポーネント
/// </summary>
public class StopWatchController : MonoBehaviour
{
    /// <summary>表示テキスト</summary>
    [SerializeField] Text m_console;
    /// <summary>動作中フラグ</summary>
    bool m_isWorking;
    /// <summary>タイマー</summary>
    float m_timer;

    void Update()
    {
        // 動作中ならタイマーを加算して表示を更新する
        if (m_isWorking)
        {
            m_timer -= Time.deltaTime;
            Refresh();
        }
    }

    /// <summary>
    /// スタート・停止を切り替える
    /// </summary>
    public void SwitchWorking()
    {
        m_isWorking = true;
    }

    /// <summary>
    /// ストップウォッチをリセットする
    /// </summary>
    public void Reset()
    {
        m_timer = 0;
        Refresh();
    }

    /// <summary>
    /// タイマー表示を更新する
    /// </summary>
    void Refresh()
    {
        m_console.text = m_timer.ToString("F2");    // 小数点以下２桁を表示する 参考: https://dobon.net/vb/dotnet/string/inttostring.html
    }
}
