using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("AAAAAAAAAAAAA");
        if (collision.gameObject.tag == "PlayerP" ||
            collision.gameObject.tag == "PlayerM" ||
            collision.gameObject.tag == "PlayerG") {
            Debug.Log("AAAAAAAAAAAAA");
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerP" ||
            collision.gameObject.tag == "PlayerM" ||
            collision.gameObject.tag == "PlayerG") {

            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
