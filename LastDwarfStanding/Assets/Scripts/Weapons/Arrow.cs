using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 firingForce;
    public float damage;
    public float lifeTime;
    public float arrowSpread;

    private Rigidbody _bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        _bulletRB = GetComponent<Rigidbody>();

        //Randomise X/Y values of firingForce
        firingForce.x += Random.Range(-arrowSpread, arrowSpread);
        firingForce.y += Random.Range(-arrowSpread, arrowSpread);

        _bulletRB.AddRelativeForce(firingForce, ForceMode.Impulse);

        StartCoroutine("ArrowTimer");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ArrowTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "EnemyRanger" && other.gameObject.tag != "Arrow")
        {
            if (other.gameObject.tag == "Player")
            {
                //Debug.Log("Deals Damage to Player");
                other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(damage);
            }
            //Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }

}
