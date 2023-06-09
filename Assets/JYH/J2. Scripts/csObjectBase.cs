﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TeamInterface;

// 나무 오브젝트 시간에 따라 성장/ 열매생성
// 흙 위에 씨앗을 뿌리고 흙을 덮으면 나무가 자라야한다...

public class csObjectBase : MonoBehaviour, IObjectStatus, IDropItem
{
    [SerializeField]
    float hp = 0;
    public float Hp { get { return hp; } set { hp = value; } }

    [SerializeField]
    float stamina = 0;
    public float Stamina { get { return stamina; } set { stamina = value; } }

    public float maxHP;

    public float HpFill() { return hp / maxHP; }


    [SerializeField]
    GameObject[] dropItems;
    public GameObject[] DropItems { get { return dropItems; } set { dropItems = value; } }

    bool isDie = false;

    public Enum_ObjectType ObjType;

    public void SetHpDamaged(float dmg, Enum_PlayerUseItemType useItemType) 
    {
        switch (ObjType) {
            case Enum_ObjectType.NONE:     
            case Enum_ObjectType.FLOWER:
            case Enum_ObjectType.GRASS:
                if (useItemType == Enum_PlayerUseItemType.HAND)
                {
                    Shake();
                }
                else
                {
                    hp-= dmg;
                }
                break;
            case Enum_ObjectType.TREE:

                if (useItemType == Enum_PlayerUseItemType.AXE)
                {
                    hp-= dmg;
                }
                else
                {
                    Shake();
                }
                    break;
            case Enum_ObjectType.ROCK:
                if (useItemType == Enum_PlayerUseItemType.PICKAXE)
                {
                    hp-= dmg;
                }
                else
                {
                    Shake();
                }
                break;
        }
    }

    public virtual void Shake()
    {
        Debug.Log("shake~~");
    }

    public virtual void DropItemFct()
    {
        StartCoroutine(Drop());
    }

    IEnumerator Drop()
    {
        Collider tmpcol = this.GetComponent<Collider>();

        if (tmpcol != null)
        {
            tmpcol.isTrigger = true;
        }

        if (dropItems.Length>0)//처음자리에 무조건 이펙트 파티클 넣으면 이쁠듯
        {
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                for (int i = 0; i < dropItems.Length; i++)
                {
                    GameObject tmp = Instantiate(dropItems[i], new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                    tmp.GetComponent<Rigidbody>().AddForce(Vector3.up * Time.deltaTime * (Random.Range(2, 5) * 5000f));
                    tmp.transform.SetParent(null);
                    //Debug.Log("drop");

                    //사운드 재생?

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }

        transform.parent.GetComponent<ICubeInfo>().CubeInfo.haveChild = false;

        Destroy(gameObject);
    }

    public virtual void Update()
    {
        if (hp <= 0 && !isDie)
        {
            //Debug.Log("die check");
            isDie = true;

            DropItemFct();
        }
    }
}
