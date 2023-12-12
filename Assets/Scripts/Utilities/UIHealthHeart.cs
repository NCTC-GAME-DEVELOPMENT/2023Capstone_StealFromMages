using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthHeart : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private GameObject heartPrefab;
    private List<HeartScript> hearts = new List<HeartScript> ();
    public void Start() {
        UpdateHearts();
        playerHealth.OnPlayerHealthUpdate += UpdateHearts;
    }
    public void UpdateHearts() {
        ClearCurrentHearts();

        float maxHealthRemainder = playerHealth.GetMaxHealth() % 2;
        int heartsToDraw = (int)((playerHealth.GetMaxHealth()/2)+maxHealthRemainder);
        for (int i = 0; i < heartsToDraw; i++) {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++) {
            int heartRemainder = (int)Mathf.Clamp(playerHealth.GetHealth() - (i*2),0,2);
            hearts[i].SetHeartState((HeartState)heartRemainder);
        }
    }
    public void CreateEmptyHeart() {
        GameObject heartObject = Instantiate(heartPrefab);
        heartObject.transform.SetParent(transform);

        HeartScript heartScript = heartObject.GetComponent<HeartScript>();
        heartScript.SetHeartState(HeartState.Empty);
        hearts.Add(heartScript);
    }
    public void ClearCurrentHearts() {
        foreach (HeartScript heart in hearts) {
            Destroy (heart.gameObject);
        }
        hearts = new List<HeartScript> ();
    }
}
