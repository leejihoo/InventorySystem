using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemContainer: MonoBehaviour, IPointerClickHandler
{
    public BaseItem BaseItem;
    public Image itemImage;
    // 팝업 발생 뿐만 아니라, 사운드 발생이나 클릭 수 체크 등 여러가지가 추가될 수 있고 클릭 시 어떤 일이 벌어지는지 itemContainer는 모르게해서 의존성을 줄인다.
    public Action<BaseItem, ItemType> OnClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked.Invoke(BaseItem,BaseItem.BaseItemModel.Type);
    }

    private void OnDestroy()
    {
        // 모든 이벤트 해제 매우매우 중요, 오브젝트가 파괴되도 구독은 상관없기 때문에 오브젝트가 파괴될 때 무조건 해제해야 된다.
        OnClicked = null;
    }
}
