using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLazer : MonoBehaviour
{
   [SerializeField] private float lazerGrowTime = 2f;
   
    private bool isGrowing = true;
    private float lazerRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capCollider2D;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        lazerFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InDestructable>() &&  !other.isTrigger)
        {
            isGrowing = false;
        }
    }

    public void UpdateLazerRange(float lazerRange)
    {
        this.lazerRange = lazerRange;
        StartCoroutine(IncreaseLazerLengthRoutine());
    }

    private void lazerFaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = transform.position - mousePos;
        transform.right = -direction;
    }

    private IEnumerator IncreaseLazerLengthRoutine()
    {
        float timePassed = 0;
        while (spriteRenderer.size.x < lazerRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float  linearT = timePassed / lazerGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, lazerRange, linearT), 1f); // mathf.lerp (grow over time), increase the sprite renderer x property (at start, where it's going, over time)
            capCollider2D.size = new Vector2(Mathf.Lerp(1f, lazerRange, linearT), capCollider2D.size.y);
            capCollider2D.offset = new Vector2((Mathf.Lerp(1f, lazerRange, linearT)) / 2, capCollider2D.offset.y);
            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }
}
