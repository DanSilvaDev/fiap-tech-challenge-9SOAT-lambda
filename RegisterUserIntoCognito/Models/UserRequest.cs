namespace RegisterUserIntoCognito.Models;
public class UserRequest
{
    public string CPF { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public UserRequestGroup AccessGroup { get; set; }
}

public enum UserRequestGroup
{
    Clientes,
    Funcionario,
    Admin
}
