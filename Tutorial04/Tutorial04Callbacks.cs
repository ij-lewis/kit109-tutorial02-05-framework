#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Implement your Tutorial callbacks here.
/// </summary>
[CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
public class Tutorial04Callbacks : ScriptableObject
{
    public const string DefaultFileName = "Tutorial04Callbacks";

    public static ScriptableObject CreateInstance()
    {
        return ScriptableObjectUtils.CreateAsset<Tutorial02Callbacks>(DefaultFileName);
    }

    //a
    public bool A3PlayerCollectScore()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("PlayerCollectScore") != null;
    }

    //b
    public bool B2Canvas()
    {
        return CommonTutorialCallbacks.GameObjectComponent<Canvas>("Canvas") != null;
    }
    public bool B3CanvasConfig1()
    {
        var canvas = CommonTutorialCallbacks.GameObjectComponent<Canvas>("Canvas");
        if (canvas == null) return false;

        return canvas.renderMode == RenderMode.ScreenSpaceOverlay;
    }
    public bool B3CanvasConfig2()
    {
        var canvas = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (canvas == null) return false;

        return canvas.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }
    public bool B3CanvasConfig3()
    {
        var canvas = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (canvas == null) return false;

        return canvas.referenceResolution.x == 1280 && canvas.referenceResolution.y == 720;
    }
    public bool B3CanvasConfig4()
    {
        var canvas = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (canvas == null) return false;

        return canvas.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight && canvas.matchWidthOrHeight == 1f;
    }
    public bool B4BackgroundImage()
    {
        var background = CommonTutorialCallbacks.GameObjectComponent<Image>("Background");
        if (background == null) return false;

        var sprite = background.sprite;
        if (sprite == null) return false;

        return sprite.name == "Background";
    }
    public bool B5BackgroundFill()
    {
        var background = CommonTutorialCallbacks.GameObjectComponent<RectTransform>("Background");
        if (background == null) return false;

        var img = CommonTutorialCallbacks.GameObjectComponent<Image>("Background");
        if (img == null) return false;

        //Debug.Log(background.offsetMin + " " + background.offsetMax + " " + background.anchorMin + " " + background.anchorMax);

        return
            background.offsetMin.Equals(Vector2.zero) && background.offsetMax.Equals(Vector2.zero) &&
            img.preserveAspect;
    }
    public bool B6Invaderoids()
    {
        var invaderoids = CommonTutorialCallbacks.GameObjectsStartingWith("Invaderoid");
        if (invaderoids.Count == 2)
        {
            return invaderoids[0].GetComponent<Image>() && invaderoids[1].GetComponent<Image>() &&
                invaderoids[0].GetComponent<Image>().sprite != null && invaderoids[1].GetComponent<Image>().sprite != null;
        }
        return false;
    }
    public bool B7TitleTextExists()
    {
        var title = CommonTutorialCallbacks.GameObjectComponent<Text>("Title");
        if (title == null) return false;

        return true;
    }
    public bool B7TitleText()
    {
        var title = CommonTutorialCallbacks.GameObjectComponent<Text>("Title");
        if (title == null) return false;

        return title.text.Equals("Invaderoids");
    }
    public bool B7TitleFont()
    {
        var title = CommonTutorialCallbacks.GameObjectComponent<Text>("Title");
        if (title == null) return false;

        var font = title.font;
        if (font == null) return false;

        return font.name.StartsWith("metal");
    }

    public bool B8Shadow()
    {
        return CommonTutorialCallbacks.GameObjectComponent<Shadow>("Title");
    }
    public bool B8ShadowColor()
    {
        var shadow = CommonTutorialCallbacks.GameObjectComponent<Shadow>("Title");
        if (shadow == null) return false;

        return shadow.effectColor.r.Equals(1) && shadow.effectColor.g.Equals(0) && shadow.effectColor.b.Equals(0);
    }
    public bool B8ShadowDistance()
    {
        var shadow = CommonTutorialCallbacks.GameObjectComponent<Shadow>("Title");
        if (shadow == null) return false;

        return shadow.effectDistance.x.Equals(9) && shadow.effectDistance.y.Equals(-9);
    }

    //c
    public bool C1ButtonGroupParent()
    {
        var group = GameObject.Find("Button Group");
        if (group == null) return false;

        var parent = group.transform.parent;
        if (parent == null) return false;

        return parent.name.Equals("Canvas");
    }    
    public bool C1ButtonGroupVerticalLayoutGroup()
    {
        return CommonTutorialCallbacks.GameObjectComponent<VerticalLayoutGroup>("Button Group");
    }
    public bool C1ButtonGroupChildren()
    {
        var group = GameObject.Find("Button Group");
        if (group == null) return false;

        return group.transform.childCount == 3 &&
            group.transform.GetChild(0).name.Equals("Start Game") &&
            group.transform.GetChild(1).name.Equals("High Scores") &&
            group.transform.GetChild(2).name.Equals("Settings") &&
            group.transform.GetChild(0).GetComponentInChildren<Text>() != null &&
                group.transform.GetChild(0).GetComponentInChildren<Text>().text.Trim().Equals("Start Game") &&
            group.transform.GetChild(1).GetComponentInChildren<Text>() != null &&
                group.transform.GetChild(1).GetComponentInChildren<Text>().text.Trim().Equals("High Scores") &&
            group.transform.GetChild(2).GetComponentInChildren<Text>() != null &&
                group.transform.GetChild(2).GetComponentInChildren<Text>().text.Trim().Equals("Settings");
    }
    public bool C2ButtonImageTargets()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            var image = button.GetComponentInChildren<Image>();
            if (image != null && image.enabled) return false;
        }
        return true;
    }
    public bool C2ButtonTargetGraphic()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            if (button.targetGraphic != button.GetComponentInChildren<Text>()) return false;
        }
        return true;
    }
    public bool C2ButtonHighlight()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            if (button.colors.highlightedColor.Equals(Color.red) == false) return false;
        }
        return true;
    }
    public bool C2ButtonSelected()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        { 

            if (button.colors.selectedColor.Equals(Color.red) == false) return false;
        }
        return true;
    }
    public bool C3FirstSelected()
    {
        var eventSystem = FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
        if (eventSystem == null) return false;

        var firstSelected = eventSystem.firstSelectedGameObject;
        if (firstSelected == null) return false;

        return firstSelected.name.StartsWith("Start Game");
    }

    //d
    public bool D1SceneSwitcher()
    {
        var go = GameObject.Find("Start Game");
        return go != null && go.GetComponent("SceneSwitcher") != null;
    }
    public bool D1EventTarget()
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>("Start Game");
        if (button == null) return false;

        if (button.onClick.GetPersistentEventCount() == 0) return false;

        var eventListenerTarget = button.onClick.GetPersistentTarget(0);
        if (eventListenerTarget == null) return false;

        return eventListenerTarget.name.StartsWith("Start Game");
    }
    public bool D1EventFunction()
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>("Start Game");
        if (button == null) return false;

        if (button.onClick.GetPersistentEventCount() == 0) return false;

        // This check relies on string comparison of the method name, so it's safe without the script type
        var eventListenerFunction = button.onClick.GetPersistentMethodName(0);
        if (eventListenerFunction == null) return false;

        return eventListenerFunction == "SwitchScene";
    }

    public bool D3SceneList()
    {
        return
            EditorBuildSettings.scenes.Length >= 2 &&
            EditorBuildSettings.scenes.Any(s => s.path.Contains("Title")) &&
            EditorBuildSettings.scenes.Any(s => s.path.Contains("Invaderoids")) &&
            string.IsNullOrEmpty(EditorBuildSettings.scenes[0].path) ?  //this handles the old version of base project with a deleted sample scene as scene 0
                EditorBuildSettings.scenes[1].path.Contains("Title") :
                EditorBuildSettings.scenes[0].path.Contains("Title");

    }

    public bool D5SceneList()
    {
        return
            EditorBuildSettings.scenes.Any(s => s.path.Contains("Settings")) &&
            EditorBuildSettings.scenes.Any(s => s.path.Contains("High Scores"));
    }

    public bool D5Buttons()
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>("High Scores");
        if (button == null) return false;

        if (button.onClick.GetPersistentEventCount() == 0) return false;

        var eventListenerFunction = button.onClick.GetPersistentMethodName(0);
        if (eventListenerFunction == null) return false;

        var button2 = CommonTutorialCallbacks.GameObjectComponent<Button>("Settings");
        if (button2 == null) return false;

        if (button2.onClick.GetPersistentEventCount() == 0) return false;

        var eventListenerFunction2 = button.onClick.GetPersistentMethodName(0);
        if (eventListenerFunction2 == null) return false;

        return eventListenerFunction == "SwitchScene" && eventListenerFunction2 == "SwitchScene";
    }

    //e
    public bool E1GameOver(string name)
    {
        return UnityEditor.SceneManagement.EditorSceneManager.sceneCount > 1; //this was lazy, meh
    }
    public bool E3EndGameListener(string name)
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("EndGameListener") != null;
    }
    public bool E3Tag()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;

        return prefab.CompareTag("Enemy");
    }
    public bool E4SceneList()
    {
        return
            EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_HighScore")) &&
            EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_InvaderoidsDestroyed")) &&
            EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_PlayerDestroyed"));
    }
}
#endif