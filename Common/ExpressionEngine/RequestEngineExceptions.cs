//Author: Sergey Lavrinenko
//Date:   28Nov2010

using System;

namespace Common.ExpressionEngine
{
    /// <summary>
    /// Basic type of exception for this engine. 
    /// Important difference is Position property it provides: vaue is index of character in string expression on which problem was detected.
    /// </summary>
    public class ExpressionEngineException : ApplicationException
    {
        int position;

        public int Position
        {
            get { return position; }
            set { position=value; }
        }

        internal ExpressionEngineException(int position, string message = "") : base(message) { this.position = position; }
    }

    /// <summary>
    /// All ExpressionParser's exceptions
    /// </summary>
    public class ParserException : ExpressionEngineException
    {
        public enum ParserExceptionType
        {
            QuatesNotClosed,
            BracketsNotClosed,
            InvalidBracketsOrder
        }

        ParserExceptionType type;

        internal ParserException(int position, ParserExceptionType type, string message = "") : base(position, message) { this.type = type; }

        public ParserExceptionType Type { get { return type; } }
    }

    /// <summary>
    /// All ExpressionBuilder's exceptions
    /// </summary>
    public class BuilderException : ExpressionEngineException
    {
        public enum BuilderExceptionType
        {
            WrongArgFormat,
            ArgNumberExceedsMax,
            UnrecognizedConversionType,
            UnexpectedConstant,
            UnexpectedExpression,
            PropertyNotExists,
            FunctionNotExists,
            ParameterTypeNotSupported,
            FunctionArgumentsExpected,
            WrongArgumentsNumber,
            NoFunctionFound,
            NoLeftOperand,
            NoRightOperand,
            IncorrectUnaryOperatorPosition,
            IncorrectExpression,
            UnexpectedError
        }

        BuilderExceptionType type;

        public BuilderException(int position, BuilderExceptionType type, string message = "") : base(position, message) { this.type = type; }

        public BuilderExceptionType Type { get { return type; } }
    }

    /// <summary>
    /// All exceptions related to types incompatibilities found in expression
    /// </summary>
    public class ExpressionTypeException : ExpressionEngineException
    {
        readonly Type expectedType;
        readonly Type obtainedType;

        public Type ExpectedType
        {
            get { return expectedType; }
        }

        public Type ObtainedType
        {
            get { return obtainedType; }
        }

        public ExpressionTypeException(int position, Type expectedType, Type obtainedType) : this(position, expectedType, obtainedType, string.Empty) { }

        public ExpressionTypeException(int position, Type expectedType, Type obtainedType, string message)
            : base(position, message)
        {
            this.expectedType = expectedType;
            this.obtainedType = obtainedType;
        }
    }
}
