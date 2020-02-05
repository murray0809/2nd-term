using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    private readonly object m_score;
    [SerializeField] UnityEngine.UI.Text m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_scoreText.text = "Score: " + m_score;
    }
}
