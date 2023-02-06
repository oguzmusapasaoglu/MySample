namespace MyCore.LogManager.ExceptionHandling;

public class ExceptionMessageHelper
{
    public const string UnexpectedSystemError = "Unexpected System Error";
    public const string DataNotFound = "Data not Found";
    public const string ProcessFailedResult = "Unexpected Process Error";
    public const string TokenException = "User Token info invalid / Expired";
    //public const string JsonParseException = "JSon datası hatalı!";
    //public const string UserPasswordCanNotBeSameWithOldPasswords = "Eski şifre ve yeni şifre aynı olamaz!";
    //public const string PasswordNotInvalid = "Şifre formatı geçersiz!";
    //public const string OldPasswordNotMatch = "Değiştirmek istediğiniz şifre geçersiz!";
    public const string LoginEror = "Incorrect Username or Password";
    //public const string WaitingConfirmation = "Kullanıcı onayı beklemede!";

    public static string EmailNotValid => "Invalid Email Format!";
    public static string ParseFieldError(string fieldName) => string.Format("{0} field cannot to parse from json!");
    public static string LengthError(string fieldName, int Length) => string.Format("The {0} field cannot exceed {1} characters!", fieldName, Length);
    public static string RequiredField(string fieldName) => string.Format("The {0} field is required!", fieldName);
    public static string IsInUse(string tableName) => string.Format("{0} field cannot be registered! Registration in use!", tableName);
    public static string DataNotMatch(string fieldName) => string.Format("Does not match {0}", fieldName);
    public static string UnauthorizedAccess() => "Unauthorized Access";
    public static string UnauthorizedAccess(int parmUserId) => string.Format("Unauthorized Access. User Id : {0}", parmUserId);
    public static string UnauthorizedAccess(string parmDataName) => string.Format("Unauthorized Access. Field : {0}", parmDataName);
    public static string GreaterThanError(int parmSize) => string.Format("Must be smaller than {0}", parmSize);
}
