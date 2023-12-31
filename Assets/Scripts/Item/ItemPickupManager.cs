using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 플레이어가 아이템을 바라볼 때, 아이템의 이름을 텍스트로 띄우며 상호작용 여부를 알려준다. 이 상태에서 상호작용 키 입력 시 해당 아이템을 맵에서 제거한다.
/// </summary>
public class ItemPickUpManager : MonoBehaviour
{
    public static ItemPickUpManager instance;

    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject currentInteractGameObject;
    private IInteractable currentInteractable;

    public Text promptText;
    private Camera camera;

    public float resourceRespawnTerm;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                currentInteractGameObject = hit.collider.gameObject;
                currentInteractable = hit.collider.GetComponent<IInteractable>();
                SetPromptText();
            }
            else
            {
                currentInteractGameObject = null;
                currentInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[F]</b> {0}", currentInteractable.GetInteractPrompt());
    }

    //아이템 습득 시 호출되어 맵에서 해당 아이템을 제거하는 메소드.
    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase == InputActionPhase.Started && currentInteractable != null)
        {
            currentInteractable.OnPickUp();

            // 아이템인가?
            if(currentInteractGameObject.GetComponent<ItemObject>()) {
                // 버린아이템인가?
                if(currentInteractGameObject.GetComponent<Rigidbody>()) {
                    Destroy(currentInteractGameObject);
                }
                else {
                    StartCoroutine(RespawnCoroutine(currentInteractGameObject));
                }
            }
            
            currentInteractGameObject = null;
            currentInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
    
    private IEnumerator RespawnCoroutine(GameObject go) {
        go.SetActive(false);

        yield return new WaitForSeconds(resourceRespawnTerm);

        go.SetActive(true);
    }
}
