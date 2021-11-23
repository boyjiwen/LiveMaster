#include "ObsSceneItemList.h"
#include "ObsMain.h"

static inline OBSScene GetCurrentScene()
{
    return ObsMain::Instance()->GetCurrentScene();
}

ObsSceneItemList::ObsSceneItemList(ObsMain* obsMain)
    :m_obsMain(obsMain),m_select(-1)
{

}

ObsSceneItemList::~ObsSceneItemList()
{

}

static bool enumItem(obs_scene_t*, obs_sceneitem_t *item, void *ptr)
{
    std::vector<OBSSceneItem> &items =
        *reinterpret_cast<std::vector<OBSSceneItem>*>(ptr);

    if (obs_sceneitem_is_group(item)) {
        obs_data_t *data = obs_sceneitem_get_private_settings(item);

        bool collapse = obs_data_get_bool(data, "collapsed");
        if (!collapse) {
            obs_scene_t *scene =
                obs_sceneitem_group_get_scene(item);

            obs_scene_enum_items(scene, enumItem, &items);
        }

        obs_data_release(data);
    }
    items.insert(items.begin(), item);
    return true;
}

void ObsSceneItemList::OnSceneChanged()
{
    OBSScene scene = GetCurrentScene();
    if (scene)
    {
        m_items.clear();
        obs_scene_enum_items(scene, enumItem, &m_items);

        if (m_obsMain->observer())
            m_obsMain->observer()->OnReloadSceneItemList();
    }
}

static inline void MoveItem(std::vector<OBSSceneItem> &items, int oldIdx, int newIdx)
{
    OBSSceneItem item = items[oldIdx];
    items.erase(items.begin()+oldIdx);
    items.insert(items.begin()+newIdx, item);
}



void ObsSceneItemList::OnReorderItems()
{
    OBSScene scene = GetCurrentScene();

    std::vector<OBSSceneItem> newitems;
    obs_scene_enum_items(scene, enumItem, &newitems);

    /* if item list has changed size, do full reset */
    if (newitems.size() != m_items.size()) {
        OnSceneChanged();
        return;
    }

    for (;;) {
        int idx1Old = 0;
        int idx1New = 0;
        int count;
        int i;

        /* find first starting changed item index */
        for (i = 0; i < newitems.size(); i++) {
            obs_sceneitem_t *oldItem = m_items[i];
            obs_sceneitem_t *newItem = newitems[i];
            if (oldItem != newItem) {
                idx1Old = i;
                break;
            }
        }

        /* if everything is the same, break */
        if (i == newitems.size()) {
            break;
        }

        /* find new starting index */
        for (i = idx1Old + 1; i < newitems.size(); i++) {
            obs_sceneitem_t *oldItem = m_items[idx1Old];
            obs_sceneitem_t *newItem = newitems[i];

            if (oldItem == newItem) {
                idx1New = i;
                break;
            }
        }

        /* if item could not be found, do full reset */
        if (i == newitems.size()) {
            OnSceneChanged();
            return;
        }

        /* get move count */
        for (count = 1; (idx1New + count) < newitems.size(); count++) {
            int oldIdx = idx1Old + count;
            int newIdx = idx1New + count;

            obs_sceneitem_t *oldItem = m_items[oldIdx];
            obs_sceneitem_t *newItem = newitems[newIdx];

            if (oldItem != newItem) {
                break;
            }
        }

        //Ìø×ªitemsµÄË³Ðò
        for (i = 0; i < count; i++) {
            int to = idx1New + count;
            if (to > idx1Old)
                to--;
            MoveItem(m_items, idx1Old, to);
        }

        if (m_obsMain->observer())
            m_obsMain->observer()->OnReloadSceneItemList();

        break;
    }
}

void ObsSceneItemList::OnAdd(obs_sceneitem_t *item)
{
    if (obs_sceneitem_is_group(item)) {
        OnSceneChanged();
    }
    else {
        m_items.insert(m_items.begin(), item);
    }

    if (m_obsMain->observer())
        m_obsMain->observer()->OnReloadSceneItemList();
}
void ObsSceneItemList::OnRemove(obs_sceneitem_t *item)
{
    int idx = -1;
    for (int i = 0; i < m_items.size(); i++) {
        if (m_items[i] == item) {
            idx = i;
            break;
        }
    }

    if (idx == -1)
        return;

    int startIdx = idx;
    int endIdx = idx;

    bool is_group = obs_sceneitem_is_group(item);
    if (is_group) {
        obs_scene_t *scene = obs_sceneitem_group_get_scene(item);

        for (int i = endIdx + 1; i < m_items.size(); i++) {
            obs_sceneitem_t *subitem = m_items[i];
            obs_scene_t *subscene =
                obs_sceneitem_get_scene(subitem);

            if (subscene == scene)
                endIdx = i;
            else
                break;
        }
    }

    m_items.erase(m_items.begin()+idx, m_items.begin() + endIdx + 1);
    if (m_obsMain->observer())
        m_obsMain->observer()->OnReloadSceneItemList();
}

