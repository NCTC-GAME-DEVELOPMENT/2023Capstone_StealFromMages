using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerBossDeath : MonoBehaviour
{
    [SerializeField]
    private EnemyMain flowerBoss;
    private void Start() {
        flowerBoss.OnDeathEvent += OnDeath;
    }
    void OnDeath() {
        Destroy(Pathfinder.Instance.gameObject);
        StartCoroutine(DeathCoroutine(3));
    }
    IEnumerator DeathCoroutine(int _delay) {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(6);
        SceneManager.UnloadSceneAsync(3);
        SceneManager.UnloadSceneAsync(5);
    }
}
