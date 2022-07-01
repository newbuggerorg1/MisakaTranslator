#include "Percompilation.h"
#include "SettingsGeneral.h"
#if __has_include("SettingsGeneral.g.cpp")
#include "SettingsGeneral.g.cpp"
#endif

namespace winrt::Misaka::AppEnv::implementation
{
    SettingsGeneral::SettingsGeneral() : base_type(L"General") { ; }

    bool SettingsGeneral::MinimizeToNotificationArea()
    {
        if (PackageInfo::RunWithId()) { return this->GetValue(L"MinimizeToNotificationArea", true); }
        else { return this->GetValue(L"MinimizeToNotificationArea", false); }
    }

    void SettingsGeneral::MinimizeToNotificationArea(bool value)
    {
        this->SetValue(L"MinimizeToNotificationArea", value);
    }

    bool SettingsGeneral::PushNotification()
    {
        if (PackageInfo::RunWithId()) { return this->GetValue(L"PushNotification", true); }
        else { return this->GetValue(L"PushNotification", false); }
    }

    void SettingsGeneral::PushNotification(bool value)
    {
        this->SetValue(L"PushNotification", value);
    }
}
