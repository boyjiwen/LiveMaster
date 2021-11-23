#pragma once
#include <Windows.h>
#include <tchar.h>
#include "ObsWindow.h"

//windows下窗口实现
class ObsWin32Window:public ObsWindow
{
public:
    static void SetInstance(HINSTANCE hins)
    {
        m_instance = hins;
    }

    ObsWin32Window();
    virtual ~ObsWin32Window();

    HWND GetHWND() const;
    operator HWND() const;

    bool RegisterWindowClass();
    bool RegisterSuperclass();

    HWND Create(HWND hwndParent, LPCTSTR pstrName, DWORD dwStyle, DWORD dwExStyle, const RECT rc,
        HMENU hMenu = NULL);

    HWND Create(HWND hwndParent, LPCTSTR pstrName, DWORD dwStyle, DWORD dwExStyle, int x = CW_USEDEFAULT,
        int y = CW_USEDEFAULT, int cx = CW_USEDEFAULT, int cy = CW_USEDEFAULT, HMENU hMenu = NULL);

    HWND Subclass(HWND hWnd);
    void Unsubclass();
    void ShowWindow(bool bShow = true, bool bTakeFocus = true);
    void Close();
    void CenterWindow();

    static int DoMessageLoop();

    //obs接口实现
    ObsSize GetClientSize() override;
    window_handle_t CreateWnd(window_handle_t parent, const ObsRect& pos) override;
    void SetWindowPos(const ObsRect& rc) override;
    window_handle_t GetWndHandle() override;
    bool CheckKeyState(unsigned int state) override;
    void SetVisible(bool visible) override;
protected:
    virtual LPCTSTR GetWindowClassName() const { return _T("ObsRenderWindow"); }
    virtual LPCTSTR GetSuperClassName() const;
    virtual UINT GetClassStyle() const;

    virtual void InitWindow() {}//初始化窗口

    LRESULT SendMessage(UINT uMsg, WPARAM wParam = 0, LPARAM lParam = 0L);
    LRESULT PostMessage(UINT uMsg, WPARAM wParam = 0, LPARAM lParam = 0L);
    void ResizeClient(int cx = -1, int cy = -1);

    virtual LRESULT HandleMessage(UINT uMsg, WPARAM wParam, LPARAM lParam);
    virtual void OnFinalMessage(HWND hWnd);

    static LRESULT CALLBACK __WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
    static LRESULT CALLBACK __ControlProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);


    void SetCapture();
    void ReleaseCapture();

protected:
    HWND m_hWnd;
    WNDPROC m_OldWndProc;
    bool m_bSubclassed;
    static HINSTANCE m_instance;
};


