#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using System.Collections.Generic;

/// <summary>
/// Implement your Tutorial callbacks here.
/// </summary>
[CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
public class Tutorial02Callbacks : ScriptableObject
{
    public const string DefaultFileName = "Tutorial02Callbacks";

    public static ScriptableObject CreateInstance()
    {
        return ScriptableObjectUtils.CreateAsset<Tutorial02Callbacks>(DefaultFileName);
    }

    //b
    public bool B2PlayerHasPlayerMovement()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("PlayerMovement") != null;
    }
    public bool B6PlayerHasRB()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<Rigidbody2D>("Player");
        if (rb == null) return false;

        return rb.gravityScale == 0;

    }
    public bool B6LinearDrag()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<Rigidbody2D>("Player");
        if (rb == null) return false;

        return rb.linearDamping == 1;

    }
    public bool B7PlayerHasWrapAround()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("WrapAround") != null;
    }
    public bool B9PlayerHasPolygon()
    {

        var rb = CommonTutorialCallbacks.GameObjectComponent<PolygonCollider2D>("Player");
        //return CommonTutorialCallbacks.GameObjectContainsScript<PolygonCollider2D>("Player");
        return rb != null;
    }

    //c
    public bool C1BulletHasRB()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<Rigidbody2D>("Bullet");
        if (rb == null) return false;

        return rb.gravityScale == 0;

    }
    public bool C1BulletHasPolygon()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<PolygonCollider2D>("Bullet");
        //return CommonTutorialCallbacks.GameObjectContainsScript<PolygonCollider2D>("Bullet");
        return rb != null;
    }
    public bool C1BulletHasWrapAround()
    {
        var go = GameObject.Find("Bullet");
        return go != null && go.GetComponent("WrapAround") != null;
    }
    public bool C3MultipleBullets()
    {
        var bullets = CommonTutorialCallbacks.GameObjectsStartingWith("Bullet");

        return bullets.Count > 1 &&
            CommonTutorialCallbacks.ObjectsInDifferentLocations(bullets);
    }
    public bool C6BulletPrefabYellow()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;

        var sr = prefab.GetComponent<SpriteRenderer>();
        if (sr == null) return false;

        return sr.color.r == 1f && sr.color.g == 1f && sr.color.b == 0f;
    }
    public bool C8BulletGameObjectRed()
    {
        if (C6BulletPrefabYellow() == false) return false; //check that they dont just accidentally set the prefab to red, because that would change all of the scene ones to red, making for loop below trigger as true
        foreach (var bullet in CommonTutorialCallbacks.GameObjectsStartingWith("Bullet"))
        {
            var sr = bullet.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.color.r == 1f && sr.color.g == 0f && sr.color.b == 0f) return true;

            }
        }
        return false;
    }

    public bool C8BulletGameObjectMidGrey()
    {
        if (C6BulletPrefabYellow() == false) return false; //check that they dont just accidentally set the prefab to red, because that would change all of the scene ones to red, making for loop below trigger as true
        foreach (var bullet in CommonTutorialCallbacks.GameObjectsStartingWith("Bullet"))
        {
            var sr = bullet.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.color.r == 0.5f && sr.color.g == 0.5f && sr.color.b == 0.5f) return true;

            }
        }
        return false;
    }

    public bool C9BulletPrefabWhite()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;

        var sr = prefab.GetComponent<SpriteRenderer>();
        if (sr == null) return false;

        return sr.color.r == 1f && sr.color.g == 1f && sr.color.b == 1f;
    }

    //d
    public bool D1NoBullets()
    {
        return CommonTutorialCallbacks.GameObjectsStartingWith("Bullet").Count == 0;
    }
    public bool D2PlayerShooting()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("PlayerShooting") != null;
    }
    public bool D3PlayerShootingPrefabLink()
    {
        var go = GameObject.Find("Player");
        if (go == null) return false;
        var shootingScript = go.GetComponent("PlayerShooting");
        if (shootingScript == null) return false;

        var so = new SerializedObject(shootingScript);
        var bulletPrefabProp = so.FindProperty("bulletPrefab");

        return (bulletPrefabProp != null && bulletPrefabProp.objectReferenceValue != null && bulletPrefabProp.objectReferenceValue.name == "Bullet");
    }

    //e
    public bool E1BulletPrefabTrigger()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;

        var c = prefab.GetComponent<Collider2D>();
        if (c == null) return false;

        return c.isTrigger;
    }

    //f
    public bool F1BulletHasDestroyAfterTime()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        return prefab != null && prefab.GetComponent("DestroyAfterTime") != null;
    }
    public bool F2BulletHasDestroyOnCollision()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        return prefab != null && prefab.GetComponent("DestroySelfAndOtherOnCollision") != null;
    }
    public bool F3BulletLayer()
    {
        return CommonTutorialCallbacks.PrefabOnLayer("Bullet", LayerMask.NameToLayer("Bullets"));
    }
    public bool F3PlayerLayer()
    {
        return CommonTutorialCallbacks.GameObjectOnLayer("Player", LayerMask.NameToLayer("Player"));
    }
    public bool F5Matrix()
    {
        var bulletsMatrix = Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Bullets"));
        var playerAndBulletCollide = (bulletsMatrix & (1 << LayerMask.NameToLayer("Player"))) > 0;

        var bulletAndBulletCollide = (bulletsMatrix & (1 << LayerMask.NameToLayer("Bullets"))) > 0;

        var invaderoidMatrix = Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Invaderoids"));
        var invaderoidandInvaderoidCollide = (invaderoidMatrix & (1 << LayerMask.NameToLayer("Invaderoids"))) > 0;

        return playerAndBulletCollide == false && invaderoidandInvaderoidCollide == false && bulletAndBulletCollide == false;
    }

    //g
    public bool G1InvaderHasRB()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<Rigidbody2D>("Invaderoid");
        if (rb == null) return false;

        return rb.gravityScale == 0;
    }
    public bool G1InvaderHasPolygon()
    {
        var rb = CommonTutorialCallbacks.GameObjectComponent<PolygonCollider2D>("Invaderoid");
        //return CommonTutorialCallbacks.GameObjectContainsScript<PolygonCollider2D>("Invaderoid");
        return rb != null && rb.isTrigger;
    }
    public bool G1InvaderHasWrap()
    {
        var go = GameObject.Find("Invaderoid");
        return go != null && go.GetComponent("WrapAround") != null;
    }
    public bool G1InvaderHasStartAtRandomPosition()
    {
        var go = GameObject.Find("Invaderoid");
        return go != null && go.GetComponent("StartAtRandomPosition") != null;
    }
    public bool G1InvaderHasStartMovingInRandomDirection()
    {
        var go = GameObject.Find("Invaderoid");
        return go != null && go.GetComponent("StartMovingInRandomDirection") != null;
    }
    public bool G1InvaderHasStartRandomRotation()
    {
        var go = GameObject.Find("Invaderoid");
        return go != null && go.GetComponent("StartRandomRotation") != null;
    }
    public bool G1InvaderHasMakeCopies()
    {
        var go = GameObject.Find("Invaderoid");
        return go != null && go.GetComponent("MakeCopiesWhenKilled") != null;
    }
    public bool G3MakeCopesWhenKilled()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) return false;

        var makeCopies = prefab.GetComponent("MakeCopiesWhenKilled");
        if (makeCopies == null) return false;

        var so = new SerializedObject(makeCopies);
        var prop = so.FindProperty("invaderoidPrefab");

        return prop != null && prop.objectReferenceValue == prefab;
    }
    public bool G4MultipleInvaders()
    {
        var invaders = CommonTutorialCallbacks.GameObjectsStartingWith("Invader");
        return invaders.Count >= 3 && CommonTutorialCallbacks.ObjectsInDifferentLocations(invaders);
    }

    //h
    public bool H1PlayerScripts()
    {
        var go = GameObject.Find("Player");
        if (go == null) return false;
        return go.GetComponent("PlayerHealth") != null && go.GetComponent("HurtOnCollision") != null;
    }
    public bool H2PlayerScriptsPrefabLink()
    {
        var explosion = CommonTutorialCallbacks.GetPrefab("Explosion");
        var go = GameObject.Find("Player");
        if (go == null) return false;

        var health = go.GetComponent("PlayerHealth");
        if (health == null) return false;
        var healthSO = new SerializedObject(health);
        var healthProp = healthSO.FindProperty("explosionPrefab");
        if (healthProp == null || healthProp.objectReferenceValue != explosion) return false;

        var hurt = go.GetComponent("HurtOnCollision");
        if (hurt == null) return false;
        var hurtSO = new SerializedObject(hurt);
        var hurtProp = hurtSO.FindProperty("explosionPrefab");
        if (hurtProp == null || hurtProp.objectReferenceValue != explosion) return false;

        return true;
    }

    //exercise
    public bool ExerciseExplosionOnDestroyScript()
    {
        var explosion = CommonTutorialCallbacks.GetPrefab("Explosion");
        var invaderoid = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (invaderoid == null) return false;

        var script = invaderoid.GetComponent("ExplosionOnDestroy");
        if (script == null) return false;

        var so = new SerializedObject(script);
        var prop = so.FindProperty("explosionPrefab");

        return prop != null && prop.objectReferenceValue == explosion;
    }
    public bool ExerciseHyperspaceScript()
    {
        var go = GameObject.Find("Player");
        return go != null && go.GetComponent("PlayerHyperspace") != null;
    }
}
#endif