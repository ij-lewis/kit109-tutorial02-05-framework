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
        var go = GameObject.Find("Player");
        if (go == null) return false;
        var movement = go.GetComponent("PlayerMovement");
        if (movement == null) return false;

        var so = new SerializedObject(movement);
        var prop = so.FindProperty("rotationSpeed");
        return prop != null && prop.floatValue == 10f;
    }

    //d
    public bool D1PickupHasCollider()
    {
        //return CommonTutorialCallbacks.GameObjectContainsScript<CircleCollider2D>("Pickup");

        var rb = CommonTutorialCallbacks.GameObjectComponent<CircleCollider2D>("Pickup");
        return rb != null && rb.isTrigger;
    }
    public bool D1PickupHasStartInRandomPosition()
    {
        var go = GameObject.Find("Pickup");
        return go != null && go.GetComponent("StartAtRandomPosition") != null;
    }
    public bool D1PickupNotWhite()
    {
        var sr = CommonTutorialCallbacks.GameObjectComponent<SpriteRenderer>("Pickup");
        if (sr == null) return false;

        return sr.color.r != 1f || sr.color.g != 1f || sr.color.b != 1f;
    }
    public bool D2PickupHasMoveToRandom()
    {
        //this version uses string look up because the script doesnt exist and so we cant compile the generic check
        var go = GameObject.Find("Pickup");
        if (go == null) return false;

        return go.GetComponent("MoveToRandomPositionOnCollision") != null;
        //return CommonTutorialCallbacks.GameObjectContainsScript<MoveToRandomPositionOnCollision>("Pickup");
    }
    public bool D3PlayerTag()
    {
        var go = GameObject.Find("Player");
        if (go == null) return false;

        return go.CompareTag("Player");
    }

    //e
    public bool E2ShieldDistanceToPlayer()
    {
        var player = GameObject.Find("Player");
        if (player == null) return false;
        var shield = GameObject.Find("Shield");
        if (shield == null) return false;

        return (player.transform.position - shield.transform.position).magnitude < 3f;
    }
    public bool E3ShieldIsChild()
    {
        var player = GameObject.Find("Player");
        if (player == null) return false;
        var shield = GameObject.Find("Shield");
        if (shield == null) return false;

        return shield.transform.parent == player.transform;
    }
    public bool E3ShieldX0()
    {
        var shield = GameObject.Find("Shield");
        if (shield == null) return false;

        return shield.transform.localPosition.x.Equals(0);
    }
    public bool E4ShieldHasPolygon()
    {
        //return CommonTutorialCallbacks.GameObjectContainsScript<PolygonCollider2D>("Shield");

        var rb = CommonTutorialCallbacks.GameObjectComponent<PolygonCollider2D>("Shield");
        return rb != null && rb.isTrigger;
    }
    public bool E4ShieldHasDestroyEnemies()
    {
        var go = GameObject.Find("Shield");
        return go != null && go.GetComponent("ShieldDestroyEnemies") != null;
    }
    public bool E4ShieldHasRotate()
    {
        var go = GameObject.Find("Shield");
        return go != null && go.GetComponent("Rotate") != null;
    }
    public bool E3ShieldPlayerLayer()
    {
        var shield = GameObject.Find("Shield");
        if (shield == null) return false;

        return shield.layer == LayerMask.NameToLayer("Player");
    }
    public bool E6ShieldHasNoRotate()
    {
        var go = GameObject.Find("Shield");
        return go != null && go.GetComponent("Rotate") == null;
    }
    public bool E7PivotIs000()
    {
        var shield = GameObject.Find("Shield Pivot");
        if (shield == null) return false;

        return shield.transform.localPosition.Equals(Vector3.zero);
    }
    public bool E7Hierarchy()
    {
        var player = GameObject.Find("Player");
        if (player == null) return false;
        var shieldPivot = GameObject.Find("Shield Pivot");
        if (shieldPivot == null) return false;
        var shield = GameObject.Find("Shield");
        if (shield == null) return false;

        return shield.transform.parent == shieldPivot.transform &&
            shieldPivot.transform.parent == player.transform;
    }
    public bool E8PivotRotation000()
    {
        var shield = GameObject.Find("Shield Pivot");
        if (shield == null) return false;

        return shield.transform.localEulerAngles.Equals(Vector3.zero);
    }
    public bool E8PivotHasRotate()
    {
        var go = GameObject.Find("Shield Pivot");
        return go != null && go.GetComponent("Rotate") != null;
    }

    //exercise
    public bool ExercisePlayerMovementImproved()
    {
        var go = GameObject.Find("Player");
        if (go == null) return false;
        return go.GetComponent("PlayerMovementImproved") != null &&
            go.GetComponent("PlayerMovement") == null;
    }
    public bool ExercisePlayerCollectScore()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("PlayerCollectScore") != null;
    }
    public bool ExerciseShieldControl()
    {
        var go = GameObject.Find("Shield Pivot");
        if (go == null) return false;
        return go.GetComponent("ShieldControl") != null &&
            go.GetComponent("Rotate") == null; 
    }
}
#endif