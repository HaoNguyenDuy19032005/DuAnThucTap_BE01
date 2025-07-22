namespace DuAnThucTap_BE01.DTO
{
    public class PermissionDto
    {
        public int Permissionid { get; set; }
        public string? Module { get; set; }
        public string Permissioncode { get; set; } = null!;
        public string? Description { get; set; }
    }
}
