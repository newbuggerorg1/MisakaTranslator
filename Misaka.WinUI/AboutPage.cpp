#include "pch.h"
#include "AboutPage.h"
#if __has_include("AboutPage.g.cpp")
#include "AboutPage.g.cpp"
#endif

namespace winrt::Misaka::WinUI::implementation
{
    AboutPage::AboutPage()
    {
        SetEnvironmentVariableW(L"WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", L"--enable-features=OverlayScrollbar,OverlayScrollbarWinStyle,OverlayScrollbarWinStyleAnimation");
        InitializeComponent();
    }

    void AboutPage::AppBarButton_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const&)
    {
        this->HtmlViewer().Source(Windows::Foundation::Uri(std::format(L"https://github.com/hanmin0822/MisakaTranslator/{0}", unbox_value<hstring>(unbox_value<Windows::UI::Xaml::Controls::AppBarButton>(sender).Tag()))));
    }

    void AboutPage::CheckUpdateButton_Click(Windows::Foundation::IInspectable const&, Windows::UI::Xaml::RoutedEventArgs const&)
    {
        CheckUpdateTip().IsOpen(true);
    }

    void AboutPage::MenuFlyoutItem_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const&)
    {
        Windows::Foundation::Uri uri(std::format(L"https://github.com/hanmin0822/MisakaTranslator/{0}", unbox_value<hstring>(unbox_value<Windows::UI::Xaml::Controls::MenuFlyoutItem>(sender).Tag())));
        Windows::System::Launcher::LaunchUriAsync(uri);
    }
}
