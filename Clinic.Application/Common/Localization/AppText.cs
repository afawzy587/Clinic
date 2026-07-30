using System.Globalization;

namespace Clinic.Application.Common.Localization;

public static partial class AppText
{
    private static bool IsArabic => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("ar", StringComparison.OrdinalIgnoreCase);

    public static string CreatedSuccessfully => Get("CreatedSuccessfully");

    public static string UpdatedSuccessfully => Get("UpdatedSuccessfully");

    public static string DeletedSuccessfully => Get("DeletedSuccessfully");

    public static string BadRequest => Get("BadRequest");

    public static string NotFound => Get("NotFound");

    public static string Unauthenticated => Get("Unauthenticated");

    public static string Unauthorized => Get("Unauthorized");

    public static string InternalServerError => Get("InternalServerError");

    public static string ValidationFailed => Get("ValidationFailed");

    public static string UnexpectedError => Get("UnexpectedError");

    public static string PatientRetrievedSuccessfully => Get("PatientRetrievedSuccessfully");

    public static string PatientWithIdNotFound(int id) => Format("PatientWithIdNotFound", id);

    public static string FirstNameRequired => Get("FirstNameRequired");

    public static string FirstName => Get("FirstName");
    public static string FirstNameInvalid => Get("FirstNameInvalid");

    public static string FirstNameMaxLength => Get("FirstNameMaxLength");

    public static string LastNameRequired => Get("LastNameRequired");

    public static string LastNameInvalid => Get("LastNameInvalid");

    public static string LastNameMaxLength => Get("LastNameMaxLength");

    public static string NameAlreadyExists => Get("NameAlreadyExists");

    public static string PhoneRequired => Get("PhoneRequired");

    public static string PhoneValid => Get("PhoneValid");

    public static string PhoneValidLength => Get("PhoneValidLength");

    public static string DateOfBirthPast => Get("DateOfBirthPast");

    private static string Get(string key)
    {
        var translations = IsArabic
            ? ArabicTranslations
            : EnglishTranslations;

        return translations.TryGetValue(key, out var translation)
            ? translation
            : key;
    }

    private static string Format(string key, params object[] args)
    {
        var template = Get(key);
        return string.Format(CultureInfo.CurrentCulture, template, args);
    }
}
