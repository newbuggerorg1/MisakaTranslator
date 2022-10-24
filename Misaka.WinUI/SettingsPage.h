#pragma once

#include "SettingsPage.g.h"

namespace winrt::Misaka::WinUI::implementation
{
    struct SettingsPage : SettingsPageT<SettingsPage>
    {
    public:
        SettingsPage();
        void NavView_SelectionChanged(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args);
    };
}

namespace winrt::Misaka::WinUI::factory_implementation
{
    struct SettingsPage : SettingsPageT<SettingsPage, implementation::SettingsPage> {};
}
