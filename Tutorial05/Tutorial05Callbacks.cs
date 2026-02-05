#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.Animations;

/// <summary>
/// Implement your Tutorial callbacks here.
/// </summary>
[CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
public class Tutorial05Callbacks : ScriptableObject
{
    public const string DefaultFileName = "Tutorial05Callbacks";

    public static ScriptableObject CreateInstance()
    {
        return ScriptableObjectUtils.CreateAsset<Tutorial02Callbacks>(DefaultFileName);
    }

    //b
    public bool B3SpriteMode()
    {
        var explosion = (TextureImporter) AssetImporter.GetAtPath("Assets/ExplosionSpritesheet.png");
        if (explosion == null) return false;

        return explosion.spriteImportMode == SpriteImportMode.Multiple;
    }
    public bool B4SpriteSlice()
    {
        var explosion = (TextureImporter)AssetImporter.GetAtPath("Assets/ExplosionSpritesheet.png");
        if (explosion == null) return false;
#pragma warning disable 0618
        return explosion.spritesheet.Count() == 4;
#pragma warning restore 0618
    }

    //c
    public bool C1AnimationCreated()
    {
        return AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim");
    }
    public bool C2LoopTime()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        AnimationClipSettings aSettings = AnimationUtility.GetAnimationClipSettings(a);
        return aSettings.loopTime == false;
    }
    public bool C8Recording()
    {
        //https://forum.unity.com/threads/knowing-when-animation-window-is-in-record-mode-in-script.547801/
        System.Reflection.Assembly editorAssembly = typeof(Editor).Assembly;

        System.Type windowType = editorAssembly.GetType
            ("UnityEditorInternal.AnimationWindowState");

        // Get a reference to the unbound property we want to access.
        System.Reflection.PropertyInfo isRecordingProp = windowType.GetProperty
            ("recording", System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public);

        // Get all instances of objects of type "AnimationWindowState"
        // (There should be exactly one of these, unless the window is closed)
        Object[] windowInstances = Resources.FindObjectsOfTypeAll(windowType);

        for (int i = 0; i < windowInstances.Length; i++)
        {
            bool isRecording = (bool)isRecordingProp.GetValue
                (windowInstances[i], null);

            //Debug.Log ("isRec: " + isRecording);

            if (isRecording) return true;
        }

        // No instances found, so we'll presume the window is closed.
        return false;
        
    }
    public bool C95thFrameIsNone()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var bindings = AnimationUtility.GetObjectReferenceCurveBindings(a);
        if (bindings.Count() != 1) return false;

        var binding = bindings[0];
        var curve = AnimationUtility.GetObjectReferenceCurve(a, binding);
        if (curve.Count() != 5) return false;

        var last = curve[4];
        return last.value == null;
    }

    //d
    public bool D1NoExplosionGO()
    {
        return CommonTutorialCallbacks.GameObjectWithNameExists("ExplosionSpritesheet_0") == false;
    }
    public bool D1InvaderoidController()
    {
        return AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
    }
    public bool D2InvaderoidAnimator()
    {
        return CommonTutorialCallbacks.PrefabComponent<Animator>("Invaderoid") != null;
    }
    public bool D2ControllerLink()
    {
        var a = CommonTutorialCallbacks.PrefabComponent<Animator>("Invaderoid");
        if (a == null) return false;

        var ac = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (ac == null) return false;

        return a.runtimeAnimatorController == ac;
    }
    public bool D4MovingState2()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        return ac.layers.First().stateMachine.states.Any(s => s.state.name.Equals("Moving"));
    }
    public bool D5Transitions()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        return moving.state.transitions.Count() == 1;
    }
    public bool D6DeadState()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        //Debug.Log(ac.layers.First().stateMachine.states.Count());
        return ac.layers.First().stateMachine.states.Any(s => s.state.name.Equals("Dead"));
    }
    public bool D6Transitions()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("ExplosionAnimation"));
        if (moving.state == null) return false;

        return moving.state.transitions.Count() == 1;
    }
    public bool D7Default()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        return ac.layers.First().stateMachine.defaultState == moving.state;
    }

    //e
    public bool E1Parameter()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        return ac.parameters.Any(p => p.name.Equals("Killed"));
    }
    public bool E2Parameter()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        return moving.state.transitions[0].conditions.Any(s => s.parameter.Equals("Killed"));
    }
    public bool E5NoExplosionOnDestroy()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        return prefab == null || prefab.GetComponent("ExplosionOnDestroy") == null;
    }
    public bool E5SendAnimTrigger()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;
        var send = prefab.GetComponent("SendAnimTriggerOnCollision");
        if (send == null) return false;
        
        var so = new SerializedObject(send);
        var triggerName = so.FindProperty("triggerName");
        var nameSubstring = so.FindProperty("nameSubstring");

        return send != null && triggerName != null && triggerName.stringValue.Equals("Killed") && nameSubstring != null && nameSubstring.stringValue.Equals("Bullet");
    }
    public bool E5NoDestroySelfKillOtherOnBullet()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        return prefab == null || prefab.GetComponent("DestroySelfAndOtherOnCollision") == null;
    }
    public bool E5DestroyOnCollisionOnBullet()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        return prefab != null && prefab.GetComponent("DestroyOnCollision") != null;
    }
    public bool E5Transition()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("ExplosionAnimation"));
        if (moving.state == null) return false;

        return moving.state.transitions.Any(t => t.hasExitTime == false);
    }
    public bool F1AnimationEventDestroyer()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        return prefab != null && prefab.GetComponent("AnimationEventObjectDestroyer") != null;
    }
    public bool F1AnimationEventAdded()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var events = AnimationUtility.GetAnimationEvents(a);
        if (events.Count() != 1) return false;

        var e = events[0];
        return e.functionName.Equals("DestroyGameObject");

    }
    public bool F3NoExitTime()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        return moving.state.transitions.Any(t => t.hasExitTime == false);
    }
    public bool F4PolygonColliderCurve()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var bindings = AnimationUtility.GetCurveBindings(a);
        // Using Type.GetType with assembly qualified name if needed, or simple string check on type name if possible, 
        // but PolygonCollider2D is Unity API, so it is safe.
        // However problem is generic check if previous was generic... wait F4PolygonColliderCurve uses typeof(PolygonCollider2D) which is fine.
        return bindings.Any(b => b.type == typeof(PolygonCollider2D) && b.propertyName.Equals("m_Enabled"));
    }
    public bool F4PolygonColliderCurveFrame0()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var bindings = AnimationUtility.GetCurveBindings(a);
        if (bindings == null) return false;
        var enabledCurve = bindings.FirstOrDefault(b => b.type == typeof(PolygonCollider2D) && b.propertyName.Equals("m_Enabled"));
        if (enabledCurve == null) return false;

        try
        {
            var curve = AnimationUtility.GetEditorCurve(a, enabledCurve);
            if (curve == null || curve.keys == null || curve.keys.Count() == 0) return false;
            return (curve.keys[0].value == 0);
        } catch (System.NullReferenceException) { return false; }
    }
}
#endif