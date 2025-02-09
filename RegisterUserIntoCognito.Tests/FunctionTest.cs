using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using RegisterUserIntoCognito.Models;

namespace RegisterUserIntoCognito.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new UserRequest()
        {

        };
        var upperCase = function.FunctionHandler(request, context);

        Assert.Equal(true, false);
    }
}
