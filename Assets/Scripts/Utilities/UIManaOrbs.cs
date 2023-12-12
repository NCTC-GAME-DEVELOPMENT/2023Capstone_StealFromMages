using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManaOrbs : MonoBehaviour
{
    [SerializeField]
    private PlayerMana playerMana;
    [SerializeField]
    private GameObject orbPrefab;
    private List<ManaOrbScript> orbs = new List<ManaOrbScript>();
    public void Start() {
        UpdateOrbs();
        playerMana.OnManaUpdate += UpdateOrbs;
    }

    private void UpdateOrbs() {
        ClearCurrentHearts();

        float maxManaRemainder = playerMana.GetMaxMana() % 2;
        int orbsToDraw = (int)((playerMana.GetMaxMana()/2)+maxManaRemainder);
        for (int i = 0; i < orbsToDraw; i++) {
            CreateEmptyHeart();
        }
        for (int i = 0; i < orbs.Count; i++) {
            int manaRemainder = (int)Mathf.Clamp(playerMana.GetMana() - (i*2),0,2);
            orbs[i].SetOrbState((OrbState)manaRemainder);
        }
    }
    public void CreateEmptyHeart() {
        GameObject orbObject = Instantiate(orbPrefab);
        orbObject.transform.SetParent(transform);

        ManaOrbScript orbScript = orbObject.GetComponent<ManaOrbScript>();
        orbScript.SetOrbState(OrbState.Empty);
        orbs.Add(orbScript);
    }
    public void ClearCurrentHearts() {
        foreach (ManaOrbScript orb in orbs) {
            Destroy(orb.gameObject);
        }
        orbs = new List<ManaOrbScript>();
    }
}
