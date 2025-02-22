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
            var awsClientId = context.ClientContext.Environment["aws_client_id"];
            var awsUserPoolId = context.ClientContext.Environment["aws_pool_id"];

            var cognitoClient = new AmazonCognitoIdentityProviderClient(new BasicAWSCredentials("", ""), RegionEndpoint.USEast2);


            var signupRequest = new SignUpRequest()
            {
                ClientId = awsClientId,
                Password = input.Password,
                Username = input.Email,
            };
            signupRequest.UserAttributes.Add(new AttributeType
            {
                Name = "custom:CPF",
                Value = input.CPF
            });
            signupRequest.UserAttributes.Add(new AttributeType()
            {
                Name = "custom:GroupAccess",
                Value = input.AccessGroup.ToString()
            });
            
            var cognitoUser = await cognitoClient.SignUpAsync(signupRequest);

            await cognitoClient.AdminAddUserToGroupAsync(new AdminAddUserToGroupRequest()
            {
                GroupName = input.AccessGroup.ToString(),
                Username = signupRequest.Username,
                UserPoolId =  awsUserPoolId
            });

            if (input.AccessGroup is UserRequestGroup.Funcionario or UserRequestGroup.Admin)
            {
                await cognitoClient.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest()
                {
                    Username = signupRequest.Username,
                    UserPoolId = awsUserPoolId,
                });
            }
            
            return cognitoUser.UserSub;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
