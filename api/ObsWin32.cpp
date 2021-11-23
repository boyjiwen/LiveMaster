#include "ObsWin32.h"
#include "util/utf8.h"
#include <assert.h>

#include <WindowsX.h>
#include <tchar.h>


#define ASSERT assert

//#define SubclassWindow(hwnd, lpfn)       \
//	((WNDPROC)SetWindowLongPtr((hwnd), GWLP_WNDPROC, (LPARAM)(WNDPROC)(lpfn)))

/////////////////////////////////////////////////////////////////////////////////////
//
//

HINSTANCE ObsWin32Window::m_instance = NULL;

ObsWin32Window::ObsWin32Window() : m_hWnd(NULL), m_OldWndProc(::DefWindowProc), m_bSubclassed(false)
{
}

ObsWin32Window::~ObsWin32Window()
{

}

HWND ObsWin32Window::GetHWND() const
{
    return m_hWnd;
}

UINT ObsWin32Window::GetClassStyle() const
{
    return 0;
}

LPCTSTR ObsWin32Window::GetSuperClassName() const
{
    return NULL;
}

ObsWin32Window::operator HWND() const
{
    return m_hWnd;
}

HWND ObsWin32Window::Create(HWND hwndParent, LPCTSTR pstrName, DWORD dwStyle, DWORD dwExStyle, const RECT rc, HMENU hMenu)
{
    return Create(hwndParent, pstrName, dwStyle, dwExStyle, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, hMenu);
}

HWND ObsWin32Window::Create(HWND hwndParent, LPCTSTR pstrName, DWORD dwStyle, DWORD dwExStyle, int x, int y, int cx, int cy, HMENU hMenu)
{
    if (GetSuperClassName() != NULL && !RegisterSuperclass()) return NULL;
    if (GetSuperClassName() == NULL && !RegisterWindowClass()) return NULL;
    m_hWnd = ::CreateWindowEx(dwExStyle, GetWindowClassName(), pstrName, dwStyle, x, y, cx, cy, hwndParent, hMenu, m_instance, this);
    InitWindow();//初始化窗口
    ASSERT(m_hWnd != NULL);

    DragAcceptFiles(m_hWnd, TRUE);
    return m_hWnd;
}

HWND ObsWin32Window::Subclass(HWND hWnd)
{
    ASSERT(::IsWindow(hWnd));
    ASSERT(m_hWnd == NULL);
    m_OldWndProc = SubclassWindow(hWnd, __WndProc);
    if (m_OldWndProc == NULL) return NULL;
    m_bSubclassed = true;
    m_hWnd = hWnd;
    return m_hWnd;
}

void ObsWin32Window::Unsubclass()
{
    ASSERT(::IsWindow(m_hWnd));
    if (!::IsWindow(m_hWnd)) return;
    if (!m_bSubclassed) return;
    SubclassWindow(m_hWnd, m_OldWndProc);
    m_OldWndProc = ::DefWindowProc;
    m_bSubclassed = false;
}

void ObsWin32Window::ShowWindow(bool bShow /*= true*/, bool bTakeFocus /*= false*/)
{
    ASSERT(::IsWindow(m_hWnd));
    if (!::IsWindow(m_hWnd)) return;
    ::ShowWindow(m_hWnd, bShow ? (bTakeFocus ? SW_SHOWNORMAL : SW_SHOWNOACTIVATE) : SW_HIDE);
}


void ObsWin32Window::Close()
{
    ASSERT(::IsWindow(m_hWnd));
    if (!::IsWindow(m_hWnd)) return;
    PostMessage(WM_CLOSE);
}

