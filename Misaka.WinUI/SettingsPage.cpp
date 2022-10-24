#include "pch.h"
#include "SettingsPage.h"
#if __has_include("SettingsPage.g.cpp")
#include "SettingsPage.g.cpp"
#endif
#include <winrt/Misaka.AppEnv.h>

namespace winrt::Misaka::WinUI::implementation
{
    SettingsPage::SettingsPage()
    {
        InitializeComponent();
    }

    void SettingsPage::NavViewItem_PointerEntered(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::Input::PointerRoutedEventArgs const&)
    {
        NavView().IsPaneOpen(true);
    }

    void SettingsPage::NavView_SelectionChanged(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args)
    {
        if (args.SelectedItemContainer())
        {
            Windows::UI::Xaml::Interop::TypeName pageTypeName;
            pageTypeName.Name = std::format(L"Misaka.WinUI.{0}", unbox_value<hstring>(args.SelectedItemContainer().Tag()));
            pageTypeName.Kind = Windows::UI::Xaml::Interop::TypeKind::Metadata;
            Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionInfo pageTransitions;
            pageTransitions.Effect(Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionEffect::FromBottom);
            ContentFrame().Navigate(pageTypeName, nullptr, pageTransitions);
        }
    }
}
