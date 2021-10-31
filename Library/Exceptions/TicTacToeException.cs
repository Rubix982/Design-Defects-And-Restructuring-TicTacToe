using System;

namespace k180307_DDR_A1.Library.Exceptions
{
    public class TicTacToeException : Exception
    {
        protected TicTacToeException(){}
    }

    public class NotEmptyGridException : TicTacToeException
    {
        public NotEmptyGridException(string message){}
    }
}