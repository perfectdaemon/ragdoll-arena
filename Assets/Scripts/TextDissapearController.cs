using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TextDissapearController : MonoBehaviour
{
    private Text cachedText;

    public float DestroyAfter;

    public float SpeedY;

    // Use this for initialization
    void Start()
    {
        cachedText = this.GetComponent<Text>();        
        cachedText.CrossFadeAlpha(0, DestroyAfter, true);
        Destroy(this.gameObject, DestroyAfter + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        cachedText.rectTransform.Translate(0, SpeedY * Time.deltaTime, 0);        
    }
}
