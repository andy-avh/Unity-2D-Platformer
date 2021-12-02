
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed; // speed of the projectile
    private float direction; // which direction the fireball is flying
    private bool hit; // did projectile hit?
    private float lifetime; // how many seconds the projectile has been active, they will need to eventually terminate so they can be reused

    // reference to our boxcollider and animator
    private BoxCollider2D boxcollider;
    private Animator anim;

    private void Awake()
    {
        // get referenvce to animator and boxcollider
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) // check if fireball hit something
        {
            return;

        }
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0); // move the fireball by the x axis (movement speed), and not y or z (0, 0)

        lifetime += Time.deltaTime; // calculate lifetime of a fireball (just time)
        if (lifetime > 3) // how long a fireball has before it goes
        {
            gameObject.SetActive(false); // terminate the fireball if goes for > 3 seconds without hitting anything
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // check if fireball hit other objects
    {
        hit = true;
        boxcollider.enabled = false; // turn off boxcollider on fireball
        anim.SetTrigger("explode"); // set off the explosion animation
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0; // reset lifetime of each fireball
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider.enabled = true;

        // this is to make sure the fireball object/animation travels in the right way (so fireball going left, fireball actually faces left, as does the animation)
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) // fireball is not facing the right way...
        {
            localScaleX = -localScaleX; // ...so we flip it
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    // deactivate the fireball once the animation ends
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
