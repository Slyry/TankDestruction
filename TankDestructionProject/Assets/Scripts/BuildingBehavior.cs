using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class BuildingBehavior : MonoBehaviour

{
    [SerializeField]
    GMScript gmScript;

    [SerializeField]
    float buildingHealth = 5;

    [SerializeField]
    Material normalMaterial;

    [SerializeField]
    Material redMaterial;

    [SerializeField]
    Material blackMaterial;

    [SerializeField]
    ParticleSystem explosion;

    Renderer rend;

    Collider bCollider;

    void Awake ()
    {
        rend = this.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        bCollider = this.GetComponent<BoxCollider>();
    }

    IEnumerator BuildingDamageLogic()
    {
        if (buildingHealth > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                BuildingDamageFlashRed();
                yield return new WaitForSeconds(.1f);
                BuildingDamageFlashWhite();
                yield return new WaitForSeconds(.1f);
                Debug.Log("test" + i);
            }
        }

        if (buildingHealth == 0)
        {
            
            StartCoroutine(BuildingDamageDestroy());
            explosion.Play();
        }
    }

    IEnumerator BuildingDamageDestroy()
    {
        rend.material = blackMaterial;
        bCollider.isTrigger = true;
        yield return new WaitForSeconds(2f);
        gmScript.destroyedBuildings = (gmScript.destroyedBuildings + 1);
        Destroy(gameObject);
    }

    void BuildingDamageFlashRed()
    {
        
        rend.material = redMaterial;
    }

    void BuildingDamageFlashWhite()
    {

        rend.material = normalMaterial;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "PlayerOneBullet")
        {
            Destroy(col.gameObject);
            buildingHealth--;
            Debug.Log("Boop");
            StartCoroutine(BuildingDamageLogic());
        }
    }
}
