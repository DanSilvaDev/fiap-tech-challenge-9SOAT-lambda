using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using RegisterUserIntoCognito.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RegisterUserIntoCognito;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(UserRequest input, ILambdaContext context)
    {
        try
        {
            var test = context.ClientContext.Environment[""];

            var cognitoClient = new AmazonCognitoIdentityProviderClient(new BasicAWSCredentials("", ""), RegionEndpoint.USEast2);


            var signupRequest = new SignUpRequest()
            {
                ClientId = "aws client id",
                Password = input.Password,
                Username = input.Email,
            };
            var attributeCpf = new AttributeType
            {
                Name = "custom:CPF",
                Value = input.CPF
            };
            signupRequest.UserAttributes.Add(attributeCpf);

            var cognitoUser = await cognitoClient.SignUpAsync(signupRequest);
            return cognitoUser.UserSub;


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }
}
