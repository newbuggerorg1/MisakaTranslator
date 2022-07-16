#pragma once

#include "AboutPage.g.h"

namespace winrt::Misaka::WinUI::implementation
{
    struct AboutPage : AboutPageT<AboutPage>
    {
    public:
        AboutPage();
        void AppBarButton_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const&);
        void CheckUpdateButton_Click(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::RoutedEventArgs const&);
        void MenuFlyoutItem_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const&);
    };
}

namespace winrt::Misaka::WinUI::factory_implementation
{
    struct AboutPage : AboutPageT<AboutPage, implementation::AboutPage> {};
}
