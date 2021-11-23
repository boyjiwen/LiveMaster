#pragma once


struct ObsSize {
    ObsSize() 
        :width(0),height(0)
    {
    }
    ObsSize(int w,int h)
        :width(w), height(h)
    {
    }
    int width;
    int height;
};

struct ObsPoint {
    ObsPoint()
        :x(0), y(0)
    {
    }
    ObsPoint(int _x, int _y)
        :x(_x), y(_y)
    {
    }
    int x;
    int y;
};

struct ObsRect {
    int x;
    int y;
    int width;
    int height;
};

typedef void* window_handle_t;


#define ITEM_LEFT   (1<<0)
#define ITEM_RIGHT  (1<<1)
#define ITEM_TOP    (1<<2)
#define ITEM_BOTTOM (1<<3)

enum class ItemHandle : uint32_t {
    None = 0,
    TopLeft = ITEM_TOP | ITEM_LEFT,
    TopCenter = ITEM_TOP,
    TopRight = ITEM_TOP | ITEM_RIGHT,
    CenterLeft = ITEM_LEFT,
    CenterRight = ITEM_RIGHT,
    BottomLeft = ITEM_BOTTOM | ITEM_LEFT,
    BottomCenter = ITEM_BOTTOM,
    BottomRight = ITEM_BOTTOM | ITEM_RIGHT
};

//鼠标按钮
enum MouseButton {
    NoButton = 0x00000000,
    LeftButton = 0x00000001,
    RightButton = 0x00000002,
    MiddleButton = 0x00000004
};

struct ObsMouseEvent
{
    MouseButton button;//鼠标按钮
    int x; int y;  //鼠标位置
};

//窗口api，不同系统不同实现
class ObsWindowBase
{
public:
    virtual ~ObsWindowBase() {}

    //获取窗口尺寸
    virtual ObsSize GetClientSize() =0;
    virtual window_handle_t CreateWnd(window_handle_t parent,
        const ObsRect& pos) =0;
    virtual void SetWindowPos(const ObsRect& rc) = 0;
    virtual window_handle_t GetWndHandle() = 0;
    virtual void SetVisible(bool visible) = 0;

protected:
    //窗口事件
    virtual void OnResize(const ObsSize& size) = 0;
    virtual void OnFocusChange(bool bGet) = 0;
    virtual void OnDropFile(const char* file) = 0;

    //鼠标事件
    virtual void OnMousePressEvent(ObsMouseEvent *event) {}
    virtual void OnMouseReleaseEvent(ObsMouseEvent *event) {}
    virtual void OnMouseMoveEvent(ObsMouseEvent *event) {}

    //按键状态
    enum KeyState{
        StateShift = 1,
        StateControl = 2,
        StateAlt = 4
    };
    virtual bool CheckKeyState(unsigned int state) = 0;
};

