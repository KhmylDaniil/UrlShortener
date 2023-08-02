namespace UrlShortener.BLL.Constants
{
    /// <summary>
    /// Сообщения об ошибках
    /// </summary>
    public static class ExceptionMessages
    {
        public const string MaxFieldLength = "Превышена длина поля.";

        public const string FieldCantBeEmpty = "Не заполнено обязательное поле.";

        public const string PasswordIsTooShort = "Пароль должен быть минимум 5 символов.";

        public const string NotUniqueLogin = "Пользователь с таким логином уже зарегистрирован.";

        public const string LoginInccorrect = "Неверный логин.";

        public const string PasswordInccorrect = "Неверный пароль.";

        public const string IncorrectRole = "Ошибка авторизации, у вас нет прав на проведение данной операции.";

        public const string FieldCantBeNegative = "Значение не должно быть меньше нуля.";
    }
}
