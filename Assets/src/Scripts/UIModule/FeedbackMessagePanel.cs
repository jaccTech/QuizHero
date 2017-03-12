using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FeedbackMessagePanel : MonoBehaviour
{
    public enum StyleType { ERROR, SUCCESS };

    [Serializable]
    public class Style
    {
        public StyleType styleType;
        public Color textColor;
        public Color bgColor;
    }

    public Text message;
    public Image background;

    public List<Style> styles = new List<Style>();

    public void ShowMessage (string msg, StyleType styleType)
    {
        Style style = GetStyleByType(styleType);

        background.color = style.bgColor;
        message.color = style.textColor;
        message.text = msg;

        message.enabled = true;
        background.enabled = true;
    }

    public void HideMessage ()
    {
        message.enabled = false;
        background.enabled = false;
    }

    private Style GetStyleByType (StyleType styleType)
    {
        for (int i = 0; i < styles.Count; i++)
        {
            if (styles[i].styleType == styleType)
                return styles[i];
        }

        // Error
        Debug.LogError("Style not found " + styleType);

        return null;
    }
}
