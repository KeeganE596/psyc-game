using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spark: GameObject in the Swipey game, stays still until tapped (Activated),
//then it will 'pop' and the spark inside will move towards the center
public class Spark : MonoBehaviour
{
    readonly Vector2 TARGET = new Vector2(0, 0);
    protected float velocity = 1;
    protected float velocityMultiplier = 1;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private GameObject sparkParticle;
    protected Animator anim;
    protected bool isActive = false;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (isActive) {
            velocityMultiplier += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, TARGET, ((velocity * Time.deltaTime) * velocityMultiplier));
        }
    }

    //Activate Spark when it is clicked/tapped, called from Swipey_TouchDetection
    public virtual void Activate() {  
        if(!isActive) {
            Despawn();
        }
    }

    public void Deactivate() {
        isActive = false;
    }

    protected void Despawn() {
        GetComponentInChildren<AudioSource>().Play(0);
        anim.SetTrigger("Activate");
        StartCoroutine("WaitForAnim"); //pause movement so animation can finish 

        GameObject ps = Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);

        if (Swipey_Spawner.Instance != null)
        {
            Swipey_Spawner.Instance.AddSparkToQueue(gameObject);
        }
    }

    IEnumerator WaitForAnim() {
        yield return new WaitForSeconds(0.35f);
        isActive = true;
    }
}
