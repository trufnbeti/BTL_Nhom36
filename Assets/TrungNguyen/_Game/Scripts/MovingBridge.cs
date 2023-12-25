using UnityEngine;
using UnityEngine.Serialization;

public class MovingBridge : MonoBehaviour
{
    [SerializeField] private Transform downPoint, upPoint;
    [SerializeField] private float speed;
    private Vector3 target;
    private Transform tf;
    private bool isUp;

    private void Awake() {
        tf = transform;
    }

    private void Start() {
        OnInit();
    }

    private void OnInit() {
        isUp = Random.value < 0.5f;
        SetTarget();
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag(GameTag.Player.ToString())) {
            other.transform.SetParent(tf);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag(GameTag.Player.ToString())) {
            other.transform.SetParent(null);
        }
    }

    private void SetTarget() {
        target = (isUp) ? upPoint.position : downPoint.position;
    }

    private void Update() {
        tf.position = Vector3.MoveTowards(tf.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(tf.position, target) < 0.1f) {
            isUp = !isUp;
            SetTarget();
        }
    }
}
