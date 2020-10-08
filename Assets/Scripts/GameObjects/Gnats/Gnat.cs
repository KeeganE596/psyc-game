using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Gnatt: GameObject in the Swipey game, always move towards the center, 
//when swiped despawn is called and gnatt will fade away over a couple seconds
public class Gnat : MonoBehaviour
{
    readonly Vector2 TARGET = new Vector2(0, 0);

    [SerializeField] protected GameObject destroyParticle;

    protected float velocity = 1.2f;
    protected bool doDespawn = false;
    protected SpriteRenderer sprite;
    protected float alpha = 1.0f;
    protected Animator anim;

    private bool isFlipped = false;
    

    protected virtual void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, TARGET, velocity * Time.deltaTime);
    }

    public virtual void Despawn(Vector2 gnatThrow) 
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(gnatThrow);
        Despawn();
    }

    public virtual void Despawn()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<AudioSource>().Play(0);
        anim.SetTrigger("destroy");
        StartCoroutine("EndGnat");
    }

    IEnumerator EndGnat() {
        //yield return new WaitForSeconds(0.15f);
        SpawnParticles();

        if (GetComponentInChildren<TextMeshPro>() != null)
        {
            TextMeshPro text = GetComponentInChildren<TextMeshPro>();
            for (byte a = 255; a > 10; a -= 10)
            {
                text.color = new Color32(255, 255, 255, a);
                yield return new WaitForSeconds(0.02f);
            }
        }
        
        yield return new WaitForSeconds(0.85f);
        ResetGnat();
    }

    IEnumerator EndTextGnat() {
        SpawnParticles();
        TextMeshPro text = GetComponentInChildren<TextMeshPro>();
        for(byte a=255; a>10; a-=10) {
            text.color = new Color32(255, 255, 255, a);
            yield return new WaitForSeconds(0.02f);
        }
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        doDespawn = false;
    }

    public virtual void FlipSprite() {
        isFlipped = true;
        transform.localScale = new Vector3(-(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
    }

    public void SetSpeed(float speed) {
        velocity = 1 + speed;
    }

    protected virtual void SpawnParticles() {
        GameObject ps = Instantiate(destroyParticle, gameObject.transform.position, Quaternion.identity);
        ParticleSystem.MainModule pModule = ps.GetComponent<ParticleSystem>().main;
    }

    public virtual void ChooseSprite() {
        return;
    }

    protected virtual void ResetGnat()
    {
        if (isFlipped)
        {
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }
        
        gameObject.SetActive(false);        
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        doDespawn = false;
        if(Swipey_Spawner.Instance != null)
        {
            Swipey_Spawner.Instance.AddGnatToQueue(gameObject);
        }
    }
}
