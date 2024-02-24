using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Score", score);
        textMeshPro.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