void ObsWin32Window::CenterWindow()
{
    ASSERT(::IsWindow(m_hWnd));
    ASSERT((GetWindowStyle(m_hWnd)&WS_CHILD) == 0);
    RECT rcDlg = { 0 };
    ::GetWindowRect(m_hWnd, &rcDlg);
    RECT rcArea = { 0 };
    RECT rcCenter = { 0 };
    //HWND hWndParent = ::GetParent(m_hWnd);
    HWND hWndCenter = ::GetWindowOwner(m_hWnd);
    ::SystemParametersInfo(SPI_GETWORKAREA, NULL, &rcArea, NULL);
    if (hWndCenter == NULL) rcCenter = rcArea; else ::GetWindowRect(hWndCenter, &rcCenter);

    int DlgWidth = rcDlg.right - rcDlg.left;
    int DlgHeight = rcDlg.bottom - rcDlg.top;

    // Find dialog's upper left based on rcCenter
    int xLeft = (rcCenter.left + rcCenter.right) / 2 - DlgWidth / 2;
    int yTop = (rcCenter.top + rcCenter.bottom) / 2 - DlgHeight / 2;

    // The dialog is outside the screen, move it inside
    if (xLeft < rcArea.left) xLeft = rcArea.left;
    else if (xLeft + DlgWidth > rcArea.right) xLeft = rcArea.right - DlgWidth;
    if (yTop < rcArea.top) yTop = rcArea.top;
    else if (yTop + DlgHeight > rcArea.bottom) yTop = rcArea.bottom - DlgHeight;
    ::SetWindowPos(m_hWnd, NULL, xLeft, yTop, -1, -1, SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
}


bool ObsWin32Window::RegisterWindowClass()
{
    WNDCLASS wc = { 0 };
    wc.style = GetClassStyle();
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hIcon = NULL;
    wc.lpfnWndProc = ObsWin32Window::__WndProc;
    wc.hInstance = m_instance;
    wc.hCursor = ::LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = NULL;
    wc.lpszMenuName = NULL;
    wc.lpszClassName = GetWindowClassName();
    ATOM ret = ::RegisterClass(&wc);
    ASSERT(ret != NULL || ::GetLastError() == ERROR_CLASS_ALREADY_EXISTS);
    return ret != NULL || ::GetLastError() == ERROR_CLASS_ALREADY_EXISTS;
}

bool ObsWin32Window::RegisterSuperclass()
{
    // Get the class information from an existing
    // window so we can subclass it later on...
    WNDCLASSEX wc = { 0 };
    wc.cbSize = sizeof(WNDCLASSEX);
    if (!::GetClassInfoEx(NULL, GetSuperClassName(), &wc)) {
        if (!::GetClassInfoEx(m_instance, GetSuperClassName(), &wc)) {
            ASSERT(!"Unable to locate window class");
            return NULL;
        }
    }
    m_OldWndProc = wc.lpfnWndProc;
    wc.lpfnWndProc = ObsWin32Window::__ControlProc;
    wc.hInstance = m_instance;
    wc.lpszClassName = GetWindowClassName();
    ATOM ret = ::RegisterClassEx(&wc);
    ASSERT(ret != NULL || ::GetLastError() == ERROR_CLASS_ALREADY_EXISTS);
    return ret != NULL || ::GetLastError() == ERROR_CLASS_ALREADY_EXISTS;
}

LRESULT CALLBACK ObsWin32Window::__WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    ObsWin32Window* pThis = NULL;
    if (uMsg == WM_NCCREATE) {
        LPCREATESTRUCT lpcs = reinterpret_cast<LPCREATESTRUCT>(lParam);
        pThis = static_cast<ObsWin32Window*>(lpcs->lpCreateParams);
        pThis->m_hWnd = hWnd;
        ::SetWindowLongPtr(hWnd, GWLP_USERDATA, reinterpret_cast<LPARAM>(pThis));
    }
    else {
        pThis = reinterpret_cast<ObsWin32Window*>(::GetWindowLongPtr(hWnd, GWLP_USERDATA));
        if (uMsg == WM_NCDESTROY && pThis != NULL) {
            LRESULT lRes = ::CallWindowProc(pThis->m_OldWndProc, hWnd, uMsg, wParam, lParam);
            ::SetWindowLongPtr(pThis->m_hWnd, GWLP_USERDATA, 0L);
            if (pThis->m_bSubclassed) pThis->Unsubclass();
            pThis->m_hWnd = NULL;
            pThis->OnFinalMessage(hWnd);
            return lRes;
        }
    }
    if (pThis != NULL) {
        return pThis->HandleMessage(uMsg, wParam, lParam);
    }
    else {
        return ::DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

LRESULT CALLBACK ObsWin32Window::__ControlProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    ObsWin32Window* pThis = NULL;
    if (uMsg == WM_NCCREATE) {
        LPCREATESTRUCT lpcs = reinterpret_cast<LPCREATESTRUCT>(lParam);
        pThis = static_cast<ObsWin32Window*>(lpcs->lpCreateParams);
        ::SetProp(hWnd, _T("WndX"), (HANDLE)pThis);
        pThis->m_hWnd = hWnd;
    }
    else {
        pThis = reinterpret_cast<ObsWin32Window*>(::GetProp(hWnd, _T("WndX")));
        if (uMsg == WM_NCDESTROY && pThis != NULL) {
            LRESULT lRes = ::CallWindowProc(pThis->m_OldWndProc, hWnd, uMsg, wParam, lParam);
            if (pThis->m_bSubclassed) pThis->Unsubclass();
            ::SetProp(hWnd, _T("WndX"), NULL);
            pThis->m_hWnd = NULL;
            pThis->OnFinalMessage(hWnd);
            return lRes;
        }
    }
    if (pThis != NULL) {
        return pThis->HandleMessage(uMsg, wParam, lParam);
    }
    else {
        return ::DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

LRESULT ObsWin32Window::SendMessage(UINT uMsg, WPARAM wParam /*= 0*/, LPARAM lParam /*= 0*/)
{
    ASSERT(::IsWindow(m_hWnd));
    return ::SendMessage(m_hWnd, uMsg, wParam, lParam);
}

LRESULT ObsWin32Window::PostMessage(UINT uMsg, WPARAM wParam /*= 0*/, LPARAM lParam /*= 0*/)
{
    ASSERT(::IsWindow(m_hWnd));
    return ::PostMessage(m_hWnd, uMsg, wParam, lParam);
}

void ObsWin32Window::ResizeClient(int cx /*= -1*/, int cy /*= -1*/)
{
    ASSERT(::IsWindow(m_hWnd));
    RECT rc = { 0 };;
    if (!::GetClientRect(m_hWnd, &rc)) return;
    if (cx != -1) rc.right = cx;
    if (cy != -1) rc.bottom = cy;
    if (!::AdjustWindowRectEx(&rc, GetWindowStyle(m_hWnd), (!(GetWindowStyle(m_hWnd) & WS_CHILD) && (::GetMenu(m_hWnd) != NULL)), GetWindowExStyle(m_hWnd))) return;
    UINT uFlags = SWP_NOZORDER | SWP_NOMOVE;
    ::SetWindowPos(m_hWnd, NULL, 0, 0, rc.right - rc.left, rc.bottom - rc.top, uFlags);
}

void ObsWin32Window::OnFinalMessage(HWND /*hWnd*/)
{
}

int ObsWin32Window::DoMessageLoop()
{
    MSG msg = { 0 };
    BOOL bRet;

    while (bRet = ::GetMessage(&msg, NULL, 0, 0) != 0)
    {
        if (bRet == -1)
        {
            break;
        }
        else
        {
            ::TranslateMessage(&msg);
            ::DispatchMessage(&msg);
        }
    }

    return (int)msg.wParam;
}



ObsWindow* ObsWindow::Create()
{
    return new ObsWin32Window();
}


ObsSize ObsWin32Window::GetClientSize()
{
    if (m_hWnd)
    {
        RECT rc;
        if(GetClientRect(m_hWnd, &rc))
            return ObsSize(rc.right - rc.left, rc.bottom - rc.top);
    }
    return ObsSize();
}

window_handle_t ObsWin32Window::CreateWnd(window_handle_t parent, const ObsRect& pos)
{
    DWORD style = WS_OVERLAPPEDWINDOW | WS_VISIBLE;
    if (parent)
    {
        style = WS_CHILDWINDOW | WS_VISIBLE;
    }

    RECT rc = { pos.x,pos.y,pos.x + pos.width,pos.y + pos.height };
    return Create((HWND)parent, _T("ObsRenderWindow"), style, 0, rc);
}


void ObsWin32Window::SetWindowPos(const ObsRect& rc)
{
    if (m_hWnd)
    {
        ::MoveWindow(m_hWnd,rc.x,rc.y,rc.width,rc.height,FALSE);
    }
}

window_handle_t ObsWin32Window::GetWndHandle()
{
    return m_hWnd;
}

#define  IsKeyPressed(nVirtKey)     ((GetKeyState(nVirtKey) & (1<<(sizeof(SHORT)*8-1))) != 0)

bool ObsWin32Window::CheckKeyState(unsigned int state)
{
    if (state == 0)
        return false;

    if ((state & StateShift)!=0 && !IsKeyPressed(VK_SHIFT))
    {
        return false;
    }

    if ((state & StateControl) != 0 && !IsKeyPressed(VK_CONTROL))
    {
        return false;
    }

    if ((state & StateAlt) != 0 && !IsKeyPressed(VK_MENU))
    {
        return false;
    }
    return true;
}

void ObsWin32Window::SetVisible(bool visible)
{
    ShowWindow(visible);
}

LRESULT ObsWin32Window::HandleMessage(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
    case WM_CLOSE:
        PostQuitMessage(0);
        break;
    case WM_SIZE:
        {
            ObsSize size(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
            OnResize(size);
        }
        break;
    case WM_MOUSEMOVE:
        {
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = NoButton;
            OnMouseMoveEvent(&event);
        }
        break;
    case WM_LBUTTONDOWN:
        {
            SetCapture();
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = LeftButton;
            OnMousePressEvent(&event);
        }
        break;
    case WM_RBUTTONDOWN:
        {
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = RightButton;
            OnMousePressEvent(&event);
        }
        break;
    case WM_MBUTTONDOWN:
        {
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = MiddleButton;
            OnMousePressEvent(&event);
        }
        break;
    case WM_LBUTTONUP:
        {
            ReleaseCapture();
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = LeftButton;
            OnMouseReleaseEvent(&event);
        }
        break;
    case WM_RBUTTONUP:
        {
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = RightButton;
            OnMouseReleaseEvent(&event);
        }
        break;
    case WM_MBUTTONUP:
        {
            ObsMouseEvent event;
            event.x = GET_X_LPARAM(lParam);
            event.y = GET_Y_LPARAM(lParam);
            event.button = MiddleButton;
            OnMouseReleaseEvent(&event);
        }
        break;
    case WM_SETFOCUS:
        OnFocusChange(true);
        break;
    case WM_KILLFOCUS:
        OnFocusChange(false);
        break;
    case WM_DROPFILES:
        {
            HDROP hdrop = (HDROP)wParam;
            TCHAR szPath[MAX_PATH];
            CHAR szPathA[MAX_PATH];
            UINT nCount = DragQueryFile(hdrop, 0xFFFFFFFF, NULL, 0);
            if (nCount)
            {
                for (UINT nIndex = 0; nIndex < nCount; ++nIndex)
                {
                    DragQueryFile(hdrop, nIndex, szPath, _countof(szPath));

                    //转为utf8
                    size_t len = wchar_to_utf8(szPath, 0, szPathA, sizeof(szPathA), 0);
                    szPathA[len] = '\0';
                    OnDropFile(szPathA);
                }
            }
            DragFinish(hdrop);
        }
        break;
    }

    return ::CallWindowProc(m_OldWndProc, m_hWnd, uMsg, wParam, lParam);
}


void ObsWin32Window::SetCapture()
{
    ::SetCapture(m_hWnd);
}

void ObsWin32Window::ReleaseCapture()
{
    ::ReleaseCapture();
}

