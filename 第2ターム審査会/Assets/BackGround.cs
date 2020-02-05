using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField]
    float speed = 4;
    [SerializeField]
    int spriteCount = 2; //背景オブジェクトの横の数

    float width;

    //初期化
    private void Start()
    {
        // スプライトの幅を取得
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    //毎フレームの処理
    void Update()
    {
        // 左へ移動
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    //カメラ外に出たときの処理
    void OnBecameInvisible()
    {
        // 幅ｘ個数分だけ右へ移動
        transform.position += Vector3.right * width * spriteCount;
    }
}