using System;
using Xunit;

namespace SandboxSnippets.Tests
{
    public class ExceptionCatcherTests
    {
        [Fact]
        public void SimpleAction()
        {
            new ExceptionFilter()
                .Catch<Exception>()
                .Execute(() => throw new Exception());
        }

        [Fact]
        public void SimpleActionCustomException_ExceptionIsRethrown()
        {
            Assert.Throws<CustomException>(() =>
                    new ExceptionFilter()
                        .Catch<Exception>()
                        .Execute(() => throw new CustomException()));
        }

        [Fact]
        public void SimpleFunc()
        {
            const string someString = "Hello";
            string ConcatsString(string x) => x + " world";

            var result = new ExceptionFilter()
                .Catch<Exception>()
                .Catch<CustomException>()
                .Execute(() => ConcatsString(someString));

            Assert.Equal("Hello world", result);
        }

        [Fact]
        public void SimpleFunc_ThrowsExceptionThatIsCaught_ReturnsDefault()
        {
            const string someString = "Hello";
            string ThrowsException(string x) => throw new Exception();

            var result = new ExceptionFilter()
                .Catch<Exception>()
                .Catch<CustomException>()
                .Execute(() => ThrowsException(someString));

            Assert.Null(result);
        }

        [Fact]
        public void SimpleFunc_ThrowsExceptionThatIsNotCaught()
        {
            const string someString = "Hello";
            string ThrowsCustomException(string x) => throw new CustomException();

            Assert.Throws<CustomException>(() => 
                new ExceptionFilter()
                    .Catch<Exception>()
                    .Execute(() => ThrowsCustomException(someString)));
        }
    }
}
