using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MonoBehaviour
{
    /// <summary>何秒後にエフェクトを消すか指定する</summary>
    [SerializeField] float m_lifeTime = 1f;

    void Start()
    {
        Destroy(this.gameObject, m_lifeTime);   // m_lifeTime 秒後にオブジェクトを破棄する
    }
}
