#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using System.Collections.Generic;

/// <summary>
/// Implement your Tutorial callbacks here.
/// </summary>
[CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
public class Tutorial03Callbacks : ScriptableObject
{
    public const string DefaultFileName = "Tutorial03Callbacks";

    public static ScriptableObject CreateInstance()
    {
        return ScriptableObjectUtils.CreateAsset<Tutorial02Callbacks>(DefaultFileName);
    }

    //b
    public bool B6RotationSpeedNow10()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Player'.";
             return false;
        }
        var movement = go.GetComponent("PlayerMovement");
        if (movement == null)
        {
             Criterion.globalLastKnownError = "The 'Player' GameObject does not have the 'PlayerMovement' script attached.";
             return false;
        }

        var so = new SerializedObject(movement);
        var prop = so.FindProperty("rotationSpeed");
        if (prop == null)
        {
             Criterion.globalLastKnownError = "Could not find 'rotationSpeed' property in 'PlayerMovement' script.";
             return false;
        }
        if (prop.floatValue != 10f)
        {
             Criterion.globalLastKnownError = $"The 'Rotation Speed' should be 10, but it is {prop.floatValue}.";
             return false;
        }
        return true;
    }

    //d
    public bool D1PickupHasCollider()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Pickup");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Pickup'.";
             return false;
        }
        var col = go.GetComponent<CircleCollider2D>();
        if (col == null)
        {
             Criterion.globalLastKnownError = "The 'Pickup' GameObject does not have a 'CircleCollider2D' component.";
             return false;
        }
        if (!col.isTrigger)
        {
             Criterion.globalLastKnownError = "The 'Pickup' CircleCollider2D 'Is Trigger' property must be checked.";
             return false;
        }
        return true;
    }
    public bool D1PickupHasStartInRandomPosition()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Pickup");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Pickup'.";
             return false;
        }
        if (go.GetComponent("StartAtRandomPosition") == null)
        {
             Criterion.globalLastKnownError = "The 'Pickup' GameObject does not have the 'StartAtRandomPosition' script attached.";
             return false;
        }
        return true;
    }
    public bool D1PickupNotWhite()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Pickup");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Pickup'.";
             return false;
        }
        var sr = go.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
             Criterion.globalLastKnownError = "The 'Pickup' GameObject does not have a 'SpriteRenderer' component.";
             return false;
        }
        if (sr.color.r == 1f && sr.color.g == 1f && sr.color.b == 1f)
        {
             Criterion.globalLastKnownError = "The 'Pickup' color is still White. Please change it to something else.";
             return false;
        }
        return true;
    }
    public bool D2PickupHasMoveToRandom()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Pickup");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Pickup'.";
             return false;
        }
        if (go.GetComponent("MoveToRandomPositionOnCollision") == null)
        {
             Criterion.globalLastKnownError = "The 'Pickup' GameObject does not have the 'MoveToRandomPositionOnCollision' script attached.";
             return false;
        }
        return true;
    }
    public bool D3PlayerTag()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Player'.";
             return false;
        }
        if (!go.CompareTag("Player"))
        {
             Criterion.globalLastKnownError = $"The 'Player' GameObject Tag is set to '{go.tag}', but it should be 'Player'.";
             return false;
        }
        return true;
    }

    //e
    public bool E2ShieldDistanceToPlayer()
    {
        var player = CommonTutorialCallbacks.FindGameObject("Player");
        if (player == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Player'.";
             return false;
        }
        var shield = CommonTutorialCallbacks.FindGameObject("Shield");
        if (shield == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named 'Shield'.";
             return false;
        }

        float dist = (player.transform.position - shield.transform.position).magnitude;
        if (dist >= 3f)
        {
             Criterion.globalLastKnownError = $"The 'Shield' is too far from the 'Player' ({dist:F2} units). Move it closer.";
             return false;
        }
        return true;
    }
    public bool E3ShieldIsChild()
    {
        var player = CommonTutorialCallbacks.FindGameObject("Player");
        if (player == null) { Criterion.globalLastKnownError = "Player missing."; return false; }
        var shield = CommonTutorialCallbacks.FindGameObject("Shield");
        if (shield == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }

        if (shield.transform.parent != player.transform)
        {
             Criterion.globalLastKnownError = "The 'Shield' GameObject should be a child of 'Player' in the Hierarchy.";
             return false;
        }
        return true;
    }
    public bool E3ShieldX0()
    {
        var shield = CommonTutorialCallbacks.FindGameObject("Shield");
        if (shield == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }

        if (!Mathf.Approximately(shield.transform.localPosition.x, 0f))
        {
             Criterion.globalLastKnownError = $"The 'Shield' local X position should be 0, currently {shield.transform.localPosition.x}.";
             return false;
        }
        return true;
    }
    public bool E4ShieldHasPolygon()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Shield");
        if (go == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }
        var col = go.GetComponent<PolygonCollider2D>();
        if (col == null)
        {
             Criterion.globalLastKnownError = "The 'Shield' does not have a PolygonCollider2D.";
             return false;
        }
        if (!col.isTrigger)
        {
             Criterion.globalLastKnownError = "The 'Shield' PolygonCollider2D must be a Trigger.";
             return false;
        }
        return true;
    }
    public bool E4ShieldHasDestroyEnemies()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Shield");
        if (go == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }
        if (go.GetComponent("ShieldDestroyEnemies") == null)
        {
             Criterion.globalLastKnownError = "The 'Shield' does not have 'ShieldDestroyEnemies' script.";
             return false;
        }
        return true;
    }
    public bool E4ShieldHasRotate()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Shield");
        if (go == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }
        if (go.GetComponent("Rotate") == null)
        {
             Criterion.globalLastKnownError = "The 'Shield' does not have 'Rotate' script.";
             return false;
        }
        return true;
    }
    public bool E3ShieldPlayerLayer()
    {
        var shield = CommonTutorialCallbacks.FindGameObject("Shield");
        if (shield == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }
        //Assuming Player layer exists if we reach here usually
        int playerLayer = LayerMask.NameToLayer("Player");
        if (shield.layer != playerLayer)
        {
             Criterion.globalLastKnownError = "The 'Shield' is not on the 'Player' layer.";
             return false;
        }
        return true;
    }
    public bool E6ShieldHasNoRotate()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Shield");
        if (go == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }
        if (go.GetComponent("Rotate") != null)
        {
             Criterion.globalLastKnownError = "The 'Shield' still has the 'Rotate' script. Please remove it.";
             return false;
        }
        return true;
    }
    public bool E7PivotIs000()
    {
        var shield = CommonTutorialCallbacks.FindGameObject("Shield Pivot");
        if (shield == null) { Criterion.globalLastKnownError = "Could not find 'Shield Pivot'."; return false; }

        if (!shield.transform.localPosition.Equals(Vector3.zero))
        {
             Criterion.globalLastKnownError = "The 'Shield Pivot' position should be (0, 0, 0) relative to its parent.";
             return false;
        }
        return true;
    }
    public bool E7Hierarchy()
    {
        var player = CommonTutorialCallbacks.FindGameObject("Player");
        if (player == null) { Criterion.globalLastKnownError = "Player missing."; return false; }
        var shieldPivot = CommonTutorialCallbacks.FindGameObject("Shield Pivot");
        if (shieldPivot == null) { Criterion.globalLastKnownError = "Shield Pivot missing."; return false; }
        var shield = CommonTutorialCallbacks.FindGameObject("Shield");
        if (shield == null) { Criterion.globalLastKnownError = "Shield missing."; return false; }

        if (shieldPivot.transform.parent != player.transform)
        {
             Criterion.globalLastKnownError = "'Shield Pivot' should be a child of 'Player'.";
             return false;
        }
        if (shield.transform.parent != shieldPivot.transform)
        {
             Criterion.globalLastKnownError = "'Shield' should be a child of 'Shield Pivot'.";
             return false;
        }
        return true;
    }
    public bool E8PivotRotation000()
    {
        var shield = CommonTutorialCallbacks.FindGameObject("Shield Pivot");
        if (shield == null) { Criterion.globalLastKnownError = "Shield Pivot missing."; return false; }

        if (shield.transform.localEulerAngles.magnitude > 0.1f) //Floating point safe check vs zero
        {
             Criterion.globalLastKnownError = "Reset the 'Shield Pivot' rotation to (0, 0, 0).";
             return false;
        }
        return true;
    }
    public bool E8PivotHasRotate()
    {
         var go = CommonTutorialCallbacks.FindGameObject("Shield Pivot");
         if (go == null) { Criterion.globalLastKnownError = "Shield Pivot missing."; return false; }
         if (go.GetComponent("Rotate") == null)
         {
              Criterion.globalLastKnownError = "'Shield Pivot' needs the 'Rotate' script.";
              return false;
         }
         return true;
    }

    //exercise
    public bool ExercisePlayerMovementImproved()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null) { Criterion.globalLastKnownError = "Player missing."; return false; }
        if (go.GetComponent("PlayerMovement") != null)
        {
             Criterion.globalLastKnownError = "Please remove the old 'PlayerMovement' script from 'Player' and add 'PlayerMovementImproved'.";
             return false;
        }
        if (go.GetComponent("PlayerMovementImproved") == null)
        {
             Criterion.globalLastKnownError = "'Player' is missing the 'PlayerMovementImproved' script.";
             return false;
        }
        return true;
    }
    public bool ExercisePlayerCollectScore()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null) { Criterion.globalLastKnownError = "Player missing."; return false; }
        if (go.GetComponent("PlayerCollectScore") == null)
        {
             Criterion.globalLastKnownError = "'Player' is missing the 'PlayerCollectScore' script.";
             return false;
        }
        return true;
    }
    public bool ExerciseShieldControl()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Shield Pivot");
        if (go == null) { Criterion.globalLastKnownError = "Shield Pivot missing."; return false; }
        if (go.GetComponent("Rotate") != null)
        {
             Criterion.globalLastKnownError = "Please remove 'Rotate' script from 'Shield Pivot' and use 'ShieldControl' instead.";
             return false;
        }
        if (go.GetComponent("ShieldControl") == null)
        {
             Criterion.globalLastKnownError = "'Shield Pivot' is missing the 'ShieldControl' script.";
             return false;
        }
        return true;
    }
}
#endif