size_t ObsSceneItemList::Count()
{
    return m_items.size();
}

OBSSceneItem ObsSceneItemList::Get(int idx)
{
    if (idx <0 || idx >= m_items.size())
        return OBSSceneItem();
    return m_items[idx];
}

OBSSceneItem ObsSceneItemList::GetCurrentSceneItem()
{
    if (m_select <0 || m_select >= m_items.size())
        return OBSSceneItem();
    return m_items[m_select];
}

void ObsSceneItemList::OnSelect(obs_sceneitem_t *item, bool bSel)
{
    int index = -1;
    for (int i=0;i<m_items.size();++i)
    {
        if (m_items[i] == item)
        {
            index = i;
            break;
        }
    }

    if (!bSel)
    {
        if (index != m_select)
            return;
        index = -1;
    }

    if (index != m_select)
    {
        m_select = index;

        if (m_obsMain->observer())
            m_obsMain->observer()->OnSceneItemSelectChanged(m_select);
    }

}

void ObsSceneItemList::MoveUp(int idx)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_sceneitem_set_order(item, OBS_ORDER_MOVE_UP);
        if (idx == m_select && idx > 0)
            m_select = m_select - 1;
    }

}

void ObsSceneItemList::MoveDown(int idx)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_sceneitem_set_order(item, OBS_ORDER_MOVE_DOWN);
        if (idx == m_select && idx < m_items.size() -1)
            m_select = m_select + 1;
    }
}


void ObsSceneItemList::MoveToTop(int idx)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_sceneitem_set_order(item, OBS_ORDER_MOVE_TOP);
        if (idx == m_select)
            m_select = 0;
    }

}

void ObsSceneItemList::MoveToBottom(int idx)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_sceneitem_set_order(item, OBS_ORDER_MOVE_BOTTOM);
        if (idx == m_select)
            m_select = m_items.size() - 1;
    }
}

void ObsSceneItemList::Remove(int idx)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_sceneitem_remove(item);
        OnRemove(item);
        if (idx == m_select)
        {
            m_select = -1;
        }
    }
}

void ObsSceneItemList::SetVisible(int idx, bool visible)
{
    OBSSceneItem item = Get(idx);
    if (item)
        obs_sceneitem_set_visible(item, visible);
}

void ObsSceneItemList::SetLocked(int idx, bool locked)
{
    OBSSceneItem item = Get(idx);
    if (item)
        obs_sceneitem_set_locked(item, locked);
}

void ObsSceneItemList::SetName(int idx, const char* name)
{
    OBSSceneItem item = Get(idx);
    if (item)
    {
        obs_source_t *source = obs_sceneitem_get_source(item);
        obs_source_set_name(source, name);
    }
}

void ObsSceneItemList::Select(int idx)
{

    for (int i = 0; i < m_items.size(); ++i)
    {
        obs_sceneitem_select(m_items[i], idx == i);
    }
}

const char* ObsSceneItemList::itemName(int idx)
{
    OBSSceneItem item = Get(idx);
    if (!item)
        return "";
    obs_source_t *source = obs_sceneitem_get_source(item);
    return obs_source_get_name(source);
}

bool ObsSceneItemList::itemVisible(int idx)
{
    OBSSceneItem item = Get(idx);
    if (!item)
        return false;
    return obs_sceneitem_visible(item);
}

const char* ObsSceneItemList::itemName(OBSSceneItem item)
{
    obs_source_t *source = obs_sceneitem_get_source(item);
    return obs_source_get_name(source);
}

bool ObsSceneItemList::itemVisible(OBSSceneItem item)
{
    return obs_sceneitem_visible(item);
}

bool ObsSceneItemList::itemLocked(OBSSceneItem item)
{
    return obs_sceneitem_locked(item);
}

bool ObsSceneItemList::itemSelected(OBSSceneItem item)
{
    return obs_sceneitem_selected(item);
}
