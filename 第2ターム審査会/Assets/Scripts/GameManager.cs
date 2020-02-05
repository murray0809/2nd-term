using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体を管理するクラス。
/// EnemyGenerator と同じ GameObject にアタッチする必要がある。
/// </summary>
[RequireComponent(typeof(EnemyGenerator), typeof(PlayerCounter))]
public class GameManager : MonoBehaviour
{
    /// <summary>残機数</summary>
    [SerializeField] int m_life = 3;
    /// <summary>得点</summary>
    int m_score;
    /// <summary>自機のプレハブを指定する</summary>
    [SerializeField] GameObject m_playerPrefab;
    /// <summary>ゲームの初期化が終わってからゲームが始まるまでの待ち時間</summary>
    [SerializeField] float m_waitTimeUntilGameStarts = 5f;
    /// <summary>自機がやられてからゲームの再初期化をするまでの待ち時間</summary>
    [SerializeField] float m_waitTimeAfterPlayerDeath = 5f;
    /// <summary>ゲームオーバー後に遷移するシーン（タイトル画面）のシーン名</summary>
    [SerializeField] string m_titleSceneName = "Result";
    /// <summary>EnemyGenerator を保持しておく変数</summary>
    EnemyGenerator m_enemyGenerator;
    /// <summary>残機表示をする PlayerCounter を保持しておく変数</summary>
    PlayerCounter m_playerCounter;
    /// <summary>スコア表示用 Text</summary>
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.Text m_messageText;

    /// <summary>タイマー</summary>
    float m_timer;
    /// <summary>ゲームの状態</summary>
    int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた

    void Start()
    {
        // EnemyGenerator を取得しておき、まずは敵の生成をしない。
        m_enemyGenerator = GetComponent<EnemyGenerator>();
        m_enemyGenerator.StopGeneration();

        m_playerCounter = GetComponent<PlayerCounter>();
        AddScore(0);
    }

    void Update()
    {
        if (m_status == 0)  // 初期化前
        {
            Debug.Log("Initialize.");
            Instantiate(m_playerPrefab);    // プレイヤーを生成する
            m_status = 1;   // ステータスを初期化済みにする
            m_playerCounter.Refresh(m_life);    // 残機表示を更新する
        }
        else if (m_status == 1) // 初期化済み、開始前
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_waitTimeUntilGameStarts)    // 待つ
            {
                Debug.Log("Game Start.");
                m_timer = 0f;   // タイマーをリセットする
                m_status = 2;   // ステータスをゲーム中にする
                m_enemyGenerator.StartGeneration(); // 敵の生成を開始する
            }
        }
        else if (m_status == 3) // プレイヤーがやられた
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_waitTimeAfterPlayerDeath)   // 待つ
            {
                if (m_life > 0) // 残機がまだある場合
                {
                    Debug.Log("Restart Game.");
                    m_timer = 0f;   // タイマーをリセットする
                    m_status = 0;   // 初期化するためにステータスを更新する
                    ClearScene();
                }
                else
                {
                    GameOver(); // 残機がもうない場合はゲームオーバーにする
                }
            }
        }
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        m_score += score;
        m_scoreText.text = "Score: " + m_score.ToString("d10");
    }

    /// <summary>
    /// プレイヤーがやられた時、外部から呼ばれる関数
    /// </summary>
    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_enemyGenerator.StopGeneration();  // 敵の生成を止める
        m_life -= 1;    // 残機を減らす
        m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
    }

    /// <summary>
    /// シーン上にある敵と敵の弾を消す
    /// </summary>
    void ClearScene()
    {
        // 敵を消す
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var go in goArray)
        {
            Destroy(go);
        }

        // 敵の弾を消す
        goArray = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (var go in goArray)
        {
            Destroy(go);
        }
    }

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    public void GameOver()
    {
        Debug.Log("Game over. Return to title scene.");
        if (m_messageText)
        {
            m_messageText.text = "Finish";
        }

        Initiate.Fade(m_titleSceneName, Color.black, 0.5f); // タイトル画面に戻る
    }

}
