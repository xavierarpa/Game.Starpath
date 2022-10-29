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
        public const string common_lang = "English";
        public const string setup_title = "Create your star for the constellation";
        public const string setup_continue = "Continue to";
        public const string setup_input_name_placeholder = "Name the star"; 
        public const string loading_message = "Loading...";
        public const string setup_input_message_placeholder = "Write a message for the star";
        public const string text_create = "Press space to create the star";
        public const string text_create_not = "You cannot create the star here";
    }
}
