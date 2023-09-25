using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManagerDefault playerInput;
    private Rigidbody2D body;

    [SerializeField]
    private float speed = 5f;

    void Awake()
    {
        playerInput = new InputManagerDefault();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }


    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Update()
    {
        var inputX = Mathf.RoundToInt(playerInput.Player.Move.ReadValue<Vector2>().x);
        var inputY = Mathf.RoundToInt(playerInput.Player.Move.ReadValue<Vector2>().y);

        body.velocity = speed * new Vector2(inputX, inputY).normalized;
    }
}
