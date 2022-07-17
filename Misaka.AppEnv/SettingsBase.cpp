#include "Percompilation.h"
#include "SettingsBase.h"
#include "SettingsBase.g.cpp"

namespace winrt::Misaka::AppEnv::implementation
{
    Windows::Storage::StorageFile const& SettingsBase::SavedFile()
    {
        static Windows::Storage::StorageFile cache = []()->Windows::Storage::StorageFile
        {
            Windows::Storage::StorageFolder folder = Windows::Storage::StorageFolder::GetFolderFromPathAsync(PackageInfo::LocalFolder()).get();
            return folder.CreateFileAsync(L"Settings.json", Windows::Storage::CreationCollisionOption::OpenIfExists).get();
        }();
        return cache;
    }

    Windows::Data::Json::JsonObject const& SettingsBase::ParsedJson()
    {
        static Windows::Data::Json::JsonObject cache = []()->Windows::Data::Json::JsonObject
        {
            try
            {
                hstring str = Windows::Storage::FileIO::ReadTextAsync(SavedFile()).get();
                return Windows::Data::Json::JsonObject::Parse(str);
            }
            catch (hresult_error const& ex)
            {
                return Windows::Data::Json::JsonObject();
                throw ex;
            }
        }();
        return cache;
    }

    SettingsBase::SettingsBase(hstring const& category)
    {
        try
        {
            categoryName = category;
            settings = ParsedJson().GetNamedObject(categoryName);
        }
        catch (hresult_error const&)
        {
            settings = Windows::Data::Json::JsonObject();
        }
    }

    void SettingsBase::ClearValue(hstring const& name)
    {
        settings.Remove(name);
    }

    Windows::Data::Json::JsonValue SettingsBase::GetValue(hstring const& name)
    {
        return settings.GetNamedValue(name, Windows::Data::Json::JsonValue::CreateNullValue());
    }

    bool SettingsBase::GetValue(hstring const& name, bool const& defaultInput)
    {
        return settings.GetNamedBoolean(name, defaultInput);
    }

    double SettingsBase::GetValue(hstring const& name, double const& defaultInput)
    {
        return settings.GetNamedNumber(name, defaultInput);
    }

    hstring SettingsBase::GetValue(hstring const& name, hstring const& defaultInput)
    {
        return settings.GetNamedString(name, defaultInput);
    }

    void SettingsBase::SetValue(hstring const& name)
    {
        settings.SetNamedValue(name, Windows::Data::Json::JsonValue::CreateNullValue());
    }

    void SettingsBase::SetValue(hstring const& name, bool const& input)
    {
        settings.SetNamedValue(name, Windows::Data::Json::JsonValue::CreateBooleanValue(input));
    }

    void SettingsBase::SetValue(hstring const& name, double const& input)
    {
        settings.SetNamedValue(name, Windows::Data::Json::JsonValue::CreateNumberValue(input));
    }

    void SettingsBase::SetValue(hstring const& name, hstring const& input)
    {
        settings.SetNamedValue(name, Windows::Data::Json::JsonValue::CreateStringValue(input));
    }

    void SettingsBase::SetValue(hstring const& name, Windows::Data::Json::IJsonValue const& input)
    {
        settings.SetNamedValue(name, input);
    }

    void SettingsBase::Save()
    {
        if (!ParsedJson().HasKey(categoryName))
        {
            ParsedJson().SetNamedValue(categoryName, settings);
        }
        hstring str = ParsedJson().Stringify();
        Windows::Storage::FileIO::WriteTextAsync(SavedFile(), str, Windows::Storage::Streams::UnicodeEncoding::Utf8).get();
    }
}
