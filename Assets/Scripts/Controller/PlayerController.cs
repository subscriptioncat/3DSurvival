using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 currentMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float xLook;
    private float cameraCurrentXRotation;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private float minXLook;
    private float maxXLook;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        minXLook = -xLook;
        maxXLook = xLook;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 direction = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }

    private void CameraLook()
    {
        cameraCurrentXRotation += mouseDelta.y * lookSensitivity;
        cameraCurrentXRotation = Mathf.Clamp(cameraCurrentXRotation, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-cameraCurrentXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    #region On- XXX -Inputs
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //Debug.Log("Jump");

            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Attack");
        }


    }

    public void OnInteractiveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Interactive");
        }


    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Inventory");
        }


    }

    public void OnStatsInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Stats");
        }


    }
    #endregion

    

    

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + ( transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + ( transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + ( transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + ( transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
    }
}
