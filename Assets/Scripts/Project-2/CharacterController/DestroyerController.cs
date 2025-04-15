using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out StackController stack)) {
            if (stack.MoveSpeed <= 0) return;

            StackManager.Instance.DespawnStack(stack);
        }
    }

}
