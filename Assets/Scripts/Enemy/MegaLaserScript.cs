using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaLaserScript : MonoBehaviour
{
    [SerializeField]
    private new BoxCollider2D collider;
    [SerializeField]
    private bool hasHitRecently;
    [SerializeField]
    private bool didWallHit;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private int petalsAlive;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private float rotationModifier;
    public void Start() {
        collider.enabled = false;
        didWallHit = false;
        hasHitRecently = false;
        transform.localPosition = new Vector2(0, 0f);
        transform.localScale = new Vector2(0, 0f);
    }
    public void Shoot(int _petalsAlive) {
        petalsAlive = _petalsAlive;
        RaycastHit2D hit = Physics2D.Linecast(parent.position, Pathfinder.Instance.GetPlayerPosition(),layerMask);
        Debug.Log("Distance : " + hit.distance);
        Vector3 vector = Pathfinder.Instance.GetPlayerPosition() - (Vector2)transform.position;
        Debug.Log(Vector2.Angle((Vector2)parent.position, Pathfinder.Instance.GetPlayerPosition()));
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        collider.enabled = true;
        parent.rotation = q;
        if (hit.collider != null) {
            Debug.Log("Hit this : " + hit.collider.name);   
            transform.localPosition = new Vector3(hit.distance / -2, 0f, -2);
            transform.localScale = new Vector3(hit.distance, .3f, 1);
            TickSystem.Instance.CreateTimer(StopShooting, 2);
        }
        else {
            Debug.Log("Hit Nothing ");
            transform.localPosition = new Vector3(-50, 0f, -2);
            transform.localScale = new Vector3(100, .3f, 1);
            TickSystem.Instance.CreateTimer(StopShooting, 2);
        }
    }
    private void StopShooting() {
        collider.enabled = false;
        didWallHit = false;
        hasHitRecently = false;
        transform.localPosition = new Vector2(0, 0f);
        transform.localScale = new Vector2(0, 0f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (!hasHitRecently && collision.gameObject.tag == "Player") {
            hasHitRecently = true;
            collision.GetComponent<IHealth>().ApplyDamage(petalsAlive);
        }
        if (!didWallHit && collision.gameObject.layer == 6) {
            didWallHit= true;
            collision.gameObject.GetComponent<IHealth>().ApplyDamage(1);
        }
    }
}
