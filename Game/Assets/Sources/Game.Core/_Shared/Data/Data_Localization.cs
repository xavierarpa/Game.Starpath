using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data_Localization
{
    public const SystemLanguage DEFAULT_LANGUAGE = SystemLanguage.English;
    public static readonly Dictionary<SystemLanguage, string> DIC_LANGUAGES = new Dictionary<SystemLanguage, string>()
    {
        [SystemLanguage.English] = "en",
        [SystemLanguage.French] = "fr",
        [SystemLanguage.Spanish] = "es",
    };

    //public static string GET_LOCALE(this SystemLanguage systemLanguage) => DIC_LANGUAGES.TryGetValue(systemLanguage, out string val) ? val : DIC_LANGUAGES[DEFAULT_LANGUAGE];
    public static string GET_LOCALE(this SystemLanguage systemLanguage) => DIC_LANGUAGES[DEFAULT_LANGUAGE];
    public static string Path_Locale => $"{Data_Addressables.LocalizationTable_String}_{Application.systemLanguage.GET_LOCALE()}";

    public static class Key
    {
        //Translations
        public const string common_lang = "common_lang";
        public const string setup_title = "setup_title";
        public const string setup_continue = "setup_continue";
        public const string setup_input_name_placeholder = "setup_input_name_placeholder"; 
        public const string loading_message = "loading_message";
        public const string setup_input_message_placeholder = "setup_input_message_placeholder";
        public const string text_create = "text_create";
        public const string text_create_not = "text_create_not";
    }
}
