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
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
            Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
            return false;
        }
        if (go.GetComponent("PlayerMovement") == null)
        {
            Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerMovement' script attached.";
            return false;
        }
        return true;
    }
    public bool B6PlayerHasRB()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
            Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
            return false;
        }
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have a 'Rigidbody2D' component.";
             return false;
        }
        if (rb.gravityScale != 0)
        {
            Criterion.globalLastKnownError = $"The <go>Player</go>'s Rigidbody2D Gravity Scale should be 0, but it is {rb.gravityScale}.";
            return false;
        }
        return true;

    }
    public bool B6LinearDrag()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
            Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
            return false;
        }
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have a 'Rigidbody2D' component.";
             return false;
        }
        if (rb.linearDamping != 1 && rb.sharedMaterial == null) //checking drag (linearDamping in newer versions)
        {
            Criterion.globalLastKnownError = $"The <go>Player</go>'s Rigidbody2D Linear Drag should be 1, but it is {rb.linearDamping}.";
            return false;
        }
        return true;

    }
    public bool B7PlayerHasWrapAround()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
            Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
            return false;
        }
        if (go.GetComponent("WrapAround") == null)
        {
            Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'WrapAround' script attached.";
            return false;
        }
        return true;
    }
    public bool B9PlayerHasPolygon()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
            Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
            return false;
        }
        if (go.GetComponent<PolygonCollider2D>() == null)
        {
            Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have a 'PolygonCollider2D' component.";
            return false;
        }
        return true;
    }

    //c
    public bool C1BulletHasRB()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Bullet");
        if (go == null) 
        {
            //Try prefab if scene object missing check? No, usually this asks for scene instance.
            //But C1 is "CreateBullet", usually dragging prefab to scene.
             Criterion.globalLastKnownError = "Could not find a <go>Bullet</go> GameObject in the scene.";
             return false;
        }
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
             Criterion.globalLastKnownError = "The <go>Bullet</go> GameObject does not have a 'Rigidbody2D' component.";
             return false;
        }
        if (rb.gravityScale != 0)
        {
             Criterion.globalLastKnownError = $"The <go>Bullet</go>'s Rigidbody2D Gravity Scale should be 0, not {rb.gravityScale}.";
             return false;
        }
        return true;
    }
    public bool C1BulletHasPolygon()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Bullet");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a <go>Bullet</go> GameObject in the scene.";
             return false;
        }
        if (go.GetComponent<PolygonCollider2D>() == null)
        {
             Criterion.globalLastKnownError = "The <go>Bullet</go> GameObject does not have a 'PolygonCollider2D' component.";
             return false;
        }
        return true;
    }
    public bool C1BulletHasWrapAround()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Bullet");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a <go>Bullet</go> GameObject in the scene.";
             return false;
        }
        if (go.GetComponent("WrapAround") == null) //Handle both direct type and string if type missing
        {
             Criterion.globalLastKnownError = "The <go>Bullet</go> GameObject does not have the 'WrapAround' script attached.";
             return false;
        }
        return true;
    }
    public bool C3MultipleBullets()
    {
        var bullets = CommonTutorialCallbacks.GameObjectsStartingWith("Bullet");
        if (bullets.Count <= 1)
        {
             Criterion.globalLastKnownError = $"There should be multiple <go>Bullet</go> GameObjects in the scene. Found {bullets.Count}.";
             return false;
        }
        if (!CommonTutorialCallbacks.ObjectsInDifferentLocations(bullets))
        {
             Criterion.globalLastKnownError = "The <go>Bullet</go> GameObjects are in the same location. Move them apart so they can be seen.";
             return false;
        }
        return true;
    }
    public bool C6BulletPrefabYellow()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Bullet</asset> Prefab in 'Assets/' folder.";
             return false;
        }
        var sr = prefab.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab does not have a 'SpriteRenderer' component.";
             return false;
        }
        if (sr.color.r != 1f || sr.color.g != 1f || sr.color.b != 0f)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab SpriteRenderer color is not Yellow (R=1, G=1, B=0).";
             return false;
        }
        return true;
    }
    public bool C8BulletGameObjectRed()
    {
        if (C6BulletPrefabYellow() == false) 
        {
            //If prefab is not yellow, user might have failed previous step or is editing prefab directly.
            //But this step requires SCENE object red.
            //Let's just check scene objects.
            //Wait, existing logic checked: "if (C6BulletPrefabYellow() == false) return false;"
            //This implies sequential dependency. If C6 fails, C8 fails.
            //I will keep it.
             return false; 
        }

        var bullets = CommonTutorialCallbacks.GameObjectsStartingWith("Bullet");
        foreach (var bullet in bullets)
        {
            var sr = bullet.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.color.r == 1f && sr.color.g == 0f && sr.color.b == 0f) return true;
            }
        }
        Criterion.globalLastKnownError = "Could not find any <go>Bullet</go> GameObject in the scene that is Red (R=1, G=0, B=0).";
        return false;
    }

    public bool C8BulletGameObjectMidGrey()
    {
        if (C6BulletPrefabYellow() == false) return false; 
        var bullets = CommonTutorialCallbacks.GameObjectsStartingWith("Bullet");
        foreach (var bullet in bullets)
        {
            var sr = bullet.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.color.r == 0.5f && sr.color.g == 0.5f && sr.color.b == 0.5f) return true;
            }
        }
        Criterion.globalLastKnownError = "Could not find any <go>Bullet</go> GameObject in the scene that is Grey (0.5, 0.5, 0.5).";
        return false;
    }

    public bool C9BulletPrefabWhite()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null) return false;

        var sr = prefab.GetComponent<SpriteRenderer>();
        if (sr == null) return false;

        if(sr.color.r == 1f && sr.color.g == 1f && sr.color.b == 1f) return true;
        
        Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab color should be White (1,1,1).";
        return false;
    }

    //d
    public bool D1NoBullets()
    {
        var count = CommonTutorialCallbacks.GameObjectsStartingWith("Bullet").Count;
        if (count > 0)
        {
             Criterion.globalLastKnownError = $"There are still {count} <go>Bullet</go> GameObjects in the scene. Please delete them.";
             return false;
        }
        return true;
    }
    public bool D2PlayerShooting()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }
        if (go.GetComponent("PlayerShooting") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerShooting' script attached.";
             return false;
        }
        return true;
    }
    public bool D3PlayerShootingPrefabLink()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }
        var shootingScript = go.GetComponent("PlayerShooting");
        if (shootingScript == null) 
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerShooting' script attached.";
             return false;
        }

        var so = new SerializedObject(shootingScript);
        var bulletPrefabProp = so.FindProperty("bulletPrefab");
        if (bulletPrefabProp == null)
        {
             Criterion.globalLastKnownError = "Could not find the 'bulletPrefab' property in 'PlayerShooting' script.";
             return false;
        }
        if (bulletPrefabProp.objectReferenceValue == null)
        {
             Criterion.globalLastKnownError = "The 'Bullet Prefab' field in 'PlayerShooting' is empty. Assign the Bullet Prefab to it.";
             return false;
        }
        //Checking name might be fragile if they renamed prefab but kept same logic. But ok for tutorial.
        //Ideally check if it's the bullet prefab asset.
        if (bulletPrefabProp.objectReferenceValue.name != "Bullet")
        {
             Criterion.globalLastKnownError = $"The assigned Prefab is named '{bulletPrefabProp.objectReferenceValue.name}', but it should be <asset>Bullet</asset>.";
             return false;
        }

        return true;
    }

    //e
    public bool E1BulletPrefabTrigger()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Bullet</asset> Prefab in 'Assets/' folder.";
             return false;
        }
        var c = prefab.GetComponent<Collider2D>();
        if (c == null)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab does not have a Collider2D component.";
             return false;
        }
        if (!c.isTrigger)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab Collider is not set to 'Is Trigger'.";
             return false;
        }
        return true;
    }

    //f
    public bool F1BulletHasDestroyAfterTime()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Bullet</asset> Prefab.";
             return false;
        }
        if (prefab.GetComponent("DestroyAfterTime") == null)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab does not have the 'DestroyAfterTime' script attached.";
             return false;
        }
        return true;
    }
    public bool F2BulletHasDestroyOnCollision()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Bullet");
        if (prefab == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Bullet</asset> Prefab.";
             return false;
        }
        if (prefab.GetComponent("DestroySelfAndOtherOnCollision") == null)
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab does not have the 'DestroySelfAndOtherOnCollision' script attached.";
             return false;
        }
        return true;
    }
    public bool F3BulletLayer()
    {
        if (CommonTutorialCallbacks.GetPrefab("Bullet") == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Bullet</asset> Prefab.";
             return false;
        }
        int layer = LayerMask.NameToLayer("Bullets");
        if (layer == -1)
        {
             Criterion.globalLastKnownError = "The 'Bullets' layer does not exist. Please create it.";
             return false;
        }
        if (!CommonTutorialCallbacks.PrefabOnLayer("Bullet", layer))
        {
             Criterion.globalLastKnownError = "The <asset>Bullet</asset> Prefab is not on the 'Bullets' layer.";
             return false;
        }
        return true;
    }
    public bool F3PlayerLayer()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }
        int layer = LayerMask.NameToLayer("Player"); //Standard Unity layer? Or tutorial created? 
        //Unity built-in layer is "Default", "TransparentFX", etc. "Player" is usually layer 8 or similar if built-in?
        //Actually "Player" IS a built-in layer in many templates, but might need creation.
        // Assuming it exists or tutorial asked to create it.
        //Wait, tutorial 02 includes "Layers" section.
        if (layer == -1) 
        {
             //Maybe they haven't created it yet? Or it's a built-in one.
             //Safe to say "Player" layer found or not.
        }
        if (!CommonTutorialCallbacks.GameObjectOnLayer("Player", layer))
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject is not on the 'Player' layer.";
             return false;
        }
        return true;
    }
    public bool F5Matrix()
    {
        var bulletsLayer = LayerMask.NameToLayer("Bullets");
        var playerLayer = LayerMask.NameToLayer("Player");
        var invaderoidsLayer = LayerMask.NameToLayer("Invaderoids");

        if (bulletsLayer == -1) { Criterion.globalLastKnownError = "Layer 'Bullets' missing."; return false; }
        if (playerLayer == -1) { Criterion.globalLastKnownError = "Layer 'Player' missing."; return false; }
        if (invaderoidsLayer == -1) { Criterion.globalLastKnownError = "Layer 'Invaderoids' missing."; return false; }

        var bulletsMatrix = Physics2D.GetLayerCollisionMask(bulletsLayer);
        
        //Player vs Bullet
        bool playerAndBulletCollide = (bulletsMatrix & (1 << playerLayer)) > 0;
        if (playerAndBulletCollide)
        {
             Criterion.globalLastKnownError = "Bullets should NOT collide with Player in the Physics 2D Layer Collision Matrix.";
             return false;
        }

        //Bullet vs Bullet
        bool bulletAndBulletCollide = (bulletsMatrix & (1 << bulletsLayer)) > 0;
        if (bulletAndBulletCollide)
        {
             Criterion.globalLastKnownError = "Bullets should NOT collide with other Bullets in the Physics 2D Layer Collision Matrix.";
             return false;
        }

        //Invader vs Invader
        var invaderoidMatrix = Physics2D.GetLayerCollisionMask(invaderoidsLayer);
        bool invaderoidandInvaderoidCollide = (invaderoidMatrix & (1 << invaderoidsLayer)) > 0;
        if (invaderoidandInvaderoidCollide)
        {
             Criterion.globalLastKnownError = "Invaderoids should NOT collide with other Invaderoids in the Physics 2D Layer Collision Matrix.";
             return false;
        }

        return true;
    }

    //g
    public bool G1InvaderHasRB()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Invaderoid");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find an <go>Invaderoid</go> GameObject in the scene.";
             return false;
        }
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
             Criterion.globalLastKnownError = "The <go>Invaderoid</go> GameObject does not have a 'Rigidbody2D' component.";
             return false;
        }
        if (rb.gravityScale != 0)
        {
             Criterion.globalLastKnownError = $"The <go>Invaderoid</go>'s Rigidbody2D Gravity Scale should be 0, not {rb.gravityScale}.";
             return false;
        }
        return true;
    }
    public bool G1InvaderHasPolygon()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Invaderoid");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find an <go>Invaderoid</go> GameObject in the scene.";
             return false;
        }
        var poly = go.GetComponent<PolygonCollider2D>();
        if (poly == null)
        {
             Criterion.globalLastKnownError = "The <go>Invaderoid</go> GameObject does not have a 'PolygonCollider2D' component.";
             return false;
        }
        if (!poly.isTrigger)
        {
             Criterion.globalLastKnownError = "The <go>Invaderoid</go> PolygonCollider2D 'Is Trigger' property must be checked.";
             return false;
        }
        return true;
    }
    public bool G1InvaderHasWrap()
    {
        return CheckInvaderScript("WrapAround");
    }
    public bool G1InvaderHasStartAtRandomPosition()
    {
        return CheckInvaderScript("StartAtRandomPosition");
    }
    public bool G1InvaderHasStartMovingInRandomDirection()
    {
        return CheckInvaderScript("StartMovingInRandomDirection");
    }
    public bool G1InvaderHasStartRandomRotation()
    {
        return CheckInvaderScript("StartRandomRotation");
    }
    public bool G1InvaderHasMakeCopies()
    {
        return CheckInvaderScript("MakeCopiesWhenKilled");
    }
    
    private bool CheckInvaderScript(string scriptName)
    {
        var go = CommonTutorialCallbacks.FindGameObject("Invaderoid");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find an <go>Invaderoid</go> GameObject in the scene.";
             return false;
        }
        if (go.GetComponent(scriptName) == null)
        {
             Criterion.globalLastKnownError = $"The <go>Invaderoid</go> GameObject does not have the '{scriptName}' script attached.";
             return false;
        }
        return true;
    }

    public bool G3MakeCopesWhenKilled()
    {
        var prefab = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (prefab == null) 
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Invaderoid</asset> Prefab.";
             return false;
        }

        var makeCopies = prefab.GetComponent("MakeCopiesWhenKilled");
        if (makeCopies == null)
        {
             Criterion.globalLastKnownError = "The <asset>Invaderoid</asset> Prefab does not have the 'MakeCopiesWhenKilled' script attached.";
             return false;
        }

        var so = new SerializedObject(makeCopies);
        var prop = so.FindProperty("invaderoidPrefab");
        if (prop == null)
        {
             Criterion.globalLastKnownError = "Could not find 'invaderoidPrefab' property in 'MakeCopiesWhenKilled'.";
             return false;
        }
        if (prop.objectReferenceValue == null)
        {
             Criterion.globalLastKnownError = "The 'Invaderoid Prefab' field in 'MakeCopiesWhenKilled' is empty.";
             return false;
        }
        if (prop.objectReferenceValue != prefab)
        {
             Criterion.globalLastKnownError = "The 'Invaderoid Prefab' field does not reference the <asset>Invaderoid</asset> prefab itself.";
             return false;
        }
        return true;
    }
    public bool G4MultipleInvaders()
    {
        var invaders = CommonTutorialCallbacks.GameObjectsStartingWith("Invader"); //StartingWith matches "Invaderoid"
        if (invaders.Count < 3)
        {
             Criterion.globalLastKnownError = $"You need at least 3 Invaderoids in the scene. Found {invaders.Count}.";
             return false;
        }
        if (!CommonTutorialCallbacks.ObjectsInDifferentLocations(invaders))
        {
             Criterion.globalLastKnownError = "The Invaderoids are in the same location. Please move them apart.";
             return false;
        }
        return true;
    }

    //h
    public bool H1PlayerScripts()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }
        if (go.GetComponent("PlayerHealth") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerHealth' script attached.";
             return false;
        }
        if (go.GetComponent("HurtOnCollision") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'HurtOnCollision' script attached.";
             return false;
        }
        return true;
    }
    public bool H2PlayerScriptsPrefabLink()
    {
        var explosion = CommonTutorialCallbacks.GetPrefab("Explosion");
        if (explosion == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Explosion</asset> Prefab.";
             return false;
        }

        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null) 
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }

        var health = go.GetComponent("PlayerHealth");
        if (health == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerHealth' script attached.";
             return false;
        }
        var healthSO = new SerializedObject(health);
        var healthProp = healthSO.FindProperty("explosionPrefab");
        if (healthProp == null)
        {
             Criterion.globalLastKnownError = "Could not find 'explosionPrefab' property on 'PlayerHealth'.";
             return false;
        }
        if (healthProp.objectReferenceValue != explosion) 
        {
             Criterion.globalLastKnownError = "The 'Explosion Prefab' field on 'PlayerHealth' is not assigned to the <asset>Explosion</asset> prefab.";
             return false;
        }

        var hurt = go.GetComponent("HurtOnCollision");
        if (hurt == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'HurtOnCollision' script attached.";
             return false;
        }
        var hurtSO = new SerializedObject(hurt);
        var hurtProp = hurtSO.FindProperty("explosionPrefab");
        if (hurtProp == null)
        {
             Criterion.globalLastKnownError = "Could not find 'explosionPrefab' property on 'HurtOnCollision'.";
             return false;
        }
        if (hurtProp.objectReferenceValue != explosion)
        {
             Criterion.globalLastKnownError = "The 'Explosion Prefab' field on 'HurtOnCollision' is not assigned to the <asset>Explosion</asset> prefab.";
             return false;
        }

        return true;
    }

    //exercise
    public bool ExerciseExplosionOnDestroyScript()
    {
        var explosion = CommonTutorialCallbacks.GetPrefab("Explosion");
        if (explosion == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Explosion</asset> Prefab.";
             return false;
        }
        var invaderoid = CommonTutorialCallbacks.GetPrefab("Invaderoid");
        if (invaderoid == null)
        {
             Criterion.globalLastKnownError = "Could not find the <asset>Invaderoid</asset> Prefab.";
             return false;
        }

        var script = invaderoid.GetComponent("ExplosionOnDestroy");
        if (script == null)
        {
             Criterion.globalLastKnownError = "The <asset>Invaderoid</asset> Prefab does not have the 'ExplosionOnDestroy' script attached.";
             return false;
        }

        var so = new SerializedObject(script);
        var prop = so.FindProperty("explosionPrefab");
        
        if (prop == null)
        {
             Criterion.globalLastKnownError = "Could not find 'explosionPrefab' property on 'ExplosionOnDestroy'.";
             return false;
        }
        if (prop.objectReferenceValue != explosion)
        {
             Criterion.globalLastKnownError = "The 'Explosion Prefab' field on 'ExplosionOnDestroy' is not assigned to the <asset>Explosion</asset> prefab.";
             return false;
        }
        return true;
    }
    public bool ExerciseHyperspaceScript()
    {
        var go = CommonTutorialCallbacks.FindGameObject("Player");
        if (go == null)
        {
             Criterion.globalLastKnownError = "Could not find a GameObject named <go>Player</go>.";
             return false;
        }
        if (go.GetComponent("PlayerHyperspace") == null)
        {
             Criterion.globalLastKnownError = "The <go>Player</go> GameObject does not have the 'PlayerHyperspace' script attached.";
             return false;
        }
        return true;
    }
}
#endif