using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleReturnScript : MonoBehaviour
{
    [SerializeField] string m_titleSceneName = "Title";
    [SerializeField] Button m_restartButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_restartButton)
        {
            m_restartButton.gameObject.SetActive(true);
        }
        Initiate.Fade(m_titleSceneName, Color.black, 0.5f);
    }
}
