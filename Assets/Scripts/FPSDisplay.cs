using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    TextMeshProUGUI fpstext;

    float deltaTime = 0.0f;
 
    private void Awake()
    {
        fpstext = GetComponent<TextMeshProUGUI>();
    }

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpstext.text = text;
    }
 
}