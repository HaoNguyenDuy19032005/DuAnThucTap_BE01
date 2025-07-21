	create database ThucTap

-- 1. Permissions
CREATE TABLE Permissions (
    PermissionID SERIAL PRIMARY KEY,
    Module VARCHAR(100),
    PermissionCode VARCHAR(100) NOT NULL UNIQUE,
    Description TEXT
);

-- 2. Roles
CREATE TABLE Roles (
    RoleID SERIAL PRIMARY KEY,
    RoleName VARCHAR(100) NOT NULL UNIQUE,
    Description TEXT
);

-- 3. RolePermissions
CREATE TABLE RolePermissions (
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    PRIMARY KEY (RoleID, PermissionID),
    CONSTRAINT fk_role FOREIGN KEY(RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE,
    CONSTRAINT fk_permission FOREIGN KEY(PermissionID) REFERENCES Permissions(PermissionID) ON DELETE CASCADE
);

-- 4. Departments
CREATE TABLE Departments (
    DepartmentID SERIAL PRIMARY KEY,
    DepartmentName VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ
);

-- 5. SchoolInformation
CREATE TABLE SchoolInformation (
    SchoolInfoID SERIAL PRIMARY KEY,
    SchoolName VARCHAR(255) NOT NULL,
    StandardCode VARCHAR(50) UNIQUE,
    Address TEXT,
    Province VARCHAR(100),
    Ward VARCHAR(100),
    District VARCHAR(100),
    SchoolType VARCHAR(100),
    PhoneNumber VARCHAR(20),
    FaxNumber VARCHAR(20),
    Email VARCHAR(255) UNIQUE,
    EstablishmentDate DATE,
    TrainingModel VARCHAR(255),
    WebsiteUrl TEXT,
    PrincipalName VARCHAR(150),
    PrincipalPhone VARCHAR(20),
    LogoUrl TEXT 
);

-- 6. SchoolYears
CREATE TABLE SchoolYears (
    SchoolYearID SERIAL PRIMARY KEY,
    SchoolInfoID INT NOT NULL,
    SchoolYearName VARCHAR(100) NOT NULL,
    StartYear INT NOT NULL,
    EndYear INT NOT NULL,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_schoolinfo FOREIGN KEY(SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE
);

-- 7. Semesters
CREATE TABLE Semesters (
    SemesterID SERIAL PRIMARY KEY,
    SchoolYearID INT NOT NULL,
    SemesterName VARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE
);

-- 8. SubjectTypes
CREATE TABLE SubjectTypes (
    SubjectTypeID SERIAL PRIMARY KEY,
    SubjectTypeName VARCHAR(255) NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ
);

-- 9. Subjects
CREATE TABLE Subjects (
    SubjectID SERIAL PRIMARY KEY,
    SubjectName VARCHAR(255) NOT NULL,
    SubjectCode VARCHAR(50) UNIQUE,
    DefaultPeriodsSem1 INT,
    DefaultPeriodsSem2 INT,
    DepartmentID INT,
    SubjectTypeID INT,
    SchoolYearID INT NOT NULL,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_department FOREIGN KEY(DepartmentID) REFERENCES Departments(DepartmentID) ON DELETE SET NULL,
    CONSTRAINT fk_subjecttype FOREIGN KEY(SubjectTypeID) REFERENCES SubjectTypes(SubjectTypeID) ON DELETE SET NULL,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE
);

-- 10. GradeTypes
CREATE TABLE GradeTypes (
    GradeTypeID SERIAL PRIMARY KEY,
    GradeTypeName VARCHAR(100) NOT NULL,
    WeightingFactor NUMERIC(5, 2) NOT NULL,
    MinInstancesSemester1 INT NOT NULL DEFAULT 0,
    MinInstancesSemester2 INT NOT NULL DEFAULT 0
);

-- 11. ClassTypes
CREATE TABLE ClassTypes (
    ClassTypeID SERIAL PRIMARY KEY,
    ClassTypeName VARCHAR(100) NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ
);

-- 12. Teachers
CREATE TABLE Teachers (
    TeacherID SERIAL PRIMARY KEY,
    TeacherCode VARCHAR(50) UNIQUE,
    FullName VARCHAR(150) NOT NULL,
    DateOfBirth DATE,
    Gender VARCHAR(10),
    Ethnicity VARCHAR(50),
    HireDate DATE,
    Nationality VARCHAR(100),
    Religion VARCHAR(50),
    Status VARCHAR(100),
    Alias VARCHAR(150),
    Address_ProvinceCity VARCHAR(100),
    Address_Ward VARCHAR(100),
    Address_District VARCHAR(100),
    Address_Street TEXT,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20),
    DateOfJoiningTheParty DATE,
    AvatarUrl VARCHAR(255),
    DateOfJoiningGroup DATE,
    IsPartyMember BOOLEAN,
    DepartmentID INT,
    SubjectID INT,
    SchoolYearID INT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_teacher_department FOREIGN KEY(DepartmentID) REFERENCES Departments(DepartmentID) ON DELETE SET NULL,
    CONSTRAINT fk_teacher_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE SET NULL,
    CONSTRAINT fk_teacher_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE SET NULL
);

-- 13. GradeLevels
CREATE TABLE GradeLevels (
    GradeLevelID SERIAL PRIMARY KEY,
    GradeLevelName VARCHAR(100) NOT NULL,
    CodeGradeLevel VARCHAR(20) UNIQUE,
    TeacherID INT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE SET NULL
);

-- 14. Classes
CREATE TABLE Classes (
    ClassID SERIAL PRIMARY KEY,
    ClassName VARCHAR(100) NOT NULL,
    MaxStudents INT,
    Description TEXT,
    SchoolYearID INT NOT NULL,
    GradeLevelID INT NOT NULL,
    ClassTypeID INT,
    TeacherID INT,
    SubjectID INT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE,
    CONSTRAINT fk_gradelevel FOREIGN KEY(GradeLevelID) REFERENCES GradeLevels(GradeLevelID) ON DELETE RESTRICT,
    CONSTRAINT fk_classtype FOREIGN KEY(ClassTypeID) REFERENCES ClassTypes(ClassTypeID) ON DELETE SET NULL,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE SET NULL,
    CONSTRAINT fk_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE SET NULL
);

-- 15. Students
CREATE TABLE Students (
    StudentID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    FullName TEXT NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    DateOfBirth DATE NOT NULL,
    StudentCode VARCHAR(50) NOT NULL,
    BirthPlace TEXT NOT NULL,
    EnrollmentDate DATE NOT NULL,
    Ethnicity VARCHAR(50) NOT NULL,
    AdmissionType VARCHAR(50) NOT NULL,
    Religion VARCHAR(50),
    Status VARCHAR(50),
    Address_ProvinceCity TEXT,
    Address_District TEXT,
    Address_Ward TEXT,
    Address_Street TEXT,
    Email VARCHAR(100),
    PhoneNumber VARCHAR(20),
    FatherName TEXT,
    FatherBirthYear INT,
    FatherOccupation TEXT,
    MotherName TEXT,
    MotherBirthYear INT,
    MotherOccupation TEXT,
    GuardianName TEXT,
    GuardianBirthYear INT,
    GuardianOccupation TEXT,
    PhoneNumberFather VARCHAR(20),
    PhoneNumberMother VARCHAR(20),
    PhoneNumberGuardian VARCHAR(20),
    ProfileImageURL TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 16. Contacts
CREATE TABLE Contacts (
    ContactID SERIAL PRIMARY KEY,
    TeacherID INT,
    Relationship VARCHAR(100),
    FullName VARCHAR(150),
    Address TEXT,
    PhoneNumber VARCHAR(20),
    CONSTRAINT fk_contact_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 17. Users
CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    FullName VARCHAR(150),
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    RoleID INT NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    TeacherID INT UNIQUE,
    StudentID INT UNIQUE,
    CONSTRAINT fk_role FOREIGN KEY(RoleID) REFERENCES Roles(RoleID) ON DELETE RESTRICT,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE SET NULL,
    CONSTRAINT fk_student FOREIGN KEY(StudentID) REFERENCES Students(StudentID) ON DELETE SET NULL
);

-- 18. DepartmentLeaders
CREATE TABLE DepartmentLeaders (
    DepartmentLeaderID SERIAL PRIMARY KEY,
    DepartmentID INT NOT NULL,
    SchoolYearID INT NOT NULL,
    TeacherID INT NOT NULL,
    StartDate DATE,
    EndDate DATE,
    CONSTRAINT fk_department FOREIGN KEY(DepartmentID) REFERENCES Departments(DepartmentID) ON DELETE CASCADE,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 19. Grades
CREATE TABLE Grades (
    GradeID SERIAL PRIMARY KEY,
    StudentID INT NOT NULL,
    SubjectID INT NOT NULL,
    SemesterID INT NOT NULL,
    GradeTypeID INT NOT NULL,
    SchoolInfoID INT NOT NULL,
    Score NUMERIC(4, 2) NOT NULL,
    Instance INT NOT NULL DEFAULT 1,
    GradedDate DATE,
    CONSTRAINT fk_student FOREIGN KEY(StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE CASCADE,
    CONSTRAINT fk_semester FOREIGN KEY(SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_gradetype FOREIGN KEY(GradeTypeID) REFERENCES GradeTypes(GradeTypeID) ON DELETE RESTRICT,
    CONSTRAINT fk_school FOREIGN KEY(SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE
);

select * from GradeTypes 



-- 20. TeacherConcurrentSubjects
CREATE TABLE TeacherConcurrentSubjects (
    TeacherID INT NOT NULL,
    SubjectID INT NOT NULL,
    SchoolYearID INT NOT NULL,
    PRIMARY KEY (TeacherID, SubjectID, SchoolYearID),
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE,
    CONSTRAINT fk_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE CASCADE,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE
);

-- 21. TeacherWorkStatusHistory
CREATE TABLE TeacherWorkStatusHistory (
    HistoryID SERIAL PRIMARY KEY,
    TeacherID INT NOT NULL,
    StatusType VARCHAR(100) NOT NULL,
    StartDate DATE,
    EndDate DATE,
    Note TEXT,
    DecisionFileUrl TEXT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 22. OperationUnit
CREATE TABLE OperationUnit (
    OperationUnitID SERIAL PRIMARY KEY,
    OrganizationName VARCHAR(255),
    Description TEXT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

-- 23. TeacherWorkHistory
CREATE TABLE TeacherWorkHistory (
    WorkHistoryID SERIAL PRIMARY KEY,
    TeacherID INT NOT NULL,
    OperationUnitID INT,
    DepartmentID INT,
    IsCurrentSchool BOOLEAN,
    PositionHeld VARCHAR(150),
    StartDate DATE,
    EndDate DATE,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE,
    CONSTRAINT fk_operation_unit FOREIGN KEY(OperationUnitID) REFERENCES OperationUnit(OperationUnitID) ON DELETE SET NULL,
    CONSTRAINT fk_department FOREIGN KEY(DepartmentID) REFERENCES Departments(DepartmentID) ON DELETE SET NULL
);

-- 24. TeacherTrainingHistory
CREATE TABLE TeacherTrainingHistory (
    TrainingID SERIAL PRIMARY KEY,
    TeacherID INT NOT NULL,
    TrainingInstitutionName VARCHAR(255),
    MajorOrSpecialization VARCHAR(255),
    StartDate DATE,
    EndDateOrGraduationYear VARCHAR(50),
    Active BOOLEAN,
    TrainingType VARCHAR(100),
    CertificateDiplomaName VARCHAR(255),
    AttachmentURL TEXT,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 25. GraderAssignmentTypes
CREATE TABLE GraderAssignmentTypes (
    GraderAssignmentTypeID SERIAL PRIMARY KEY,
    TypeName VARCHAR(255) NOT NULL
);

-- 26. Exams
CREATE TABLE Exams (
    ExamID SERIAL PRIMARY KEY,
    SchoolYearID INT NOT NULL,
    GradeLevelID INT NOT NULL,
    SemesterID INT NOT NULL,
    SubjectID INT NOT NULL,
    ExamName VARCHAR(255) NOT NULL,
    ExamDate DATE,
    DurationMinutes INT,
    ClassTypeID INT,
    GraderAssignmentTypeID INT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_schoolyear FOREIGN KEY(SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE,
    CONSTRAINT fk_gradelevel FOREIGN KEY(GradeLevelID) REFERENCES GradeLevels(GradeLevelID) ON DELETE CASCADE,
    CONSTRAINT fk_semester FOREIGN KEY(SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE CASCADE,
    CONSTRAINT fk_classtype FOREIGN KEY(ClassTypeID) REFERENCES ClassTypes(ClassTypeID) ON DELETE SET NULL,
    CONSTRAINT fk_grader_assignment_type FOREIGN KEY(GraderAssignmentTypeID) REFERENCES GraderAssignmentTypes(GraderAssignmentTypeID) ON DELETE SET NULL
);

-- 27. ExamSchedules
CREATE TABLE ExamSchedules (
    ExamScheduleID SERIAL PRIMARY KEY,
    ExamID INT NOT NULL,
    ClassID INT NOT NULL,
    CONSTRAINT fk_exam FOREIGN KEY(ExamID) REFERENCES Exams(ExamID) ON DELETE CASCADE,
    CONSTRAINT fk_class FOREIGN KEY(ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    UNIQUE(ExamID, ClassID)
);

-- 28. ExamGraders
CREATE TABLE ExamGraders (
    ExamGraderID SERIAL PRIMARY KEY,
    ExamScheduleID INT NOT NULL,
    TeacherID INT NOT NULL,
    CONSTRAINT fk_exam_schedule FOREIGN KEY(ExamScheduleID) REFERENCES ExamSchedules(ExamScheduleID) ON DELETE CASCADE,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE,
    UNIQUE(ExamScheduleID, TeacherID)
);

-- 29. CommendationTypes
CREATE TABLE CommendationTypes (
    CommendationTypeID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    TypeName TEXT NOT NULL
);

-- 30. DisciplineTypes
CREATE TABLE DisciplineTypes (
    DisciplineTypeID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    TypeName TEXT,
    Severity TEXT
);

-- 31. SubjectsOfExemption
CREATE TABLE SubjectsOfExemption (
    ObjectID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ExemptionName TEXT,
    Description TEXT,
    IsActive BOOLEAN
);

-- 32. StudentCommendations
CREATE TABLE StudentCommendations (
    CommendationID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    SemesterID INT NOT NULL,
    SchoolInfoID INT NOT NULL,
    CommendationTypeID INT NOT NULL,
    CommendationDate DATE,
    Content TEXT,
    AttachmentURL TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_schools FOREIGN KEY (SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE,
    CONSTRAINT fk_commendationtypes FOREIGN KEY (CommendationTypeID) REFERENCES CommendationTypes(CommendationTypeID) ON DELETE CASCADE
);

-- 33. StudentDisciplines
CREATE TABLE StudentDisciplines (
    DisciplineID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    SemesterID INT NOT NULL,
    SchoolInfoID INT NOT NULL,
    DisciplineTypeID INT NOT NULL,
    CommendationDate DATE,
    Content TEXT,
    AttachmentURL TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_schools FOREIGN KEY (SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE,
    CONSTRAINT fk_disciplinetypes FOREIGN KEY (DisciplineTypeID) REFERENCES DisciplineTypes(DisciplineTypeID) ON DELETE CASCADE
);

-- 34. StudentSubjectSummary
CREATE TABLE StudentSubjectSummary (
    SubjectSummaryID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    SubjectID INT NOT NULL,
    SemesterID INT NOT NULL,
    SchoolInfoID INT NOT NULL,
    AverageScore FLOAT,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_subjects FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_schools FOREIGN KEY (SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE
);
select * from SchoolInformation
select * from Students
select * from studentyearlystatus 
select * from classes 
select * from semesters 
select * from schoolinformation 

-- 35. StudentSemesterSummary
CREATE TABLE StudentSemesterSummary (
    SummaryID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    SemesterID INT NOT NULL,
    SchoolInfoID INT NOT NULL,
    AverageScore FLOAT,
    PerformanceRating TEXT,
    ConductRating TEXT,
    CalculatedDate DATE,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE,
    CONSTRAINT fk_schools FOREIGN KEY (SchoolInfoID) REFERENCES SchoolInformation(SchoolInfoID) ON DELETE CASCADE
);
	select * from classes 
	
-- 36. StudentYearlyStatus
CREATE TABLE StudentYearlyStatus (
    StudentYearlyStatusID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    SchoolYearID INT NOT NULL,
    ClassID INT NOT NULL,
    GradeLevelID INT NOT NULL,
    EnrollmentStatus TEXT,
    EnrollmentDate DATE,
    GraduationDate DATE,
    Notes TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_schoolyears FOREIGN KEY (SchoolYearID) REFERENCES SchoolYears(SchoolYearID) ON DELETE CASCADE,
    CONSTRAINT fk_classes FOREIGN KEY (ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    CONSTRAINT fk_gradelevel FOREIGN KEY(GradeLevelID) REFERENCES GradeLevels(GradeLevelID) ON DELETE CASCADE
);

-- 37. ClassTransferHistory
CREATE TABLE ClassTransferHistory (
    TransferID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    FromClassID INT NOT NULL,
    ToClassID INT NOT NULL,
    SemesterID INT NOT NULL,
    Reason TEXT,
    AttachmentURL TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_classes_from FOREIGN KEY (FromClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    CONSTRAINT fk_classes_to FOREIGN KEY (ToClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE
);


-- 38. SchoolTransferHistory
CREATE TABLE SchoolTransferHistory (
    SchoolTransferID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,

    StudentID INT NOT NULL,
    FromSchoolInfoID INT NOT NULL,
    FromClassID INT,
    ToSchoolInfoID INT NOT NULL,
    ToClassID INT,
    SemesterID INT NOT NULL,

    TransferDate DATE,
    Reason TEXT,
    AttachmentURL TEXT,
    TransferType TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    -- Chỉ giữ lại foreign key cần thiết
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_classes_from FOREIGN KEY (FromClassID) REFERENCES Classes(ClassID) ON DELETE SET NULL,
    CONSTRAINT fk_classes_to FOREIGN KEY (ToClassID) REFERENCES Classes(ClassID) ON DELETE SET NULL,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE
);



-- 39. StudentExemptions
CREATE TABLE StudentExemptions (
    StudentExemptionID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    ObjectID INT NOT NULL,
    FormOfExemption TEXT,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_subjectsofexemption FOREIGN KEY (ObjectID) REFERENCES SubjectsOfExemption(ObjectID) ON DELETE CASCADE
);

-- 40. StudentPreservations
CREATE TABLE StudentPreservations (
    PreservationID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentID INT NOT NULL,
    ClassID INT NOT NULL,
    PreservationDate DATE NOT NULL,
    SemesterID INT NOT NULL,
    PreservationDuration TEXT NOT NULL,
    Reason TEXT NOT NULL,
    AttachmentURL TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_classes FOREIGN KEY (ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    CONSTRAINT fk_semesters FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE
);

-- 41. Courses
CREATE TABLE Courses (
    CourseID SERIAL PRIMARY KEY,
    CourseName TEXT NOT NULL,
    TeacherID INT NOT NULL,
    ThumbnailUrl TEXT,
    Description TEXT,
    StartDate DATE,
    EndDate DATE,
    MaxStudents INT,
    Price NUMERIC,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 42. Lessons
CREATE TABLE Lessons (
    LessonID SERIAL PRIMARY KEY,
    TeacherID INT NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT,
    StartTime TIMESTAMP,
    EndTime TIMESTAMP,
    DurationInMinutes INT,
    Password TEXT,
    AutoStartOnTime BOOLEAN,
    IsRecordingEnabled BOOLEAN,
    AllowStudentSharing BOOLEAN,
    ShareableLink TEXT,
    MeetingID TEXT,
    CreatedAt TIMESTAMP DEFAULT NOW(),
    CourseID INT NOT NULL,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE,
    CONSTRAINT fk_course FOREIGN KEY(CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);

-- 43. Tests
CREATE TABLE Tests (
    TestID SERIAL PRIMARY KEY,
    TeacherID INT NOT NULL,
    Title TEXT NOT NULL,
    TestFormat TEXT,
    DurationInMinutes INT,
    StartTime TIMESTAMP,
    EndTime TIMESTAMP,
    Description TEXT,
    Classification TEXT,
    AttachmentUrl TEXT,
    RequireStudentAttachment BOOLEAN,
    CONSTRAINT fk_teacher FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- 44. TestQuestions
CREATE TABLE TestQuestions (
    QuestionID SERIAL PRIMARY KEY,
    TestID INT NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT,
    AttachmentUrl TEXT,
    DisplayOrder INT,
    OptionA TEXT,
    OptionB TEXT,
    OptionC TEXT,
    OptionD TEXT,
    CorrectOption TEXT,
    Points INT,
    CONSTRAINT fk_test FOREIGN KEY(TestID) REFERENCES Tests(TestID) ON DELETE CASCADE
);

-- 45. TestAssignments
CREATE TABLE TestAssignments (
    AssignmentID SERIAL PRIMARY KEY,
    TestID INT NOT NULL,
    ClassID INT NOT NULL,
    Status TEXT,
    CONSTRAINT fk_test FOREIGN KEY(TestID) REFERENCES Tests(TestID) ON DELETE CASCADE,
    CONSTRAINT fk_class FOREIGN KEY(ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE
);
drop table StudentTestSubmissions
-- 46. StudentTestSubmissions
CREATE TABLE StudentTestSubmissions (
    SubmissionID SERIAL PRIMARY KEY,
    AssignmentID INT NOT NULL,
    StudentID INT NOT NULL,
    StartTime TIMESTAMP,
    SubmissionTime TIMESTAMP,
    Status TEXT,
    Score NUMERIC,
    TeacherFeedback TEXT,
    CONSTRAINT fk_assignment FOREIGN KEY(AssignmentID) REFERENCES TestAssignments(AssignmentID) ON DELETE CASCADE,
    CONSTRAINT fk_student FOREIGN KEY(StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
select * from classes
-- 47. StudentTestAnswers
CREATE TABLE StudentTestAnswers (
    AnswerID SERIAL PRIMARY KEY,
    SubmissionID INT NOT NULL,
    QuestionID INT NOT NULL,
    AnswerContent TEXT,
    SelectedOption TEXT,
    IsCorrect BOOLEAN,
    CONSTRAINT fk_submission FOREIGN KEY(SubmissionID) REFERENCES StudentTestSubmissions(SubmissionID) ON DELETE CASCADE,
    CONSTRAINT fk_question FOREIGN KEY(QuestionID) REFERENCES TestQuestions(QuestionID) ON DELETE CASCADE
);

-- 48. SubmissionFiles
CREATE TABLE SubmissionFiles (
    FileID SERIAL PRIMARY KEY,
    SubmissionID INT NOT NULL,
    FileName TEXT,
    FileUrl TEXT,
    FileSizeKB INT,
    CONSTRAINT fk_submission FOREIGN KEY(SubmissionID) REFERENCES StudentTestSubmissions(SubmissionID) ON DELETE CASCADE
);

-- 49. Enrollments
CREATE TABLE Enrollments (
    EnrollmentID SERIAL PRIMARY KEY,
    StudentID INT NOT NULL,
    ClassID INT NOT NULL,
    EnrollmentDate DATE,
    CONSTRAINT fk_student FOREIGN KEY(StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT fk_class FOREIGN KEY(ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE
);

-- 50. QnaThreads
CREATE TABLE QnaThreads (
    ThreadID SERIAL PRIMARY KEY,
    ClassID INT NOT NULL,
    CreatorID INT NOT NULL,
    Title TEXT,
    ReplyCount INT,
    LastActivityAt TIMESTAMP,
    Status TEXT,
    RowCreatedAt TIMESTAMP DEFAULT NOW(),
    CONSTRAINT fk_class FOREIGN KEY(ClassID) REFERENCES Classes(ClassID) ON DELETE CASCADE,
    CONSTRAINT fk_creator FOREIGN KEY(CreatorID) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- 51. QnaParticipants
CREATE TABLE QnaParticipants (
    ThreadID INT NOT NULL,
    UserID INT NOT NULL,
    FirstParticipatedAt TIMESTAMP,
    PRIMARY KEY (ThreadID, UserID),
    CONSTRAINT fk_thread FOREIGN KEY(ThreadID) REFERENCES QnaThreads(ThreadID) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY(UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- 52. UserThreadReadStatus
CREATE TABLE UserThreadReadStatus (
    UserID INT NOT NULL,
    ThreadID INT NOT NULL,
    LastReadTimestamp TIMESTAMP,
    PRIMARY KEY (UserID, ThreadID),
    CONSTRAINT fk_user FOREIGN KEY(UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    CONSTRAINT fk_thread FOREIGN KEY(ThreadID) REFERENCES QnaThreads(ThreadID) ON DELETE CASCADE
);

-- 53. Announcements
CREATE TABLE Announcements (
    AnnouncementID SERIAL PRIMARY KEY,
    CreatorID INT NOT NULL,
    Title TEXT,
    Body TEXT,
    TargetAudience TEXT,
    URL TEXT,
    CreatedAt TIMESTAMP DEFAULT NOW(),
    CONSTRAINT fk_creator FOREIGN KEY(CreatorID) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- 54. UserNotifications
CREATE TABLE UserNotifications (
    UniqUserNotificationDueID SERIAL PRIMARY KEY,
    UserID INT NOT NULL,
    AnnouncementID INT NOT NULL,
    IsRead BOOLEAN DEFAULT FALSE,
    ReadAt TIMESTAMP,
    CONSTRAINT fk_user FOREIGN KEY(UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    CONSTRAINT fk_announcement FOREIGN KEY(AnnouncementID) REFERENCES Announcements(AnnouncementID) ON DELETE CASCADE
);

-- StudentTransferReceipt Table
CREATE TABLE StudentTransferReceipt (
    ReceiptID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    StudentName TEXT NOT NULL,
    StudentCode VARCHAR(50) NOT NULL,
    TransferDate DATE NOT NULL,
    SemesterID INT NOT NULL,
    Province TEXT NOT NULL,
    District TEXT NOT NULL,
    FromSchool TEXT NOT NULL,
    Reason TEXT,
    AttachmentFile TEXT NOT NULL,
    CONSTRAINT fk_semester FOREIGN KEY (SemesterID) REFERENCES Semesters(SemesterID) ON DELETE CASCADE
);

select * from DisplayedTestList 




CREATE TABLE DisplayedTestList (
    DisplayItemID SERIAL PRIMARY KEY,
    -- LIÊN KẾT: ID của môn học (cho cột "Môn học" trên giao diện)
    SubjectID INT NOT NULL,  
    -- LIÊN KẾT: ID của giáo viên (cho cột "Giảng viên" trên giao diện)
    TeacherID INT NOT NULL,  

    -- Các cột hiển thị trực tiếp từ ảnh "Danh sách bài kiểm tra"
    Title TEXT NOT NULL,        -- Nội dung kiểm tra
    StartTime TIMESTAMP NOT NULL, -- Ngày làm bài
    DurationInMinutes INT NOT NULL, -- Thời lượng
    StatusDisplay TEXT,         -- Tình trạng (Lưu ý: Đây là cột động, cần được cập nhật)
    ActionDisplay TEXT,         -- Bà	i làm (Lưu ý: Đây là cột động, cần được cập nhật)

    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_displayed_subject_id FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE RESTRICT,
    CONSTRAINT fk_displayed_teacher_id FOREIGN KEY(TeacherID) REFERENCES Teachers(TeacherID) ON DELETE RESTRICT
);





CREATE TABLE TestHeaders (
    TestID SERIAL PRIMARY KEY,
    SubjectID INT NOT NULL,
    ClassID INT NOT NULL,
    Title TEXT NOT NULL,
    TestFormat VARCHAR(50) NOT NULL,
    DurationInMinutes INT NOT NULL,
    StartTime TIMESTAMP NOT NULL,
    AttachmentUrl TEXT,
    RequireStudentAttachment BOOLEAN DEFAULT FALSE,
    SubmissionRules TEXT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_testheader_subject FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE RESTRICT,
    CONSTRAINT fk_testheader_class FOREIGN KEY(ClassID) REFERENCES Classes(ClassID) ON DELETE RESTRICT
);

CREATE TABLE TestQuestionItems (
    QuestionID SERIAL PRIMARY KEY,
    TestID INT NOT NULL,
    DisplayOrder INT NOT NULL,
    Content TEXT NOT NULL,
    OptionA TEXT,
    OptionB TEXT,
    OptionC TEXT,
    OptionD TEXT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_question_test FOREIGN KEY(TestID) REFERENCES TestHeaders(TestID) ON DELETE CASCADE
);

CREATE TABLE TestStudentSubmissions (
    SubmissionID SERIAL PRIMARY KEY,
    TestID INT NOT NULL,
    StudentID INT NOT NULL,
    StartTime TIMESTAMP,
    SubmissionTime TIMESTAMP,
    Status VARCHAR(50),
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_submission_test FOREIGN KEY(TestID) REFERENCES TestHeaders(TestID) ON DELETE CASCADE,
    CONSTRAINT fk_submission_student FOREIGN KEY(StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);


CREATE TABLE TestStudentAnswers (
    AnswerID SERIAL PRIMARY KEY,
    SubmissionID INT NOT NULL,
    QuestionID INT,
    SelectedOption VARCHAR(1),
    AnswerContent TEXT,
    AnswerAttachmentUrl TEXT,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ,
    CONSTRAINT fk_answer_submission FOREIGN KEY(SubmissionID) REFERENCES TestStudentSubmissions(SubmissionID) ON DELETE CASCADE,
    CONSTRAINT fk_answer_question FOREIGN KEY(QuestionID) REFERENCES TestQuestionItems(QuestionID) ON DELETE CASCADE
);


ALTER TABLE StudentSubjectSummary
ADD CONSTRAINT uq_subject_summary UNIQUE (StudentID, SubjectID, SemesterID, SchoolInfoID);

ALTER TABLE StudentSemesterSummary
ADD CONSTRAINT uq_semester_summary UNIQUE (StudentID, SemesterID, SchoolInfoID);

CREATE TRIGGER trg_update_subject_summary
AFTER INSERT OR UPDATE ON Grades
FOR EACH ROW
EXECUTE FUNCTION fn_update_subject_summary();


CREATE OR REPLACE FUNCTION fn_update_subject_summary()
RETURNS TRIGGER AS $$
BEGIN
    -- Cập nhật bảng tổng kết môn học
    INSERT INTO StudentSubjectSummary(StudentID, SubjectID, SemesterID, SchoolInfoID, AverageScore)
    SELECT
        NEW.StudentID,
        NEW.SubjectID,
        NEW.SemesterID,
        NEW.SchoolInfoID,
        ROUND(AVG(Score), 2)
    FROM Grades
    WHERE StudentID = NEW.StudentID
      AND SubjectID = NEW.SubjectID
      AND SemesterID = NEW.SemesterID
      AND SchoolInfoID = NEW.SchoolInfoID
    GROUP BY StudentID, SubjectID, SemesterID, SchoolInfoID
    ON CONFLICT (StudentID, SubjectID, SemesterID, SchoolInfoID)
    DO UPDATE SET AverageScore = EXCLUDED.AverageScore;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

select * from schoolinformation s 

CREATE TRIGGER trg_update_subject_summary
AFTER INSERT OR UPDATE OR DELETE ON Grades
FOR EACH ROW
EXECUTE FUNCTION fn_update_subject_summary();

CREATE OR REPLACE FUNCTION fn_update_semester_summary()
RETURNS TRIGGER AS $$
BEGIN
    -- Tính điểm trung bình toàn học kỳ và cập nhật đánh giá
    INSERT INTO StudentSemesterSummary(StudentID, SemesterID, SchoolInfoID, AverageScore, PerformanceRating, ConductRating, CalculatedDate)
    SELECT
        NEW.StudentID,
        NEW.SemesterID,
        NEW.SchoolInfoID,
        ROUND(AVG(AverageScore)::numeric, 2),
        CASE
            WHEN AVG(AverageScore) >= 8 THEN 'Giỏi'
            WHEN AVG(AverageScore) >= 6.5 THEN 'Khá'
            WHEN AVG(AverageScore) >= 5 THEN 'Trung bình'
            ELSE 'Yếu'
        END,
        'Tốt', -- giả định
        NOW()
    FROM StudentSubjectSummary
    WHERE StudentID = NEW.StudentID
      AND SemesterID = NEW.SemesterID
      AND SchoolInfoID = NEW.SchoolInfoID
    GROUP BY StudentID, SemesterID, SchoolInfoID
    ON CONFLICT (StudentID, SemesterID, SchoolInfoID)
    DO UPDATE SET 
        AverageScore = EXCLUDED.AverageScore,
        PerformanceRating = EXCLUDED.PerformanceRating,
        ConductRating = EXCLUDED.ConductRating,
        CalculatedDate = EXCLUDED.CalculatedDate;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER trg_update_semester_summary
AFTER INSERT OR UPDATE OR DELETE ON StudentSubjectSummary
FOR EACH ROW
EXECUTE FUNCTION fn_update_semester_summary();


CREATE OR REPLACE FUNCTION fn_update_subject_summary()
RETURNS TRIGGER AS $$
BEGIN
    -- Cập nhật bảng tổng kết môn học
    INSERT INTO StudentSubjectSummary(StudentID, SubjectID, SemesterID, SchoolInfoID, AverageScore)
    SELECT
        NEW.StudentID,
        NEW.SubjectID,
        NEW.SemesterID,
        NEW.SchoolInfoID,
        ROUND(AVG(Score)::numeric, 2)
    FROM Grades
    WHERE StudentID = NEW.StudentID
      AND SubjectID = NEW.SubjectID
      AND SemesterID = NEW.SemesterID
      AND SchoolInfoID = NEW.SchoolInfoID
    GROUP BY StudentID, SubjectID, SemesterID, SchoolInfoID
    ON CONFLICT (StudentID, SubjectID, SemesterID, SchoolInfoID)
    DO UPDATE SET AverageScore = EXCLUDED.AverageScore;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;









-- Chèn điểm cho sinh viên 5, môn 1, học kỳ 1
INSERT INTO Grades (StudentID, SubjectID, SemesterID, GradeTypeID, SchoolInfoID, Score, GradedDate)
VALUES (5, 1, 1, 1, 1, 9.0, CURRENT_DATE);

-- Chèn thêm 1 điểm nữa để đủ dữ liệu
INSERT INTO Grades (StudentID, SubjectID, SemesterID, GradeTypeID, SchoolInfoID, Score, GradedDate)
VALUES (5, 2, 1, 1, 1, 7.5, CURRENT_DATE);





select * from grades g 


-- Dữ liệu mẫu cho Grades
INSERT INTO Grades (StudentID, SubjectID, SemesterID, GradeTypeID, SchoolInfoID, Score, GradedDate)
VALUES 

-- Student 11
(11, 1, 2, 1, 1, 9.0, '2025-07-01'),
(11, 1, 2, 2, 1, 8.5, '2025-07-05'),
(11, 2, 2, 1, 1, 7.0, '2025-07-02'),
(11, 2, 2, 2, 1, 8.0, '2025-07-06'),
(11, 3, 2, 1, 1, 9.0, '2025-07-03'),
(11, 3, 2, 2, 1, 9.5, '2025-07-07');

















select * from grades g 

-- 1. Thêm dữ liệu mẫu cho bảng Permissions
INSERT INTO Permissions (Module, PermissionCode, Description) VALUES
('User Management', 'USER_CREATE', 'Create user'),
('User Management', 'USER_READ', 'Read user'),
('User Management', 'USER_UPDATE', 'Update user'),
('User Management', 'USER_DELETE', 'Delete user');

-- 2. Thêm dữ liệu mẫu cho bảng Roles
INSERT INTO Roles (RoleName, Description) VALUES
('Admin', 'Administrator role with full access'),
('Teacher', 'Role for teachers'),
('Student', 'Role for students');

-- 3. Thêm dữ liệu mẫu cho bảng RolePermissions
INSERT INTO RolePermissions (RoleID, PermissionID) VALUES
(1, 1), (1, 2), (1, 3), (1, 4),
(2, 2), (2, 3),
(3, 2);

-- 4. Thêm dữ liệu mẫu cho bảng Departments
INSERT INTO Departments (DepartmentName) VALUES
('Mathematics'), ('Science'), ('Literature');

-- 5. Thêm dữ liệu mẫu cho bảng SchoolInformation
INSERT INTO SchoolInformation (SchoolName, StandardCode, Address, Province, Ward, District, SchoolType, PhoneNumber, Email) VALUES
('Trường THPT ABC', 'ST123', '123 Đường ABC', 'Hà Nội', 'Phường 1', 'Quận 1', 'High School', '0123456789', 'contact@abc.edu.vn');

-- 6. Thêm dữ liệu mẫu cho bảng SchoolYears
INSERT INTO SchoolYears (SchoolInfoID, SchoolYearName, StartYear, EndYear) VALUES
(1, '2023-2024', 2023, 2024);

-- 7. Thêm dữ liệu mẫu cho bảng Semesters
INSERT INTO Semesters (SchoolYearID, SemesterName, StartDate, EndDate) VALUES
(1, 'Semester 1', '2023-09-01', '2024-01-31'),
(1, 'Semester 2', '2024-02-01', '2024-06-30');

-- 8. Thêm dữ liệu mẫu cho bảng SubjectTypes
INSERT INTO SubjectTypes (SubjectTypeName) VALUES
('Core'), ('Elective');

-- 9. Thêm dữ liệu mẫu cho bảng Subjects
INSERT INTO Subjects (SubjectName, SubjectCode, DepartmentID, SubjectTypeID, SchoolYearID) VALUES
('Toán học', 'MATH101', 1, 1, 1),
('Vật lý', 'PHYS101', 2, 1, 1),
('Ngữ văn', 'LIT101', 3, 1, 1);

-- 10. Thêm dữ liệu mẫu cho bảng GradeTypes
INSERT INTO GradeTypes (GradeTypeName, WeightingFactor) VALUES
('Midterm', 0.4),
('Final', 0.6);

-- 11. Thêm dữ liệu mẫu cho bảng ClassTypes
INSERT INTO ClassTypes (ClassTypeName) VALUES
('Regular'), ('Advanced');

-- 12. Thêm dữ liệu mẫu cho bảng Teachers
INSERT INTO Teachers (TeacherCode, FullName, DateOfBirth, Gender, Email) VALUES
('T001', 'Nguyễn Văn A', '1980-01-01', 'Male', 'a.nguyen@school.edu.vn'),
('T002', 'Trần Thị B', '1985-02-02', 'Female', 'b.tran@school.edu.vn');

-- 13. Thêm dữ liệu mẫu cho bảng GradeLevels
INSERT INTO GradeLevels (GradeLevelName, CodeGradeLevel) VALUES
('Khối 10', 'G10'),
('Khối 11', 'G11');

-- 14. Thêm dữ liệu mẫu cho bảng Classes
INSERT INTO Classes (ClassName, SchoolYearID, GradeLevelID) VALUES
('10A1', 1, 1),
('11B1', 1, 2);

-- 15. Thêm dữ liệu mẫu cho bảng Students
INSERT INTO Students (
    FullName, Gender, DateOfBirth, StudentCode, BirthPlace,
    EnrollmentDate, Ethnicity, AdmissionType, Religion, Status
) VALUES
('Lê Văn C', 'Male', '2005-01-01', 'ST001', 'Hà Nội',
 '2023-09-01', 'Kinh', 'Xét tuyển', 'Không', 'Đang học'),

('Phạm Thị D', 'Female', '2005-02-02', 'ST002', 'Hà Nội',
 '2023-09-01', 'Kinh', 'Thi tuyển', 'Phật giáo', 'Đang học');


-- 16. Thêm dữ liệu mẫu cho bảng Contacts
INSERT INTO Contacts (TeacherID, Relationship, FullName, PhoneNumber) VALUES
(1, 'Father', 'Lê Văn E', '0987654321'),
(2, 'Mother', 'Trần Thị F', '0123456789');

-- 17. Thêm dữ liệu mẫu cho bảng Users
INSERT INTO Users (FullName, Email, PasswordHash, RoleID) VALUES
('Nguyễn Văn G', 'g.nguyen@school.edu.vn', 'hashed_password_1', 1),
('Trần Thị H', 'h.tran@school.edu.vn', 'hashed_password_2', 2);

-- 18. Thêm dữ liệu mẫu cho bảng DepartmentLeaders
INSERT INTO DepartmentLeaders (DepartmentID, SchoolYearID, TeacherID) VALUES
(1, 1, 1),
(2, 1, 2);

-- 19. Thêm dữ liệu mẫu cho bảng Grades
INSERT INTO Grades (StudentID, SubjectID, SemesterID, GradeTypeID, Score) VALUES
(2, 1, 1, 1, 8.5),
(2, 2, 1, 2, 9.0),
(3, 1, 1, 1, 7.0);






-- 20. Thêm dữ liệu mẫu cho bảng TeacherConcurrentSubjects
INSERT INTO TeacherConcurrentSubjects (TeacherID, SubjectID, SchoolYearID) VALUES
(1, 1, 1),
(2, 2, 1);

-- 21. Thêm dữ liệu mẫu cho bảng TeacherWorkStatusHistory
INSERT INTO TeacherWorkStatusHistory (TeacherID, StatusType, StartDate) VALUES
(1, 'Active', '2022-01-01'),
(2, 'On Leave', '2023-01-01');

-- 22. Thêm dữ liệu mẫu cho bảng OperationUnit
INSERT INTO OperationUnit (OrganizationName, Description) VALUES
('Bộ Giáo dục và Đào tạo', 'Cơ quan quản lý giáo dục quốc gia');

-- 23. Thêm dữ liệu mẫu cho bảng TeacherWorkHistory
INSERT INTO TeacherWorkHistory (TeacherID, OperationUnitID, DepartmentID, IsCurrentSchool, PositionHeld, StartDate) VALUES
(1, 1, 1, TRUE, 'Giáo viên Toán', '2020-01-01');

-- 24. Thêm dữ liệu mẫu cho bảng TeacherTrainingHistory
INSERT INTO TeacherTrainingHistory (TeacherID, TrainingInstitutionName, MajorOrSpecialization, StartDate, EndDateOrGraduationYear) VALUES
(1, 'Đại học Sư phạm', 'Sư phạm Toán', '2000-01-01', '2004-01-01');

-- 25. Thêm dữ liệu mẫu cho bảng GraderAssignmentTypes
INSERT INTO GraderAssignmentTypes (TypeName) VALUES
('Assignment'), ('Exam');

-- 26. Thêm dữ liệu mẫu cho bảng Exams
INSERT INTO Exams (SchoolYearID, GradeLevelID, SemesterID, SubjectID, ExamName, ExamDate) VALUES
(1, 1, 1, 1, 'Kiểm tra giữa kỳ Toán', '2023-11-01');

-- 27. Thêm dữ liệu mẫu cho bảng ExamSchedules
INSERT INTO ExamSchedules (ExamID, ClassID) VALUES
(1, 1);

-- 28. Thêm dữ liệu mẫu cho bảng ExamGraders
INSERT INTO ExamGraders (ExamScheduleID, TeacherID) VALUES
(1, 1);

-- 29. Thêm dữ liệu mẫu cho bảng CommendationTypes
INSERT INTO CommendationTypes (TypeName) VALUES
('Học sinh xuất sắc'), ('Học sinh tiên tiến');

-- 30. Thêm dữ liệu mẫu cho bảng DisciplineTypes
INSERT INTO DisciplineTypes (TypeName, Severity) VALUES
('Vi phạm nội quy', 'Nhẹ'), ('Vi phạm đạo đức', 'Nặng');

-- 31. Thêm dữ liệu mẫu cho bảng SubjectsOfExemption
INSERT INTO SubjectsOfExemption (ExemptionName, Description, IsActive) VALUES
('Miễn học môn Toán', 'Học sinh không cần học môn Toán', TRUE);

-- 41. Thêm dữ liệu mẫu cho bảng Courses
INSERT INTO Courses (CourseName, TeacherID, Description) VALUES
('Khóa học Toán nâng cao', 1, 'Khóa học dành cho học sinh khá giỏi');

-- 42. Thêm dữ liệu mẫu cho bảng Lessons
INSERT INTO Lessons (TeacherID, Title, CourseID, Description) VALUES
(1, 'Bài học 1: Giới thiệu về Đại số', 1, 'Giới thiệu về các khái niệm cơ bản trong Đại số');

-- 43. Thêm dữ liệu mẫu cho bảng Tests
INSERT INTO Tests (TeacherID, Title, TestFormat) VALUES
(1, 'Kiểm tra giữa kỳ Toán', 'Trắc nghiệm');

-- 44. Thêm dữ liệu mẫu cho bảng TestQuestions
INSERT INTO TestQuestions (TestID, Title, Description) VALUES
(1, 'Câu hỏi 1', 'Giải phương trình x + 2 = 5');

-- 45. Thêm dữ liệu mẫu cho bảng TestAssignments
INSERT INTO TestAssignments (TestID, ClassID, Status) VALUES
(1, 1, 'Chưa thực hiện');

-- 48. Thêm dữ liệu mẫu cho bảng SubmissionFiles
INSERT INTO SubmissionFiles (SubmissionID, FileName, FileUrl) VALUES
(2, 'Bài nộp Toán', 'http://example.com/file1');

-- 50. Thêm dữ liệu mẫu cho bảng QnaThreads
INSERT INTO QnaThreads (ClassID, CreatorID, Title) VALUES
(1, 1, 'Hỏi đáp về bài học');

-- 51. Thêm dữ liệu mẫu cho bảng QnaParticipants
INSERT INTO QnaParticipants (ThreadID, UserID) VALUES
(1, 1);

-- 52. Thêm dữ liệu mẫu cho bảng UserThreadReadStatus
INSERT INTO UserThreadReadStatus (UserID, ThreadID) VALUES
(1, 1);

-- 53. Thêm dữ liệu mẫu cho bảng Announcements
INSERT INTO Announcements (CreatorID, Title, Body) VALUES
(1, 'Thông báo nghỉ học', 'Học sinh nghỉ học vào ngày 01/01');

-- 54. Thêm dữ liệu mẫu cho bảng UserNotifications
INSERT INTO UserNotifications (UserID, AnnouncementID) VALUES
(1, 1);
