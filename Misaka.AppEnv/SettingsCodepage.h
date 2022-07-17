#pragma once

#include "SettingsCodepage.g.h"
#include "SettingsBase.h"

namespace winrt::Misaka::AppEnv::implementation
{
    struct SettingsCodepage : SettingsCodepageT<SettingsCodepage, implementation::SettingsBase>
    {
    public:
        SettingsCodepage();
        hstring LocaleEmulatorPath();
        void LocaleEmulatorPath(hstring value);
    };
}

namespace winrt::Misaka::AppEnv::factory_implementation
{
    struct SettingsCodepage : SettingsCodepageT<SettingsCodepage, implementation::SettingsCodepage> {};
}
