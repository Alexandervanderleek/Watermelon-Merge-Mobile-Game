using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralxManager : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject otherBackGround;

    private float spriteWidth;
    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);

        if(transform.position.x < startPos.x - spriteWidth){
            transform.position = startPos;
        }

    }
}
