using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float pushRadius;
    [Range(0,400)]
    [SerializeField] private float pushMagnitude = 200f;
    private Vector2 pushPosition;



    [Header("Debug")]
    [SerializeField] private bool enableGiz;

    void Awake(){
        MergeManager.onMergeProcessed += MergeProcessedCallback;
    }

    void OnDestroy(){
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
    }

    void MergeProcessedCallback(EmojiType type, Vector2 pos){
        pushPosition = pos;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pushPosition, pushRadius);

        foreach(Collider2D col in colliders){
            if(col.TryGetComponent(out Emoji emoji)){
                Vector2 force = ((Vector2)emoji.transform.position - pushPosition).normalized;

                force *= pushMagnitude;

                emoji.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos(){
        if(!enableGiz){
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pushPosition, pushRadius);

    }
#endif

}
