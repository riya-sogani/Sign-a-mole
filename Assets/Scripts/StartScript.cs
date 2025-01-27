using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;

    // Sprites to alternate between
    public Image mole1;
    public Image mole2;
    public Button start;

    // Time interval between switches (in seconds)
    private float interval = 1.0f;

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the sprite alternating coroutine
        mole1.enabled = false;
        mole2.enabled = false;
        StartCoroutine(AlternateSprites());
    }

    public void StartGame()
    {
        // Reloads the current active scene
        Time.timeScale = 1;
        Debug.Log("here");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    // Update is called once per frame
    private IEnumerator AlternateSprites()
    {
        // Loop indefinitely
        while (true)
        {
            // Set the sprite to sprite1, wait for 3 seconds
            mole2.enabled = false;
            mole1.enabled = true;
            yield return new WaitForSeconds(interval);

            // Set the sprite to sprite2, wait for 3 seconds
            mole2.enabled = true;
            mole1.enabled = false;
            yield return new WaitForSeconds(interval);
        }
    }

    void Update()
    {
        
    }
}
