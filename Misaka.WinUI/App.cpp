#include "pch.h"
#include "App.h"
#include "App.g.cpp"
#include "MainPage.h"

namespace winrt::Misaka::WinUI::implementation
{
    App::App()
    {
        Initialize();
    }
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nShowCmd)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    winrt::init_apartment(winrt::apartment_type::single_threaded);

    winrt::Misaka::WinUI::App app = winrt::make<winrt::Misaka::WinUI::implementation::App>();
    winrt::Misaka::WinUI::MainPage mainPage = winrt::make<winrt::Misaka::WinUI::implementation::MainPage>();

    HWND mainWindow = CreateWindowExW(WS_EX_CLIENTEDGE, L"Mile.Xaml.ContentWindow", nullptr, WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, winrt::get_abi(mainPage));
    if (!mainWindow)
    {
        return -1;
    }
    ShowWindow(mainWindow, nShowCmd);
    UpdateWindow(mainWindow);

    MSG msg;
    while (GetMessageW(&msg, nullptr, 0, 0))
    {
        if (msg.message == WM_SYSKEYDOWN && msg.wParam == VK_F4)
        {
            SendMessageW(GetAncestor(msg.hwnd, GA_ROOT), msg.message, msg.wParam, msg.lParam);
            continue;
        }
        TranslateMessage(&msg);
        DispatchMessageW(&msg);
    }

    app.Close();
    return static_cast<int>(msg.wParam);
}