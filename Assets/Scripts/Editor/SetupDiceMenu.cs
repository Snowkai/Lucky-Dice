using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class SetupDiceMenu : EditorWindow
{
    static string kitPath = "Assets/MenuUI_kit/";
    static string fontPath = "Assets/Resourse/Font/Cinzel/static/";

    [MenuItem("Tools/Setup Dice Menu")]
    public static void Run()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null) { Debug.LogError("Canvas not found"); return; }

        Sprite woodBg = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Panels/wood_background.png");
        Sprite parchment = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Panels/parchment_panel.png");
        Sprite numberField = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Controls/number_field.png");
        Sprite btnNormal = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Controls/round_button_normal.png");
        Sprite btnHover = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Controls/round_button_hover.png");
        Sprite btnPressed = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Controls/round_button_pressed.png");
        Sprite btnDisabled = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Controls/round_button_disabled.png");
        Sprite iconPlus = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Icons/icon_plus.png");
        Sprite iconMinus = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Icons/icon_minus.png");
        Sprite iconCheck = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Icons/icon_check.png");
        Sprite dividerLong = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Decor/divider_line_long.png");
        Sprite dividerShort = AssetDatabase.LoadAssetAtPath<Sprite>(kitPath + "Decor/divider_line_short.png");

        Font cinzelBold = AssetDatabase.LoadAssetAtPath<Font>(fontPath + "Cinzel-Bold.ttf");
        Font cinzelSemiBold = AssetDatabase.LoadAssetAtPath<Font>(fontPath + "Cinzel-SemiBold.ttf");
        Font cinzelRegular = AssetDatabase.LoadAssetAtPath<Font>(fontPath + "Cinzel-Regular.ttf");

        Debug.Log("[Setup] Loaded. woodBg=" + (woodBg != null) + " parchment=" + (parchment != null) +
            " btnNormal=" + (btnNormal != null) + " bold=" + (cinzelBold != null));

        Color textColor = HexColor("#2B211B");
        Color iconColor = HexColor("#FAEFD5");

        SetImage(canvas.transform.Find("Setting_bg"), woodBg, Image.Type.Simple, Color.white);
        SetImage(canvas.transform.Find("Setting_bg/Menu"), parchment, Image.Type.Sliced, Color.white);

        Transform titleT = canvas.transform.Find("Setting_bg/Menu/Title_DICE");
        if (titleT != null) SetText(titleT, "DICE", cinzelBold, 64, textColor, TextAnchor.MiddleCenter);

        string[] tags = { "D4", "D6", "D8", "D10", "D12", "D20" };
        foreach (string tag in tags)
        {
            Transform row = canvas.transform.Find("Setting_bg/Menu/Row_" + tag);
            if (row == null) { Debug.LogWarning("Row not found: " + tag); continue; }

            Transform labelT = row.Find("Label");
            if (labelT != null) SetText(labelT, tag, cinzelBold, 28, textColor, TextAnchor.MiddleLeft);

            SetupButton(row.Find("BtnMinus"), btnNormal, btnHover, btnPressed, btnDisabled, iconMinus, iconColor);
            SetupButton(row.Find("BtnPlus"), btnNormal, btnHover, btnPressed, btnDisabled, iconPlus, iconColor);

            Transform countT = row.Find("CountField");
            if (countT != null)
            {
                SetImage(countT, numberField, Image.Type.Sliced, Color.white);
                Transform countText = countT.Find("Text");
                if (countText != null) SetText(countText, "0", cinzelRegular, 24, textColor, TextAnchor.MiddleCenter);
            }
        }

        for (int i = 0; i < tags.Length - 1; i++)
        {
            Transform divT = canvas.transform.Find("Setting_bg/Menu/Divider_" + tags[i]);
            if (divT != null)
            {
                var divImg = divT.GetComponent<Image>();
                if (divImg != null)
                {
                    divImg.sprite = (i % 2 == 0) ? dividerLong : dividerShort;
                    divImg.type = Image.Type.Simple;
                    divImg.color = new Color(0.72f, 0.53f, 0.18f, 0.5f);
                }
            }
        }

        Transform scoreRow = canvas.transform.Find("Setting_bg/Menu/ScoreRow");
        if (scoreRow != null)
        {
            Transform slT = scoreRow.Find("ScoreLabel");
            if (slT != null) SetText(slT, "Display score", cinzelSemiBold, 24, textColor, TextAnchor.MiddleLeft);
            SetupButton(scoreRow.Find("BtnCheck"), btnNormal, btnHover, btnPressed, btnDisabled, iconCheck, iconColor);
        }

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(canvas);
        Debug.Log("[Setup] Dice menu setup complete!");
    }

    static void SetupButton(Transform btnT, Sprite normal, Sprite hover, Sprite pressed, Sprite disabled, Sprite icon, Color iconColor)
    {
        if (btnT == null) return;

        var img = btnT.GetComponent<Image>();
        if (img != null && normal != null)
        {
            img.sprite = normal;
            img.type = Image.Type.Simple;
            img.color = Color.white;
        }

        var btn = btnT.GetComponent<Button>();
        if (btn != null)
        {
            btn.transition = Selectable.Transition.SpriteSwap;
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = hover;
            ss.pressedSprite = pressed;
            ss.selectedSprite = hover;
            ss.disabledSprite = disabled;
            btn.spriteState = ss;
        }

        foreach (Transform child in btnT)
        {
            var childImg = child.GetComponent<Image>();
            if (childImg != null && icon != null)
            {
                childImg.sprite = icon;
                childImg.color = iconColor;
                break;
            }
        }
    }

    static void SetImage(Transform t, Sprite sprite, Image.Type type, Color color)
    {
        if (t == null) return;
        var img = t.GetComponent<Image>();
        if (img != null && sprite != null)
        {
            img.sprite = sprite;
            img.type = type;
            img.color = color;
        }
    }

    static void SetText(Transform t, string text, Font font, int size, Color color, TextAnchor anchor)
    {
        if (t == null) return;
        var txt = t.GetComponent<Text>();
        if (txt != null)
        {
            txt.text = text;
            if (font != null) txt.font = font;
            txt.fontSize = size;
            txt.color = color;
            txt.alignment = anchor;
        }
    }

    static Color HexColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }
}
