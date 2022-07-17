#include "Percompilation.h"
#include "SettingsCodepage.h"
#if __has_include("SettingsCodepage.g.cpp")
#include "SettingsCodepage.g.cpp"
#endif

namespace winrt::Misaka::AppEnv::implementation
{
    SettingsCodepage::SettingsCodepage() : base_type(L"CodePage") { ; }

    hstring SettingsCodepage::LocaleEmulatorPath()
    {
        return this->GetValue(L"LocaleEmulatorPath", hstring());
    }

    void SettingsCodepage::LocaleEmulatorPath(hstring const& value)
    {
        this->SetValue(L"LocaleEmulatorPath", value);
    }
}
