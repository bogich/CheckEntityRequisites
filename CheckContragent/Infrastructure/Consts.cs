namespace CheckContragent.Infrastructure.Consts
{
    public class Consts
    {
        public const int part = 10000;

        public const string Item0 = "0 - Налогоплательщик зарегистрирован в ЕГРН и имел статус действующего в указанную дату";
        public const string Item1 = "1 - Налогоплательщик зарегистрирован в ЕГРН, но не имел статус действующего в указанную дату";
        public const string Item2 = "2 - Налогоплательщик зарегистрирован в ЕГРН";
        public const string Item3 = "3 - Налогоплательщик с указанным ИНН зарегистрирован в ЕГРН, КПП не соответствует ИНН или не указан";
        public const string Item4 = "4 - Налогоплательщик с указанным ИНН не зарегистрирован в ЕГРН";
        public const string Item5 = "5 - Некорректный ИНН";
        public const string Item6 = "6 - Недопустимое количество символов ИНН";
        public const string Item7 = "7 - Недопустимое количество символов КПП";
        public const string Item8 = "8 - Недопустимые символы в ИНН";
        public const string Item9 = "9 - Недопустимые символы в КПП";
        public const string Item10 = "10 - КПП не должен использоваться при проверке ИП";
        public const string Item11 = "11 - некорректный формат даты";
        public const string Item12 = "12 - некорректная дата (ранее 01.01.1991 или позднее текущей даты)";
    }
}
