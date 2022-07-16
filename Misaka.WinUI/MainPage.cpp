#include "pch.h"
#include "MainPage.h"
#include "MainPage.g.cpp"

using namespace winrt;

namespace winrt::Misaka::WinUI::implementation
{
    MainPage::MainPage()
    {
        InitializeComponent();
    }

    void MainPage::NavView_SelectionChanged(Windows::Foundation::IInspectable const&, Microsoft::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args)
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
