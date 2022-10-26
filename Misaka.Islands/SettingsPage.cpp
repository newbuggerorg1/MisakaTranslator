#include "pch.h"
#include "SettingsPage.h"
#if __has_include("SettingsPage.g.cpp")
#include "SettingsPage.g.cpp"
#endif
#include <winrt/Misaka.AppEnv.h>

namespace winrt::Misaka::Islands::implementation
{
    SettingsPage::SettingsPage()
    {
        InitializeComponent();
    }

    void SettingsPage::NavView_SelectionChanged(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args)
    {
        if (args.SelectedItemContainer())
        {
            Windows::UI::Xaml::Interop::TypeName pageTypeName;
            hstring tag = unbox_value<hstring>(args.SelectedItemContainer().Tag());
            pageTypeName.Name = std::format(L"Misaka.Islands.{0}", tag);
            pageTypeName.Kind = Windows::UI::Xaml::Interop::TypeKind::Metadata;
            Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionInfo pageTransitions;
            pageTransitions.Effect(Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionEffect::FromBottom);
            ContentFrame().Navigate(pageTypeName, nullptr, pageTransitions);
        }
    }
}
