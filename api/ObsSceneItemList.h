#pragma once
#include <obs.hpp>
#include <vector>

class ObsMain;

//ά��sceneitem�б�����ӿڣ���ˢ�±༭�б�
class ObsSceneItemList
{
public:
    ObsSceneItemList(ObsMain* obsMain);
    ~ObsSceneItemList();

    void Clear() { m_items.clear(); }

    //֪ͨ��������б�
    void OnSceneChanged();
    void OnReorderItems();
    void OnAdd(obs_sceneitem_t *item);
    void OnRemove(obs_sceneitem_t *item);
    void OnSelect(obs_sceneitem_t *item,bool bSel);

    size_t Count();
    OBSSceneItem Get(int idx);
    int GetCurrentIndex() { return m_select; }
    OBSSceneItem GetCurrentSceneItem();

    //item�����ӿ�
    void MoveUp(int idx);
    void MoveDown(int idx);

    void MoveToTop(int idx);
    void MoveToBottom(int idx);

    void SetVisible(int idx,bool visible);
    void SetLocked(int idx, bool locked);
    void SetName(int idx, const char* name);
    void Remove(int idx);
    void Select(int idx);

    const char* itemName(int idx);
    bool itemVisible(int idx);

    //��ȡitem����
    static const char* itemName(OBSSceneItem item);
    static bool itemVisible(OBSSceneItem item);
    static bool itemLocked(OBSSceneItem item);
    static bool itemSelected(OBSSceneItem item);
private:
    ObsMain* m_obsMain;
    std::vector<OBSSceneItem> m_items;
    int m_select;
};

