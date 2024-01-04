using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Emoji : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EmojiType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool hasCollided = false;
    
    [Header("Actions")]
    public static Action<Emoji, Emoji> onCollisionOtherEmoji;

    [Header("Effects")]
    [SerializeField] private ParticleSystem mergerdParticles;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer.sprite = ActiveSpriteManger.instance.GetActiveSpriteAt((int)type);
    }

    public int GetMytype(){
        return (int)type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ManageCollision(Collision2D other){

       
        //hasCollided = true;

        if(other.collider.TryGetComponent(out Emoji emoji)){
            if(emoji.GetEmojiType() != type){
                //Debug.Log("Cant be merged");
                return;
            }

            //Debug.Log("lets merge this shit");

            onCollisionOtherEmoji?.Invoke(this, emoji);

        }
    }


    private void OnCollisionStay2D(Collision2D other){
        ManageCollision(other);
    }

    private void OnCollisionEnter2D(Collision2D other){
        ManageCollision(other);
    }


    public void MoveTo(UnityEngine.Vector2 targetPosition){
        transform.position = targetPosition;
    }

    public void Merge(){

        //Add Particle Instantiation;

        mergerdParticles.transform.SetParent(null);
        mergerdParticles.Play();


        Destroy(gameObject);
    }

    public EmojiType GetEmojiType(){
        return type;
    }

    public void EnablePhysics(){
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(EnableChecks());
    }

    IEnumerator EnableChecks(){
        yield return new WaitForSeconds(1.5f);
        hasCollided = true;
    }

    public bool HasCollided(){
        return hasCollided;
    }



}
