#include "Percompilation.h"
#include "SettingsExtractor.h"
#if __has_include("SettingsExtractor.g.cpp")
#include "SettingsExtractor.g.cpp"
#endif

namespace winrt::Misaka::AppEnv::implementation
{
    SettingsExtractor::SettingsExtractor() : base_type(L"Extractor") { ; }

    bool SettingsExtractor::AutoHook()
    {
        return this->GetValue(L"AutoHook", false);
    }

    void SettingsExtractor::AutoHook(bool const& value)
    {
        this->SetValue(L"AutoHook", value);
    }

    bool SettingsExtractor::AutoDetach()
    {
        return this->GetValue(L"AutoDetach", true);
    }

    void SettingsExtractor::AutoDetach(bool const& value)
    {
        this->SetValue(L"AutoDetach", value);
    }

    hstring SettingsExtractor::TextractorPath()
    {
        return this->GetValue(L"TextractorPath", hstring());
    }

    void SettingsExtractor::TextractorPath(hstring const& value)
    {
        this->SetValue(L"TextractorPath", value);
    }
}
