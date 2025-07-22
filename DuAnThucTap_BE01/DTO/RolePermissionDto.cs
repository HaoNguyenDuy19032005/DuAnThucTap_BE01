namespace DuAnThucTap_BE01.DTO
{
    public class RolePermissionDto
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionCode { get; set; }
        public string? PermissionModule { get; set; }
    }
}
