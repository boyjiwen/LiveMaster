#pragma once
#include <obs.hpp>
#include <memory>
#include <vector>
#include <graphics/vec2.h>
#include <graphics/matrix4.h>
#include "ObsWindowBase.h"

//obs渲染窗口
class ObsWindow :public ObsWindowBase
{
public:
    ObsWindow();
    ~ObsWindow();

    static ObsWindow*  Create();
    bool CreateDisplay();//创建obsdisplay

    obs_display_t* display() { return m_display; }

    void SetEnable(bool enable) { obs_display_set_enabled(m_display,enable); }
    bool IsEnable() { return obs_display_enabled(m_display); }

    gs_vertbuffer_t *box = nullptr;
    gs_vertbuffer_t *boxLeft = nullptr;
    gs_vertbuffer_t *boxTop = nullptr;
    gs_vertbuffer_t *boxRight = nullptr;
    gs_vertbuffer_t *boxBottom = nullptr;

    void ResetVideo();
protected:
    static void _RenderWindow(void* param,uint32_t cx, uint32_t cy);
    void RenderWindow(uint32_t cx, uint32_t cy);
    void OnResize(const ObsSize& size) override;

    void OnMousePressEvent(ObsMouseEvent *event) override;
    void OnMouseReleaseEvent(ObsMouseEvent *event) override;
    void OnMouseMoveEvent(ObsMouseEvent *event) override;
    void OnFocusChange(bool bGet) override;
    void OnDropFile(const char* file) override;
    void GetStretchHandleData(const vec2 &pos);

    void InitPrimitives();
    void DrawBackdrop(float cx, float cy);
    void DrawSceneEditing();

    //是否锁定
    bool m_locked = false;
    OBSDisplay m_display;

    float m_previewScale = 1.0;
    int m_previewCX = 0;
    int m_previewCY = 0;
    int m_previewX = 0;
    int m_previewY = 0;

    vec2 GetMouseEventPos(ObsMouseEvent *event);
    static bool SelectedAtPos(const vec2 &pos);
    void DoSelect(const vec2 &pos);
    void DoCtrlSelect(const vec2 &pos);

    OBSSceneItem GetItemAtPos(const vec2 &pos, bool selectBelow);
    OBSSceneItem GetSelectItemAtPos(const vec2 &pos);

    void ProcessClick(const vec2 &pos) {
        DoSelect(pos);
    }

    vec3 CalculateStretchPos(const vec3 &tl, const vec3 &br);
    void ClampAspect(vec3 &tl, vec3 &br, vec2 &size,
        const vec2 &baseSize);

    vec3 GetSnapOffset(const vec3 &tl, const vec3 &br);
    void SnapItemMovement(vec2 &offset);
    void MoveItems(const vec2 &pos);
    void CropItem(const vec2 &pos);
    void StretchItem(const vec2 &pos);

    //鼠标事件
    obs_sceneitem_crop startCrop;
    vec2         startItemPos;
    vec2         cropSize;
    OBSSceneItem stretchGroup;
    OBSSceneItem stretchItem;
    ItemHandle   stretchHandle = ItemHandle::None;
    vec2         stretchItemSize;
    matrix4      screenToItem;
    matrix4      itemToScreen;
    matrix4      invGroupTransform;

    ObsSize   size;

    vec2         startPos;
    vec2         lastMoveOffset;
    bool         cropping = false;
    bool         mouseDown = false;
    bool         rmouseDown = false;
    bool         mouseMoved = false;
    bool         mouseOverItems = false;
};


