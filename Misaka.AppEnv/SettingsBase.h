#pragma once

#include "SettingsBase.g.h"

namespace winrt::Misaka::AppEnv::implementation
{
    class SettingsBase : public SettingsBaseT<SettingsBase>
    {
    private:
        hstring categoryName;
        static Windows::Storage::StorageFile const& SavedFile();
        static Windows::Data::Json::JsonObject const& ParsedJson();
    protected:
        Windows::Data::Json::JsonObject settings;
        Windows::Data::Json::JsonValue GetValue(hstring const& name);
        bool GetValue(hstring const& name, bool const& defaultInput);
        double GetValue(hstring const& name, double const& defaultInput);
        hstring GetValue(hstring const& name, hstring const& defaultInput);
        void SetValue(hstring const& name);
        void SetValue(hstring const& name, bool const& input);
        void SetValue(hstring const& name, double const& input);
        void SetValue(hstring const& name, hstring const& input);
        void SetValue(hstring const& name, Windows::Data::Json::IJsonValue const& input);
    public:
        SettingsBase(hstring const& category);
        void ClearValue(hstring const& name);
        void Save();
    };
}

namespace winrt::Misaka::AppEnv::factory_implementation
{
    class SettingsBase : public SettingsBaseT<SettingsBase, implementation::SettingsBase> {};
}
