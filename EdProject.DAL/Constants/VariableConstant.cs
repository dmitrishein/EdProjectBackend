﻿namespace EdProject
{
    public static class VariableConstant
    {
        public const int SKIP_ZERO_PAGE = 1;
        public const int MIN_FIELD_SIZE = 3;
        public const decimal MIN_PRICE = 0.1M;
        public const int EMPTY = 0;
        public const int REFRESH_TOKEN_CAPACITY = 32;

        public const string EMAIL_PATERN = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public const string PASSWORD_PATTERN = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{7,}$";
    }
}