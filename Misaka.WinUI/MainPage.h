#pragma once

#include "MainPage.g.h"

namespace winrt::Misaka::WinUI::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
    public:
        MainPage();
    };
}

namespace winrt::Misaka::WinUI::factory_implementation
{
    struct MainPage : MainPageT<MainPage, implementation::MainPage> {};
}
