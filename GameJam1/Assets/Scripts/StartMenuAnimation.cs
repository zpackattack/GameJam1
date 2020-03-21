
using UnityEngine;

public class StartMenuAnimation : MonoBehaviour
{
    float Low = .6f;
    float High = 1.2f;
    Vector3 diff = new Vector3(.15f, .15f, .15f);
    bool shrink = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float now = transform.localScale.x;
        transform.Rotate(0f, 0f, 10f * Time.deltaTime);
        if(now >= Low && shrink)
        {
            transform.localScale -= diff * Time.deltaTime;
            now = transform.localScale.x;
            if (now<=Low)
            {
                shrink = false;
            }
        }
        else if(now <= High && !shrink)
        {
            transform.localScale += diff * Time.deltaTime;
            now = transform.localScale.x;
            if (now>=High)
            {
                shrink = true;
            }
        }
    }
}
