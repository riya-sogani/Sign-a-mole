using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Graphics")]
    [SerializeField] private Sprite moleChicken;
    [SerializeField] private Sprite moleWatch;
    [SerializeField] private Sprite moleSing;
    [SerializeField] private Sprite moleHand;
    [SerializeField] private Sprite moleDance;
    [SerializeField] private Sprite moleHit;

    public Vector2 startPosition; //= new Vector2(0f , -3.25f);
    private Vector2 endPosition; //= Vector2.zero;
    private float showDuration = 3f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    private bool hittable = true;

    public enum MoleType {chicken, watch, dance, sing, hand};

    private MoleType moleType;

    public LogicScript logic;

    public CsvWriter csvWriter;
    private void CreateNext()
    {
        int random = Random.Range(1, 6);
        if (random == 1)
        {
            moleType = MoleType.chicken;
            spriteRenderer.sprite = moleChicken;
        }
        else if (random == 2)
        {
            moleType = MoleType.watch;
            spriteRenderer.sprite = moleWatch;
        }
        else if (random == 3)
        {
            moleType = MoleType.dance;
            spriteRenderer.sprite = moleDance;
        }
        else if (random == 4)
        {
            moleType = MoleType.sing;
            spriteRenderer.sprite = moleSing;
        }
        else
        {
            moleType = MoleType.hand;
            spriteRenderer.sprite = moleHand;
        }
        hittable = true;
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            logic.RunRecognizer();
            string recognizedSign = logic.recognizedSign;
            Debug.Log("RECOGNIZED SIGN: " + recognizedSign);
            if(moleType == MoleType.chicken)
            {
                csvWriter.AddValue("chicken");
                if(recognizedSign == "chicken")
                {
                    answerMole();
                }
            }
            else if(moleType == MoleType.watch)
            {
                csvWriter.AddValue("watch");
                if(recognizedSign == "watch")
                {
                    answerMole();
                }
            }
            else if(moleType == MoleType.hand)
            {
                csvWriter.AddValue("hand");
                if(recognizedSign == "hand")
                {
                    answerMole();
                }
            }
            else if(moleType == MoleType.dance)
            {
                csvWriter.AddValue("dance");
                if(recognizedSign == "dance")
                {
                    answerMole();
                }
            }
            else if(moleType == MoleType.sing)
            {
                csvWriter.AddValue("sing");
                if(recognizedSign == "sing")
                {
                    answerMole();
                }
            }
            csvWriter.AddValue(recognizedSign);
            csvWriter.NextRow();
        }
        
    }

    private void answerMole()
    {
        spriteRenderer.sprite = moleHit;
        StopAllCoroutines();
        StartCoroutine(QuickHide());
        hittable = false;
        logic.addScore();
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
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        csvWriter = GameObject.FindGameObjectWithTag("Logic").GetComponent<CsvWriter>();
        startPosition = new Vector2(transform.position.x, transform.position.y - 3.25f);
        endPosition = new Vector2(transform.position.x, transform.position.y);
        transform.localPosition = startPosition;
        float startInterval = Random.Range(1, 10);
        float spawnInterval = Random.Range(10, 20);
        InvokeRepeating("Repetition", startInterval, spawnInterval);
        //StartCoroutine(ShowHide());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
