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
        var path = "Assets/ExplosionSpritesheet.png";
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null)
        {
             Criterion.globalLastKnownError = $"Could not find 'ExplosionSpritesheet.png' at '{path}'.";
             return false;
        }

        if (importer.spriteImportMode != SpriteImportMode.Multiple)
        {
             Criterion.globalLastKnownError = "The 'Sprite Mode' must be set to 'Multiple'.";
             return false;
        }
        return true;
    }
    public bool B4SpriteSlice()
    {
        var path = "Assets/ExplosionSpritesheet.png";
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) return false;

#pragma warning disable 0618
        if (importer.spritesheet.Count() != 4)
        {
             Criterion.globalLastKnownError = $"The spritesheet should be sliced into 4 sprites (found {importer.spritesheet.Count()}). Click 'Sprite Editor', 'Slice', then 'Apply'.";
             return false;
        }
        return true;
#pragma warning restore 0618
    }

    //c
    public bool C1AnimationCreated()
    {
        if (AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim") == null)
        {
             Criterion.globalLastKnownError = "Could not find 'ExplosionAnimation.anim' in 'Assets/Animations/'.";
             return false;
        }
        return true;
    }
    public bool C2LoopTime()
    {
        var path = "Assets/Animations/ExplosionAnimation.anim";
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>(path); 
        if (a == null) return false; 

        AnimationClipSettings aSettings = AnimationUtility.GetAnimationClipSettings(a);
        if (aSettings.loopTime)
        {
             Criterion.globalLastKnownError = "The 'Loop Time' checkbox should be unchecked.";
             return false;
        }
        return true;
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

        if (windowInstances.Length == 0)
        {
             Criterion.globalLastKnownError = "The Animation Window must be open.";
             return false;
        }

        for (int i = 0; i < windowInstances.Length; i++)
        {
            bool isRecording = (bool)isRecordingProp.GetValue
                (windowInstances[i], null);

            if (isRecording) return true;
        }

        Criterion.globalLastKnownError = "Enable recording mode (red circle button) in the Animation Window.";
        return false;
    }
    public bool C95thFrameIsNone()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var bindings = AnimationUtility.GetObjectReferenceCurveBindings(a);
        if (bindings.Count() != 1)
        {
             Criterion.globalLastKnownError = "The animation should be modifying exactly one Sprite property.";
             return false;
        }

        var binding = bindings[0];
        var curve = AnimationUtility.GetObjectReferenceCurve(a, binding);
        if (curve.Count() < 5)
        {
             Criterion.globalLastKnownError = "The animation needs fewer keyframes or verify you added the empty keyframe at frame 4 (0:04).";
             return false;
        }
        //Actual request is check if 5th element is null? original code said count!=5. 
        // 0, 1, 2, 3 -> 4 sprites. 4th index (5th item) -> null.
        if (curve.Length < 5) return false;

        var last = curve[4];
        if (last.value != null)
        {
             Criterion.globalLastKnownError = "The keyframe at 0:04 should have no sprite assigned (None).";
             return false;
        }
        return true;
    }

    //d
    public bool D1NoExplosionGO()
    {
        if (CommonTutorialCallbacks.GameObjectWithNameExists("ExplosionSpritesheet_0"))
        {
             Criterion.globalLastKnownError = "Delete the 'ExplosionSpritesheet_0' GameObject from the Scene.";
             return false;
        }
        return true;
    }
    public bool D1InvaderoidController()
    {
        if (AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/InvaderoidController.controller") == null)
        {
             Criterion.globalLastKnownError = "Could not find 'InvaderoidController.controller' in 'Assets/Animations/'.";
             return false;
        }
        return true;
    }
    public bool D2InvaderoidAnimator()
    {
        if (CommonTutorialCallbacks.PrefabComponent<Animator>("Invaderoid") == null)
        {
             Criterion.globalLastKnownError = "The 'Invaderoid' Prefab needs an 'Animator' component.";
             return false;
        }
        return true;
    }
    public bool D2ControllerLink()
    {
        var a = CommonTutorialCallbacks.PrefabComponent<Animator>("Invaderoid");
        if (a == null) { Criterion.globalLastKnownError = "Invaderoid Prefab missing Animator."; return false; }

        var ac = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (ac == null) { Criterion.globalLastKnownError = "InvaderoidController missing."; return false; }

        if (a.runtimeAnimatorController != ac)
        {
             Criterion.globalLastKnownError = "Assign 'InvaderoidController' to the 'Controller' field of the Invaderoid's Animator.";
             return false;
        }
        return true;
    }
    public bool D4MovingState2()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        if (!ac.layers.First().stateMachine.states.Any(s => s.state.name.Equals("Moving")))
        {
             Criterion.globalLastKnownError = "The Animator Controller must have a state named 'Moving'.";
             return false;
        }
        return true;
    }
    public bool D5Transitions()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        if (moving.state.transitions.Count() != 1)
        {
             Criterion.globalLastKnownError = "Create a transition from 'Moving' to 'ExplosionAnimation'.";
             return false;
        }
        return true;
    }
    public bool D6DeadState()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        if (!ac.layers.First().stateMachine.states.Any(s => s.state.name.Equals("Dead")))
        {
             Criterion.globalLastKnownError = "Create an empty state named 'Dead'.";
             return false;
        }
        return true;
    }
    public bool D6Transitions()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        //Finding state "ExplosionAnimation"
        var expl = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("ExplosionAnimation"));
        if (expl.state == null)
        {
             Criterion.globalLastKnownError = "State 'ExplosionAnimation' missing.";
             return false;
        }

        if (expl.state.transitions.Count() != 1)
        {
             Criterion.globalLastKnownError = "Create a transition from 'ExplosionAnimation' to 'Dead'.";
             return false;
        }
        return true;
    }
    public bool D7Default()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        if (ac.layers.First().stateMachine.defaultState != moving.state)
        {
             Criterion.globalLastKnownError = "Set 'Moving' as the Layer Default State (Right-click -> Set as Layer Default Check).";
             return false;
        }
        return true;
    }

    //e
    public bool E1Parameter()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        if (!ac.parameters.Any(p => p.name.Equals("Killed")))
        {
             Criterion.globalLastKnownError = "The Animator Controller needs a Trigger parameter named 'Killed'.";
             return false;
        }
        return true;
    }
    public bool E2Parameter()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;
        
        if (moving.state.transitions.Length == 0) return false;

        if (!moving.state.transitions[0].conditions.Any(s => s.parameter.Equals("Killed")))
        {
             Criterion.globalLastKnownError = "The transition from 'Moving' to 'ExplosionAnimation' must have a Condition using the 'Killed' trigger.";
             return false;
        }
        return true;
    }
    public bool E5NoExplosionOnDestroy()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;
        if (prefab.GetComponent("ExplosionOnDestroy") != null)
        {
             Criterion.globalLastKnownError = "Remove the 'ExplosionOnDestroy' script from the 'Invaderoid' Prefab.";
             return false;
        }
        return true;
    }
    public bool E5SendAnimTrigger()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;
        var send = prefab.GetComponent("SendAnimTriggerOnCollision");
        if (send == null)
        {
             Criterion.globalLastKnownError = "The 'Invaderoid' Prefab is missing the 'SendAnimTriggerOnCollision' script.";
             return false;
        }
        
        var so = new SerializedObject(send);
        var triggerName = so.FindProperty("triggerName");
        var nameSubstring = so.FindProperty("nameSubstring");

        if (triggerName.stringValue != "Killed")
        {
             Criterion.globalLastKnownError = $"Set 'Trigger Name' to 'Killed' (currently '{triggerName.stringValue}').";
             return false;
        }
        if (nameSubstring.stringValue != "Bullet")
        {
             Criterion.globalLastKnownError = $"Set 'Name Substring' to 'Bullet' (currently '{nameSubstring.stringValue}').";
             return false;
        }

        return true;
    }
    public bool E5NoDestroySelfKillOtherOnBullet()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;
        if (prefab.GetComponent("DestroySelfAndOtherOnCollision") != null)
        {
             Criterion.globalLastKnownError = "Remove 'DestroySelfAndOtherOnCollision' script from the 'Bullet' Prefab.";
             return false;
        }
        return true;
    }
    public bool E5DestroyOnCollisionOnBullet()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;
        if (prefab.GetComponent("DestroyOnCollision") == null)
        {
             Criterion.globalLastKnownError = "Add 'DestroyOnCollision' script to the 'Bullet' Prefab.";
             return false;
        }
        return true;
    }
    public bool E5Transition()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var expl = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("ExplosionAnimation"));
        if (expl.state == null) return false;

        if (!expl.state.transitions.Any(t => t.hasExitTime == false))
        {
             //Wait, logic in prev code was: Any(t => t.hasExitTime == false)
             //Usually Explosion -> Dead happens after animation finishes, so Exit Time should be TRUE?
             //Let me re-read original code E5Transition. 
             //Line 231: return moving.state.transitions.Any(t => t.hasExitTime == false);
             //This seems to imply user should UNCHECK valid exit time? Or logic is inverted?
             //Usually "Explosion" plays fully then goes to "Dead". So Has Exit Time = true.
             //Maybe valid check is: Are there any transitions WITHOUT exit time? If so, return true.
             //If original code wanted `hasExitTime == false`, I should enforce that.
             //But wait, for Explosion -> Dead, usually we want Exit Time.
             //Maybe I misread D6/E5. D6 handles transitions count. E5 handles settings.
             //Let's stick to original logic: must have hasExitTime == false.
             //Wait, if it's False, it transitions immediately? That cuts off explosion.
             //Maybe "Moving" -> "Explosion" needs false? That was F3NoExitTime.
             //E5Transition is checking "ExplosionAnimation" state transitions.
             //Check F3NoExitTime (Line 259) also checks `hasExitTime == false`.
             //E5 might be wrong in my assumption or the tutorial wants instant death?
             //Wait, "ExplosionAnimation" is the state. Transition to "Dead".
             //If exit time is false, it might rely on something else? or maybe it's just wrong but I must preserve it.
             //Actually, looking at `E5Transition` in original: it checks `moving.state.transitions` where `moving` is "ExplosionAnimation".
             //So "ExplosionAnimation" -> "Dead". HasExitTime == false? 
             //That effectively makes "Dead" happen instantly unless there's a condition. 
             //If no condition and no exit time, it might warn?
             //Let's trust the original code's intent: it returns true if `hasExitTime == false` is found.
             Criterion.globalLastKnownError = "Uncheck 'Has Exit Time' on the transition from 'ExplosionAnimation' to 'Dead'.";
             return false;
        }
        return true;
    }
    public bool F1AnimationEventDestroyer()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;
        if (prefab.GetComponent("AnimationEventObjectDestroyer") == null)
        {
             Criterion.globalLastKnownError = "Add the 'AnimationEventObjectDestroyer' script to the 'Invaderoid' Prefab.";
             return false;
        }
        return true;
    }
    public bool F1AnimationEventAdded()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var events = AnimationUtility.GetAnimationEvents(a);
        if (events.Count() == 0)
        {
             Criterion.globalLastKnownError = "No Animation Events found. Add an event at frame 0:04 calling 'DestroyGameObject'.";
             return false;
        }
        if (events.Count() > 1)
        {
             Criterion.globalLastKnownError = "Too many Animation Events found. There should be only one.";
             return false;
        }

        var e = events[0];
        if (e.functionName != "DestroyGameObject")
        {
             Criterion.globalLastKnownError = $"The Event Function should be 'DestroyGameObject', but is '{e.functionName}'.";
             return false;
        }
        return true;
    }
    public bool F3NoExitTime()
    {
        var rac = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Animations/InvaderoidController.controller"); ;
        if (rac == null) return false;

        var ac = rac as AnimatorController;
        var moving = ac.layers.First().stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Moving"));
        if (moving.state == null) return false;

        if (!moving.state.transitions.Any(t => t.hasExitTime == false))
        {
             Criterion.globalLastKnownError = "Uncheck 'Has Exit Time' on the transition from 'Moving' to 'ExplosionAnimation'.";
             return false;
        }
        return true;
    }
    public bool F4PolygonColliderCurve()
    {
        var a = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animations/ExplosionAnimation.anim"); ;
        if (a == null) return false;

        var bindings = AnimationUtility.GetCurveBindings(a);
        if (!bindings.Any(b => b.type == typeof(PolygonCollider2D) && b.propertyName.Equals("m_Enabled")))
        {
             Criterion.globalLastKnownError = "Record the 'PolygonCollider2D.Enabled' property in the animation.";
             return false;
        }
        return true;
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
            
            //Frame 0 value.
            if (curve.keys[0].value != 0)
            {
                 Criterion.globalLastKnownError = "The Polygon Collider should be disabled (unchecked) at the start of the animation (Frame 0).";
                 return false;
            }
            return true;
        } catch (System.NullReferenceException) { return false; }
    }
}
#endif