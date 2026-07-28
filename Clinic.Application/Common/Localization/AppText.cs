using System.Globalization;

namespace Clinic.Application.Common.Localization;

public static class AppText
{
    private static readonly IReadOnlyDictionary<string, (string English, string Arabic)> Translations =
        new Dictionary<string, (string English, string Arabic)>
        {
            ["CreatedSuccessfully"] = ("Created successfully.", "\u062A\u0645 \u0627\u0644\u0625\u0646\u0634\u0627\u0621 \u0628\u0646\u062C\u0627\u062D."),
            ["UpdatedSuccessfully"] = ("Updated successfully.", "\u062A\u0645 \u0627\u0644\u062A\u062D\u062F\u064A\u062B \u0628\u0646\u062C\u0627\u062D."),
            ["DeletedSuccessfully"] = ("Deleted successfully.", "\u062A\u0645 \u0627\u0644\u062D\u0630\u0641 \u0628\u0646\u062C\u0627\u062D."),
            ["BadRequest"] = ("Bad request.", "\u0637\u0644\u0628 \u063A\u064A\u0631 \u0635\u0627\u0644\u062D."),
            ["NotFound"] = ("Not found.", "\u063A\u064A\u0631 \u0645\u0648\u062C\u0648\u062F."),
            ["Unauthenticated"] = ("Unauthenticated.", "\u063A\u064A\u0631 \u0645\u0635\u0631\u062D."),
            ["Unauthorized"] = ("Unauthorized.", "\u063A\u064A\u0631 \u0645\u062E\u0648\u0644."),
            ["InternalServerError"] = ("Internal server error.", "\u062E\u0637\u0623 \u062F\u0627\u062E\u0644\u064A \u0641\u064A \u0627\u0644\u062E\u0627\u062F\u0645."),
            ["ValidationFailed"] = ("Validation failed.", "\u0641\u0634\u0644 \u0627\u0644\u062A\u062D\u0642\u0642 \u0645\u0646 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A."),
            ["UnexpectedError"] = ("An unexpected error occurred.", "\u062D\u062F\u062B \u062E\u0637\u0623 \u063A\u064A\u0631 \u0645\u062A\u0648\u0642\u0639."),
            ["PatientRetrievedSuccessfully"] = ("Patient retrieved successfully.", "\u062A\u0645 \u0627\u0633\u062A\u0631\u062C\u0627\u0639 \u0628\u064A\u0627\u0646\u0627\u062A \u0627\u0644\u0645\u0631\u064A\u0636 \u0628\u0646\u062C\u0627\u062D."),
            ["PatientWithIdNotFound"] = ("Patient with ID {0} was not found.", "\u0644\u0645 \u064A\u062A\u0645 \u0627\u0644\u0639\u062B\u0648\u0631 \u0639\u0644\u0649 \u0627\u0644\u0645\u0631\u064A\u0636 \u0628\u0627\u0644\u0645\u0639\u0631\u0641 {0}."),
            ["FirstNameRequired"] = ("First name is required.", "\u0627\u0644\u0627\u0633\u0645 \u0627\u0644\u0623\u0648\u0644 \u0645\u0637\u0644\u0648\u0628."),
            ["FirstNameMaxLength"] = ("First name cannot exceed 50 characters.", "\u0644\u0627 \u064A\u0645\u0643\u0646 \u0623\u0646 \u064A\u062A\u062C\u0627\u0648\u0632 \u0627\u0644\u0627\u0633\u0645 \u0627\u0644\u0623\u0648\u0644 50 \u062D\u0631\u0641\u064B\u0627."),
            ["LastNameRequired"] = ("Last name is required.", "\u0627\u0633\u0645 \u0627\u0644\u0639\u0627\u0626\u0644\u0629 \u0645\u0637\u0644\u0648\u0628."),
            ["LastNameMaxLength"] = ("Last name cannot exceed 50 characters.", "\u0644\u0627 \u064A\u0645\u0643\u0646 \u0623\u0646 \u064A\u062A\u062C\u0627\u0648\u0632 \u0627\u0633\u0645 \u0627\u0644\u0639\u0627\u0626\u0644\u0629 50 \u062D\u0631\u0641\u064B\u0627."),
            ["PhoneRequired"] = ("Phone number is required.", "\u0631\u0642\u0645 \u0627\u0644\u0647\u0627\u062A\u0641 \u0645\u0637\u0644\u0648\u0628."),
            ["PhoneValid"] = ("Phone number must be valid.", "\u064A\u062C\u0628 \u0623\u0646 \u064A\u0643\u0648\u0646 \u0631\u0642\u0645 \u0627\u0644\u0647\u0627\u062A\u0641 \u0635\u0627\u0644\u062D\u064B\u0627."),
            ["PhoneValidLength"] = ("Phone number must be valid and contain 10 to 15 digits.", "\u064A\u062C\u0628 \u0623\u0646 \u064A\u0643\u0648\u0646 \u0631\u0642\u0645 \u0627\u0644\u0647\u0627\u062A\u0641 \u0635\u0627\u0644\u062D\u064B\u0627 \u0648\u064A\u062A\u0643\u0648\u0646 \u0645\u0646 10 \u0625\u0644\u0649 15 \u0631\u0642\u0645\u064B\u0627."),
            ["DateOfBirthPast"] = ("Date of birth must be in the past.", "\u064A\u062C\u0628 \u0623\u0646 \u064A\u0643\u0648\u0646 \u062A\u0627\u0631\u064A\u062E \u0627\u0644\u0645\u064A\u0644\u0627\u062F \u0641\u064A \u0627\u0644\u0645\u0627\u0636\u064A.")
        };

    private static bool IsArabic =>
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("ar", StringComparison.OrdinalIgnoreCase);

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

    public static string FirstNameMaxLength => Get("FirstNameMaxLength");

    public static string LastNameRequired => Get("LastNameRequired");

    public static string LastNameMaxLength => Get("LastNameMaxLength");

    public static string PhoneRequired => Get("PhoneRequired");

    public static string PhoneValid => Get("PhoneValid");

    public static string PhoneValidLength => Get("PhoneValidLength");

    public static string DateOfBirthPast => Get("DateOfBirthPast");

    private static string Get(string key)
    {
        if (!Translations.TryGetValue(key, out var translation))
        {
            return key;
        }

        return IsArabic ? translation.Arabic : translation.English;
    }

    private static string Format(string key, params object[] args)
    {
        var template = Get(key);
        return string.Format(CultureInfo.CurrentCulture, template, args);
    }
}
