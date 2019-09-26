namespace Galaxy.Taurus.AuthorizationAPI.ICached
{
    public interface IVerificationCodeCached
    {
        void SetImageVerificationCode(string key, string value);

        string GetImageVerificationCode(string key);

        void RemoveImageVerificationCode(string key);

        void SetPhoneVerificationCode(string key, string value);

        string GetPhoneVerificationCode(string key);
    }
}
