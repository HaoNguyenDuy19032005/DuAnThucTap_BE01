namespace DuAnThucTap_BE01.DTO
{
    public class TeacherWorkHistoryDto
    {
        public int Workhistoryid { get; set; }
        public int Teacherid { get; set; }
        public string? TeacherName { get; set; }
        public int? Operationunitid { get; set; }
        public string? OperationUnitName { get; set; }
        public int? Departmentid { get; set; }
        public string? DepartmentName { get; set; }
        public bool? Iscurrentschool { get; set; }
        public string? Positionheld { get; set; }
        public DateOnly? Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
    }
}
