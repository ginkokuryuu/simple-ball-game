using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType { Attacker, Defender };

public class PlayerControl : MonoBehaviour
{
    Camera theCamera;
    [SerializeField] LayerMask fieldLayer = 14;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                print("touch");
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                        PlayerTouch(touch.position);
                }
            }
        }

        if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerTouch(Input.mousePosition);
            }
        }
    }

    void PlayerTouch(Vector3 _position)
    {
        RaycastHit hit;
        Ray ray = theCamera.ScreenPointToRay(_position);
        if(Physics.Raycast(ray, out hit, 3000, fieldLayer))
        {
            print(hit.point);
            float sidePoint = hit.transform.InverseTransformPoint(hit.point).z;
            bool evenMatch = GameManager.INSTANCE.CurrentMatch % 2 == 0;
            Owner owner;
            SoldierType soldierType = SoldierType.Attacker;
            if (sidePoint < 0)
            {
                owner = Owner.Player1;
                if (!evenMatch)
                    soldierType = SoldierType.Defender;
            }
            else
            {
                owner = Owner.Player2;
                if (evenMatch)
                    soldierType = SoldierType.Defender;
            }

            SpawnSoldier(soldierType, owner, hit.point);
        }
    }

    void SpawnSoldier(SoldierType _soldierType, Owner _owner, Vector3 _position)
    {
        PieceSpawner.INSTANCE.TryToSpawnSoldier(_soldierType, _owner, _position);
    }
}
