using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private MessageBox _Instance;
    public MessageBox Instance
    {
        get 
        {
            if (_Instance == null)
            {
                _Instance = (MessageBox)FindAnyObjectByType(typeof(MessageBox));
            }
            return _Instance; }
    }

    public ScrollRect errMessageBox;
    public Font messageFont;

    static Text[] errMessages = new Text[27];

    public void MessageAligner()
    {

        for (int i = 0; i < 27; i++)
        {
            GameObject errorMessage = new GameObject("errMessageSpace_"+i);
            errorMessage.transform.SetParent(errMessageBox.content);
            errorMessage.AddComponent<Text>();
            Text newErrMessage = errorMessage.GetComponent<Text>();
            newErrMessage.alignment = TextAnchor.MiddleLeft;
            newErrMessage.font = messageFont;
            newErrMessage.fontSize = 14;  // 调整字体大小
            newErrMessage.color = Color.black;  // 设置文本颜色
            newErrMessage.rectTransform.localPosition = new Vector3(0, -520 + (-40 * (i + 1)), 0);
            newErrMessage.rectTransform.sizeDelta = new Vector2(1400, 40);  // 设置大小
            newErrMessage.rectTransform.anchorMin = new Vector2(0.5f, 1);
            newErrMessage.rectTransform.anchorMax = new Vector2(0.5f, 1);
            newErrMessage.rectTransform.pivot = new Vector2(0.5f, 0.5f);

            errMessages[i]= newErrMessage;

        }
    }
    public static void MessageUpdate()
    {
        for (int i = 25; i >= 0; i--)
        {
            errMessages[i + 1].text = errMessages[i].text;
        }
    }
    public static void Log(string Message)
    {
        MessageUpdate();
        errMessages[0].text = Message;
    }

    // Start is called before the first frame update
    void Start()
    {
        MessageAligner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
