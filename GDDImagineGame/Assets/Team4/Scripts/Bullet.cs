using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 startMarker;
    public Vector3 endMarker;

    private Player owner;

    public float speed;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        
        speed = 50.0f;
        startTime = Time.time;
        startMarker = owner.GetComponent<Transform>().position;
        endMarker = startMarker + owner.GetComponent<Transform>().forward * owner.GetComponent<AllyPlayer>().FireRange;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);
        if (transform.position.Equals(endMarker))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                collider.GetComponent<EnemyPlayer>().InitDeath();
                break;
            default:
                break;
        }
    }

    public void setOwner(Player owner)
    {
        this.owner = owner;
    }
}
