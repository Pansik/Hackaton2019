using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float attackDamage = 5f;
    private EnemySpawner spawner;

    private float hp = 5;

    // Use this for initialization
    void Start () {
        spawner = transform.parent.GetComponent<EnemySpawner>();
	}
	
	// Update is called once per frame
	void Update () {

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.black);
    }

    public void ShootPlayer()
    {
        PlayerController.Instance.GetHit(attackDamage);
        //Shoot player ...
    }

    public void HitPlayer()
    {
        PlayerController.Instance.GetHit(attackDamage);
        //Hit player
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.GetHit(attackDamage * 2);
            spawner.DeleteEnemyFromList(gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            if(hp == 0)
                Destroy(gameObject);
        }
    }
}
