using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Graphics")]
    [SerializeField] private Sprite moleDad;
    [SerializeField] private Sprite moleElephant;
    [SerializeField] private Sprite moleRed;
    [SerializeField] private Sprite moleWhere;
    [SerializeField] private Sprite moleOwl;
    [SerializeField] private Sprite moleHit;

    public Vector2 startPosition; //= new Vector2(0f , -3.25f);
    private Vector2 endPosition; //= Vector2.zero;
    private float showDuration = 1f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    private bool hittable = true;

    public enum MoleType {dad, elephant, owl, red, where};

    private MoleType moleType;

    private void CreateNext()
    {
        int random = Random.Range(1, 6);
        if (random == 1)
        {
            moleType = MoleType.dad;
            spriteRenderer.sprite = moleDad;
        }
        else if (random == 2)
        {
            moleType = MoleType.elephant;
            spriteRenderer.sprite = moleElephant;
        }
        else if (random == 3)
        {
            moleType = MoleType.owl;
            spriteRenderer.sprite = moleOwl;
        }
        else if (random == 4)
        {
            moleType = MoleType.red;
            spriteRenderer.sprite = moleRed;
        }
        else
        {
            moleType = MoleType.where;
            spriteRenderer.sprite = moleWhere;
        }
        hittable = true;
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            
            spriteRenderer.sprite = moleHit;
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            hittable = false;
        }
        
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Hide();
        }

    }

    public void Hide()
    {
        transform.localPosition = startPosition;
    }

    private IEnumerator ShowHide()
    {
        CreateNext();
        transform.localPosition = startPosition;
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            //Debug.Log(elapsed);
            transform.position = Vector2.Lerp(transform.position, endPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;

        }
        transform.position = endPosition;

        yield return new WaitForSeconds(duration);
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.position = Vector2.Lerp(transform.position, startPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;


    }

    private void Repetition()
    {
        StartCoroutine(ShowHide());
    }

    void Start()
    {
        startPosition = new Vector2(transform.position.x, transform.position.y - 3.25f);
        endPosition = new Vector2(transform.position.x, transform.position.y);
        transform.localPosition = startPosition;
        float startInterval = Random.Range(1, 10);
        float spawnInterval = Random.Range(5, 15);
        InvokeRepeating("Repetition", startInterval, spawnInterval);
        //StartCoroutine(ShowHide());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
