using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LairCannon : MonoBehaviour {
    
    void OnParticleCollision(GameObject go)
    {
        Debug.Log("Blubl");
        if (go.tag.Equals("Ground"))
        {
            Debug.Log("Blubl" + go.transform.position);
            Building_EnemyLair lair = GetComponentInParent<Building_EnemyLair>();
            GameObject obj = Instantiate(lair.spawnPrefab);
            obj.transform.position = go.transform.position + Vector3.up * 1.2f;
        }
    }

    public IEnumerator SpawnDrops(Building_EnemyLair lair, float startdelay)
    {
        yield return new WaitForSeconds(startdelay);
        //rotate cannon to target
        Vector3 firedirection = FindObjectOfType<Building_Flag>().transform.position - lair.transform.position;
        transform.parent.rotation = Quaternion.LookRotation(firedirection, Vector3.up);

        ParticleSystem spwner = GetComponent<ParticleSystem>();
        while (true)
        {
            yield return new WaitForSeconds(lair.spawnFrequence);
            if (spwner) spwner.Emit(1);
        }
    }
}
