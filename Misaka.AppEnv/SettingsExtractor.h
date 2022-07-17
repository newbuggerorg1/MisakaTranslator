#pragma once

#include "SettingsExtractor.g.h"
#include "SettingsBase.h"

namespace winrt::Misaka::AppEnv::implementation
{
    struct SettingsExtractor : SettingsExtractorT<SettingsExtractor, SettingsBase>
    {
    public:
        SettingsExtractor();
        bool AutoHook();
        void AutoHook(bool const& value);
        bool AutoDetach();
        void AutoDetach(bool const& value);
        hstring TextractorPath();
        void TextractorPath(hstring const& value);
    };
}

namespace winrt::Misaka::AppEnv::factory_implementation
{
    struct SettingsExtractor : SettingsExtractorT<SettingsExtractor, implementation::SettingsExtractor> {};
}
