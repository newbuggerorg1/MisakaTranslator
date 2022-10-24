#pragma once

#include "AboutPage.g.h"

namespace winrt::Misaka::WinUI::implementation
{
    struct AboutPage : AboutPageT<AboutPage>
    {
    public:
        AboutPage();
        void OpenLink_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& e);
    };
}

namespace winrt::Misaka::WinUI::factory_implementation
{
    struct AboutPage : AboutPageT<AboutPage, implementation::AboutPage> {};
}
