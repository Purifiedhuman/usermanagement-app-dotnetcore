namespace UserService.Dtos.UserDto;

public class GetUsersDto
{
    public string? SearchUser { get; set; }
    public Boolean? isDeleted { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
