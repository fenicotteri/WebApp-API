namespace WebApp.Application.Members.Queries.GetById;

public sealed record MemberResponse( 
    string Email, 
    string FirstName, 
    string LastName, 
    DateTime CreatedAt);
