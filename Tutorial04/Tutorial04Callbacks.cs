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
    //a
    public bool A3PlayerCollectScore()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null) return false; //Likely previous tutorial dependency, simple check is ok or add explicit error
        if (go.GetComponent("PlayerCollectScore") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the <asset>PlayerCollectScore</asset> script attached.";
             return false;
        }
        return true;
    }

    //b
    public bool B2Canvas()
    {
        if (CommonTutorialCallbacks.FindGameObject("Canvas") == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Canvas</go>.";
             return false;
        }
        if (CommonTutorialCallbacks.GameObjectComponent<Canvas>("Canvas") == null)
        {
             Criterion.globalLastKnownError = "The <go>Canvas</go> GameObject does not have a 'Canvas' component.";
             return false;
        }
        return true;
    }
    public bool B3CanvasConfig1()
    {
        var canvas = CommonTutorialCallbacks.GameObjectComponent<Canvas>("Canvas");
        if (canvas == null) return false; //B2 covers this

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
             Criterion.globalLastKnownError = "The Canvas 'Render Mode' must be set to 'Screen Space - Overlay'.";
             return false;
        }
        return true;
    }
    public bool B3CanvasConfig2()
    {
        var scaler = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (scaler == null)
        {
             Criterion.globalLastKnownError = "The <go>Canvas</go> is missing the 'Canvas Scaler' component.";
             return false;
        }
        if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
             Criterion.globalLastKnownError = "The Canvas Scaler 'UI Scale Mode' must be 'Scale With Screen Size'.";
             return false;
        }
        return true;
    }
    public bool B3CanvasConfig3()
    {
        var scaler = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (scaler == null) return false;

        if (scaler.referenceResolution.x != 1280 || scaler.referenceResolution.y != 720)
        {
             Criterion.globalLastKnownError = $"Reference Resolution should be 1280 x 720, but is {scaler.referenceResolution.x} x {scaler.referenceResolution.y}.";
             return false;
        }
        return true;
    }
    public bool B3CanvasConfig4()
    {
        var scaler = CommonTutorialCallbacks.GameObjectComponent<CanvasScaler>("Canvas");
        if (scaler == null) return false;

        if (scaler.screenMatchMode != CanvasScaler.ScreenMatchMode.MatchWidthOrHeight || scaler.matchWidthOrHeight != 1f)
        {
             Criterion.globalLastKnownError = "Set 'Screen Match Mode' to 'Match Width Or Height' and 'Match' value to 1 (Height).";
             return false;
        }
        return true;
    }
    public bool B4BackgroundImage()
    {
        var background = CommonTutorialCallbacks.FindGameObject("Background");
        if (background == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Background</go>.";
             return false;
        }
        var img = background.GetComponent<Image>();
        if (img == null)
        {
             Criterion.globalLastKnownError = "The <go>Background</go> GameObject is missing the 'Image' component.";
             return false;
        }

        var sprite = img.sprite;
        if (sprite == null)
        {
             Criterion.globalLastKnownError = "The 'Image' component has no Source Image assigned.";
             return false;
        }
        if (sprite.name != "Background")
        {
             Criterion.globalLastKnownError = $"The assigned Sprite must be <asset>Background</asset>, but it is '{sprite.name}'.";
             return false;
        }
        return true;
    }
    public bool B5BackgroundFill()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Background");
        if (go == null) return false;
        var rT = go.GetComponent<RectTransform>();
        var img = go.GetComponent<Image>();
        
        if (!rT.offsetMin.Equals(Vector2.zero) || !rT.offsetMax.Equals(Vector2.zero))
        {
             Criterion.globalLastKnownError = "The <go>Background</go> RectTransform must be stretched to fill the Canvas (Left/Top/Right/Bottom all 0).";
             return false;
        }
        if (!img.preserveAspect)
        {
             Criterion.globalLastKnownError = "Check 'Preserve Aspect' on the Image component.";
             return false;
        }
        return true;
    }
    public bool B6Invaderoids()
    {
        var invaderoids = CommonTutorialCallbacks.GameObjectsStartingWith("Invaderoid");
        if (invaderoids.Count != 2)
        {
             Criterion.globalLastKnownError = $"There should be exactly 2 Invaderoid UI Images. Found {invaderoids.Count}.";
             return false;
        }
        
        foreach(var inv in invaderoids)
        {
            var img = inv.GetComponent<Image>();
            if (img == null)
            {
                Criterion.globalLastKnownError = $"GameObject <go>{inv.name}</go> is missing the 'Image' component.";
                return false;
            }
            if (img.sprite == null)
            {
                Criterion.globalLastKnownError = $"GameObject <go>{inv.name}</go> has no Sprite assigned.";
                return false;
            }
        }
        return true;
    }
    public bool B7TitleTextExists()
    {
        var title = CommonTutorialCallbacks.FindGameObject("Title");
        if (title == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Title</go>.";
             return false;
        }
        if (title.GetComponent<Text>() == null)
        {
             Criterion.globalLastKnownError = "The <go>Title</go> GameObject is missing the 'Text' component.";
             return false;
        }
        return true;
    }
    public bool B7TitleText()
    {
        var title = CommonTutorialCallbacks.GameObjectComponent<Text>("Title");
        if (title == null) return false;

        if (title.text != "Invaderoids")
        {
             Criterion.globalLastKnownError = $"The Text content should be 'Invaderoids', but it is '{title.text}'.";
             return false;
        }
        return true;
    }
    public bool B7TitleFont()
    {
        var title = CommonTutorialCallbacks.GameObjectComponent<Text>("Title");
        if (title == null) return false;

        var font = title.font;
        if (font == null)
        {
             Criterion.globalLastKnownError = "No Font assigned to the Text component.";
             return false;
        }

        if (!font.name.ToLower().StartsWith("metal"))
        {
             Criterion.globalLastKnownError = "Please assign the 'Metal' font (or similar name) to the Text.";
             return false;
        }
        return true;
    }
    public bool B8Shadow()
    {
        var title = CommonTutorialCallbacks.FindGameObject("Title");
        if (title == null) return false;
        if (title.GetComponent<Shadow>() == null)
        {
             Criterion.globalLastKnownError = "The <go>Title</go> GameObject is missing the 'Shadow' component.";
             return false;
        }
        return true;
    }
    public bool B8ShadowColor()
    {
        var shadow = CommonTutorialCallbacks.GameObjectComponent<Shadow>("Title");
        if (shadow == null) return false;

        if (shadow.effectColor.r != 1 || shadow.effectColor.g != 0 || shadow.effectColor.b != 0)
        {
             Criterion.globalLastKnownError = "The Shadow color should be Red (1, 0, 0).";
             return false;
        }
        return true;
    }
    public bool B8ShadowDistance()
    {
        var shadow = CommonTutorialCallbacks.GameObjectComponent<Shadow>("Title");
        if (shadow == null) return false;

        if (shadow.effectDistance.x != 9 || shadow.effectDistance.y != -9)
        {
             Criterion.globalLastKnownError = $"Shadow Distance should be (9, -9), but is ({shadow.effectDistance.x}, {shadow.effectDistance.y}).";
             return false;
        }
        return true;
    }

    //c
    public bool C1ButtonGroupParent()
    {
        var group = CommonTutorialCallbacks.FindGameObject("Button Group");
        if (group == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Button Group</go>.";
             return false;
        }
        var parent = group.transform.parent;
        if (parent == null || parent.name != "Canvas")
        {
             Criterion.globalLastKnownError = "The <go>Button Group</go> should be a child of the <go>Canvas</go>.";
             return false;
        }
        return true;
    }    
    public bool C1ButtonGroupVerticalLayoutGroup()
    {
        var group = CommonTutorialCallbacks.FindGameObject("Button Group");
        if (group == null) { Criterion.globalLastKnownError = "Could not find <go>Button Group</go>."; return false; }
        if (group.GetComponent<VerticalLayoutGroup>() == null)
        {
             Criterion.globalLastKnownError = "The <go>Button Group</go> needs a 'Vertical Layout Group' component.";
             return false;
        }
        return true;
    }
    public bool C1ButtonGroupChildren()
    {
        var group = CommonTutorialCallbacks.FindGameObject("Button Group");
        if (group == null) { Criterion.globalLastKnownError = "Could not find <go>Button Group</go>."; return false; }

        if (group.transform.childCount != 3)
        {
             Criterion.globalLastKnownError = $"The <go>Button Group</go> should have exactly 3 children. Found {group.transform.childCount}.";
             return false;
        }
        //Names
        var names = new string[] { "Start Game", "High Scores", "Settings" };
        for(int i=0; i<3; i++)
        {
            if (group.transform.GetChild(i).name != names[i])
            {
                 Criterion.globalLastKnownError = $"Child {i+1} should be named <go>{names[i]}</go>, but is <go>{group.transform.GetChild(i).name}</go>.";
                 return false;
            }
            var text = group.transform.GetChild(i).GetComponentInChildren<Text>();
            if (text == null)
            {
                 Criterion.globalLastKnownError = $"The button <go>{names[i]}</go> is missing a Text component in its children.";
                 return false;
            }
            if (text.text.Trim() != names[i])
            {
                 Criterion.globalLastKnownError = $"The text on button <go>{names[i]}</go> should be '{names[i]}'.";
                 return false;
            }
        }
        return true;
    }
    public bool C2ButtonImageTargets()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            var image = button.GetComponent<Image>(); //Usually on the button itself
            if (image != null && image.enabled)
            {
                 Criterion.globalLastKnownError = $"The Image component on button <go>{button.name}</go> should be disabled (uncheck the box next to 'Image').";
                 return false;
            }
        }
        return true;
    }
    public bool C2ButtonTargetGraphic()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            var text = button.GetComponentInChildren<Text>();
            if (button.targetGraphic != text)
            {
                 Criterion.globalLastKnownError = $"The 'Target Graphic' for button <go>{button.name}</go> should be assigned to its Text component (drag the Text into the field).";
                 return false;
            }
        }
        return true;
    }
    public bool C2ButtonHighlight()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        {
            if (button.colors.highlightedColor != Color.red)
            {
                 Criterion.globalLastKnownError = $"The 'Highlighted Color' for button <go>{button.name}</go> should be Red.";
                 return false;
            }
        }
        return true;
    }
    public bool C2ButtonSelected()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var button in buttons)
        { 
            if (button.colors.selectedColor != Color.red)
            {
                 Criterion.globalLastKnownError = $"The 'Selected Color' for button <go>{button.name}</go> should be Red.";
                 return false;
            }
        }
        return true;
    }
    public bool C3FirstSelected()
    {
        var eventSystem = FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
        if (eventSystem == null)
        {
             Criterion.globalLastKnownError = "Could not find an EventSystem in the scene.";
             return false;
        }

        var firstSelected = eventSystem.firstSelectedGameObject;
        if (firstSelected == null)
        {
             Criterion.globalLastKnownError = "The EventSystem 'First Selected' field is empty.";
             return false;
        }

        if (!firstSelected.name.StartsWith("Start Game"))
        {
             Criterion.globalLastKnownError = "The 'First Selected' object should be the <go>Start Game</go> button.";
             return false;
        }
        return true;
    }

    //d
    public bool D1SceneSwitcher()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Start Game");
        if (go == null) 
        {
            Criterion.globalLastKnownError = "Could not find <go>Start Game</go> button.";
            return false;
        }
        if (go.GetComponent("SceneSwitcher") == null)
        {
             Criterion.globalLastKnownError = "The <go>Start Game</go> button needs the <asset>SceneSwitcher</asset> script attached.";
             return false;
        }
        return true;
    }
    public bool D1EventTarget()
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>("Start Game");
        if (button == null) 
        {
            Criterion.globalLastKnownError = "Could not find <go>Start Game</go> button.";
            return false;
        }

        if (button.onClick.GetPersistentEventCount() == 0)
        {
             Criterion.globalLastKnownError = "The <go>Start Game</go> button has no OnClick events configured.";
             return false;
        }

        var eventListenerTarget = button.onClick.GetPersistentTarget(0);
        if (eventListenerTarget == null)
        {
             Criterion.globalLastKnownError = "The OnClick event target is missing (None).";
             return false;
        }

        if (!eventListenerTarget.name.StartsWith("Start Game"))
        {
             Criterion.globalLastKnownError = "The OnClick event target should be the <go>Start Game</go> button itself (drag the button into the field).";
             return false;
        }
        return true;
    }
    public bool D1EventFunction()
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>("Start Game");
        if (button == null) return false;
        if (button.onClick.GetPersistentEventCount() == 0) return false;

        var eventListenerFunction = button.onClick.GetPersistentMethodName(0);
        if (eventListenerFunction != "SwitchScene")
        {
             Criterion.globalLastKnownError = $"The OnClick function is set to '{eventListenerFunction}', but it should be <asset>SceneSwitcher.SwitchScene</asset>.";
             return false;
        }
        return true;
    }

    public bool D3SceneList()
    {
        if (EditorBuildSettings.scenes.Length < 2)
        {
             Criterion.globalLastKnownError = "There should be at least 2 scenes in the Build Settings (Title and Invaderoids).";
             return false;
        }
        
        bool hasTitle = EditorBuildSettings.scenes.Any(s => s.path.Contains("Title"));
        bool hasGame = EditorBuildSettings.scenes.Any(s => s.path.Contains("Invaderoids"));

        if (!hasTitle)
        {
             Criterion.globalLastKnownError = "The <asset>Title</asset> scene is missing from Build Settings.";
             return false;
        }
        if (!hasGame)
        {
             Criterion.globalLastKnownError = "The <asset>Invaderoids</asset> scene is missing from Build Settings.";
             return false;
        }

        //Check order (Title first)
        //Logic says: if scenes[0] is empty/deleted logic OR scenes[0] is Title.
        //I'll trust simplistic check: First valid scene should be Title.
        var first = EditorBuildSettings.scenes.FirstOrDefault(s => !string.IsNullOrEmpty(s.path));
        if (first != null && !first.path.Contains("Title"))
        {
             Criterion.globalLastKnownError = "The <asset>Title</asset> scene should be the first scene in Build Settings (index 0).";
             return false;
        }

        return true;
    }

    public bool D5SceneList()
    {
        bool hasSettings = EditorBuildSettings.scenes.Any(s => s.path.Contains("Settings"));
        bool hasScores = EditorBuildSettings.scenes.Any(s => s.path.Contains("High Scores"));

        if (!hasSettings) { Criterion.globalLastKnownError = "Add the 'Settings' scene to Build Settings."; return false; }
        if (!hasScores) { Criterion.globalLastKnownError = "Add the 'High Scores' scene to Build Settings."; return false; }
        return true;
    }

    public bool D5Buttons()
    {
        if (!CheckButtonEvent("High Scores", "SwitchScene")) return false;
        if (!CheckButtonEvent("Settings", "SwitchScene")) return false;
        return true;
    }

    private bool CheckButtonEvent(string buttonName, string methodName)
    {
        var button = CommonTutorialCallbacks.GameObjectComponent<Button>(buttonName);
        if (button == null)
        {
             Criterion.globalLastKnownError = $"Could not find <go>{buttonName}</go> button.";
             return false;
        }

        if (button.onClick.GetPersistentEventCount() == 0)
        {
             Criterion.globalLastKnownError = $"The <go>{buttonName}</go> button has no OnClick events.";
             return false;
        }

        var func = button.onClick.GetPersistentMethodName(0);
        if (func != methodName)
        {
             Criterion.globalLastKnownError = $"The <go>{buttonName}</go> button OnClick function should be '{methodName}', but is '{func}'.";
             return false;
        }
        return true;
    }

    //e
    public bool E1GameOver(string name)
    {
        //name param is unused?
        if (UnityEditor.SceneManagement.EditorSceneManager.sceneCount <= 1)
        {
             //This is checking if they loaded the GameOver scene ADDITIVELY?
             //Or referencing multi-scene auditing? 
             //Usually E1 says "Open game over scenes".
             //If sceneCount > 1, means multiple scenes open.
             Criterion.globalLastKnownError = "Please open the Game Over scenes additively (drag them into Hierarchy) or ensure multiple are open.";
             return false;
        }
        return true; 
    }
    public bool E3EndGameListener(string name)
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null) 
        {
             Criterion.globalLastKnownError = "Could not find <go>Player</go>.";
             return false;
        }
        if (go.GetComponent("EndGameListener") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject needs the <asset>EndGameListener</asset> script.";
             return false;
        }
        return true;
    }
    public bool E3Tag()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) 
        {
             Criterion.globalLastKnownError = "Could not find <asset>Invaderoid</asset> Prefab.";
             return false;
        }

        if (!prefab.CompareTag("Enemy"))
        {
             Criterion.globalLastKnownError = "The <asset>Invaderoid</asset> Prefab Tag should be set to 'Enemy'.";
             return false;
        }
        return true;
    }
    public bool E4SceneList()
    {
        bool hasHigh = EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_HighScore"));
        bool hasInv = EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_InvaderoidsDestroyed"));
        bool hasPly = EditorBuildSettings.scenes.Any(s => s.path.Contains("GameOver_PlayerDestroyed"));

        if (!hasHigh) { Criterion.globalLastKnownError = "Add 'GameOver_HighScore' to Build Settings."; return false; }
        if (!hasInv) { Criterion.globalLastKnownError = "Add 'GameOver_InvaderoidsDestroyed' to Build Settings."; return false; }
        if (!hasPly) { Criterion.globalLastKnownError = "Add 'GameOver_PlayerDestroyed' to Build Settings."; return false; }
        return true;
    }
}
#endif