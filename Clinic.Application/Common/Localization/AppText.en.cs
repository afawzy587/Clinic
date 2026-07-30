namespace Clinic.Application.Common.Localization;

public static partial class AppText
{
    private static readonly IReadOnlyDictionary<string, string> EnglishTranslations =
        new Dictionary<string, string>
        {
            ["CreatedSuccessfully"] = "Created successfully.",
            ["UpdatedSuccessfully"] = "Updated successfully.",
            ["DeletedSuccessfully"] = "Deleted successfully.",
            ["BadRequest"] = "Bad request.",
            ["NotFound"] = "Not found.",
            ["Unauthenticated"] = "Unauthenticated.",
            ["Unauthorized"] = "Unauthorized.",
            ["InternalServerError"] = "Internal server error.",
            ["ValidationFailed"] = "Validation failed.",
            ["UnexpectedError"] = "An unexpected error occurred.",
            ["PatientRetrievedSuccessfully"] = "Patient retrieved successfully.",
            ["PatientWithIdNotFound"] = "Patient with ID {0} was not found.",
            ["FirstNameRequired"] = "First name is required.",
            ["FirstNameInvalid"] = "First name must contain at least 3 letters and can include numbers, spaces, and @.",
            ["FirstNameMaxLength"] = "First name cannot exceed 50 characters.",
            ["LastNameRequired"] = "Last name is required.",
            ["LastNameInvalid"] = "Last name must contain at least 3 letters and can include numbers, spaces, and @.",
            ["LastNameMaxLength"] = "Last name cannot exceed 50 characters.",
            ["PhoneRequired"] = "Phone number is required.",
            ["PhoneValid"] = "Phone number must be valid.",
            ["PhoneValidLength"] = "Phone number must be valid and contain 10 to 15 digits.",
            ["DateOfBirthPast"] = "Date of birth must be in the past.",
            ["NameAlreadyExists"] = "Name already exists.",
            ["FirstName"]= " Frist name"
        };
}
