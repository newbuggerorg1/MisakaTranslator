#pragma once

#include "MainPage.g.h"

namespace winrt::Misaka::WinUI::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
    public:
        MainPage();
        void NavView_SelectionChanged(Windows::Foundation::IInspectable const&, Microsoft::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args);
    };
}

namespace winrt::Misaka::WinUI::factory_implementation
{
    struct MainPage : MainPageT<MainPage, implementation::MainPage> {};
}
