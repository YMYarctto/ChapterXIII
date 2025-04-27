using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMaterial;
using ETag;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using EColor;

public class Potion : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    string potion_name;
    [HideInInspector] public List<Efficacy> EfficacyList;
    [HideInInspector] public List<SideEffect> SideEffectList;
    string potion_description;

    Transform parent;
    Transform onDrag;

    GameObject potion;
    GameObject mini;

    PotionImage current_image;
    bool incollider=false;
    AshBin ash_bin;

    public void Init(List<MaterialName> materialList, List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        EfficacyList = new(efficacyList.OrderBy(x => (int)x).ToList());
        SideEffectList = new(sideEffectList.OrderBy(x => (int)x).ToList());
        var potionInfo = PotionConst.GetPotionName(materialList);
        if (potionInfo.IsNull())
        {
            potionInfo = PotionConst.GetPotionName(efficacyList);
        }
        potion_name = potionInfo.potionName;
        potion_description = potionInfo.potionDescription;
        parent = transform.parent;
        onDrag = GameObject.Find("OnDrag").transform;
        potion = transform.Find("potion").gameObject;
        potion.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        mini = transform.Find("potion_mini").gameObject;
        mini.SetActive(false);
        current_image = PotionImage.potion;

        foreach (var efficacy in efficacyList)
        {
            var sprites = ResourceManager.instance.GetPotionSprite(efficacy);
            if (sprites != null)
            {
                potion.GetComponent<Image>().sprite = sprites.potion;
                potion.transform.Find("bottle_plug").GetComponent<Image>().sprite = sprites.bottle_plug;
                potion.transform.Find("bottle_plug").GetComponent<RectTransform>().sizeDelta=new Vector2(sprites.bottle_plug.bounds.size.x*100,sprites.bottle_plug.bounds.size.y*100);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(onDrag, true);
        transform.position = eventData.position;
        ChangeImage(PotionImage.potion);
        ShowUI();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 射线检测鼠标是否碰撞到碰撞题
        Collider2D collider = Physics2D.Raycast(eventData.position, Vector2.zero).collider;
        // 检测是否有碰撞题，有则是否为物品栏
        if (collider != null)
        {
            if(collider.CompareTag("Ash_bin"))
            {
                UIManager.instance.GetUIView<ItemInfoUI>("ItemInfoUI").RemoveAll();
                Destroy(gameObject);
                return;
            }
            if (collider.CompareTag("Customer"))
            {
                collider.transform.parent.GetComponent<Customer_Normal>().GivePotion(this);
                Destroy(gameObject);
                return;
            }
            if (collider.CompareTag("Inventory"))
            {
                if (collider.transform.childCount == 0)
                {
                    parent = collider.transform;
                    current_image = PotionImage.mini;
                }
            }
            if (collider.CompareTag("InWarehouse"))
            {
                if (!incollider&&collider.transform.childCount == 0)
                {
                    parent = collider.transform;
                    current_image = PotionImage.potion;
                }
            }

        }
        transform.SetParent(parent, true);
        if (current_image == PotionImage.mini)
        {
            ChangeImage(PotionImage.mini);
        }
        transform.localPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Collider2D collider = Physics2D.Raycast(eventData.position, Vector2.zero).collider;
        if (collider != null)
        {
            if(!incollider&&collider.CompareTag("Ash_bin"))
            {
                ash_bin??=collider.GetComponent<AshBin>();
                ash_bin.Open();
                incollider=true;
            }
            if(incollider&&!collider.CompareTag("Ash_bin"))
            {
                ash_bin??=collider.GetComponent<AshBin>();
                ash_bin.Close();
                incollider=false;
            }
            return;
        }
        if(incollider&&ash_bin!=null)
        {
            ash_bin.Close();
            incollider=false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUI();
    }

    void ShowUI()
    {
        var view = UIManager.instance.GetUIView<ItemInfoUI>("ItemInfoUI");
        view.RemoveAllTag();
        view.ShowImage();
        view.ChangeTitle(potion_name);
        view.ChangeDescription(potion_description);
        foreach (Efficacy efficacy in EfficacyList)
        {
            view.AddTag(efficacy.ToString(), TagColor.Efficacy);
        }
        foreach (SideEffect sideEffect in SideEffectList)
        {
            view.AddTag(sideEffect.ToString(), TagColor.SideEffect);
        }
    }

    void ChangeImage(PotionImage image)
    {
        if (image == PotionImage.potion)
        {
            potion.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            mini.SetActive(false);
        }
        else
        {
            potion.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            mini.SetActive(true);
        }
    }

    enum PotionImage
    {
        potion,
        mini,
    }
}
