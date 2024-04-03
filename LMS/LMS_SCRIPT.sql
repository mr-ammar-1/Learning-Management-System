CREATE TABLE Credentials (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(50) CHECK(Role IN ('Admin', 'Student', 'Teacher')) NOT NULL,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Term (
    TermID INT PRIMARY KEY IDENTITY(1,1),
    TermName VARCHAR(50) NOT NULL,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
);

CREATE TABLE Class (
    ClassID INT PRIMARY KEY IDENTITY(1,1),
    ClassName VARCHAR(50) NOT NULL,
    TermID INT NOT NULL,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TermID) REFERENCES Term(TermID)
);

CREATE TABLE Section (
    SectionID INT PRIMARY KEY IDENTITY(1,1),
    SectionName VARCHAR(50) NOT NULL,
    ClassID INT,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID)
);

CREATE TABLE Student (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    Name VARCHAR(50) NOT NULL,
    Gender VARCHAR(50) CHECK(Gender IN ('Male', 'Female')) NOT NULL,
    FatherName VARCHAR(50) NOT NULL,
    DOB DATETIME NOT NULL,
    Email VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    RegNo VARCHAR(20) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    City VARCHAR(50) NOT NULL,
    SectionID INT,
    ClassID INT,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Credentials(ID)
);

CREATE TABLE Course (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName VARCHAR(100) NOT NULL,
    CourseCode VARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Teacher (
    TeacherID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    Name VARCHAR(50) NOT NULL,
    Gender VARCHAR(50) CHECK(Gender IN ('Male', 'Female')) NOT NULL,
    FatherName VARCHAR(50) NOT NULL,
    DOB DATETIME NOT NULL,
    Email VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    RegNo VARCHAR(20) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    City VARCHAR(50) NOT NULL,
    Province VARCHAR(50) NOT NULL,
    Status VARCHAR(50) CHECK(Status IN ('Active', 'Inactive')) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Credentials(ID)
);

CREATE TABLE Fees (
    FeeID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    ClassID INT,
    SectionID INT,
    TermID INT,
    Status VARCHAR(50) CHECK(Status IN ('Paid', 'Unpaid')) DEFAULT 'Unpaid',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID),
    FOREIGN KEY (TermID) REFERENCES Term(TermID)
);

CREATE TABLE FeesSubmission (
    FeeSubmissionID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    FeeID INT,
    SubmissionDate DATETIME,
    Status VARCHAR(50) DEFAULT 'Paid' CHECK(Status IN ('Paid', 'Pending')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (FeeID) REFERENCES Fees(FeeID)
);

CREATE TABLE teacherAttendance (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    TeacherID INT,
    ClassID INT,
    SectionID INT,
    TermID INT,
    Datee DATETIME,
    Status VARCHAR(50) CHECK(Status IN ('Present', 'Absent')) DEFAULT 'Present',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID),
    FOREIGN KEY (TermID) REFERENCES Term(TermID)
);

CREATE TABLE StudentAttendance (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    TeacherID INT,
    SectionID INT,
    CourseID INT,
    Datee DATETIME,
    Status VARCHAR(50) CHECK(Status IN ('Present', 'Absent')) DEFAULT 'Present',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
);

CREATE TABLE TeacherCourseEnrollment (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    TeacherID INT,
    ClassID INT,
    SectionID INT,
    CourseID INT,
    TermID INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID),
    FOREIGN KEY (TermID) REFERENCES Term(TermID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID)
);

CREATE TABLE Assignment (
    AssignmentID INT PRIMARY KEY IDENTITY(1,1),
    AssignmentName VARCHAR(100) NOT NULL,
    TeacherID INT,
    ClassID INT,
    SectionID INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID)
);

CREATE TABLE StudentCourseEnrollment (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    CourseID INT,
    ClassID INT,
    SectionID INT,
    TermID INT,
    Status VARCHAR(50) DEFAULT 'Active' CHECK(Status IN ('Active', 'Inactive')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (SectionID) REFERENCES Section(SectionID),
    FOREIGN KEY (TermID) REFERENCES Term(TermID)
);

CREATE TABLE AssignmentSubmission (
    SubmissionID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    AssignmentID INT,
    SubmissionDate DATETIME,
    Status VARCHAR(50) DEFAULT 'Submitted' CHECK(Status IN ('Submitted', 'Pending')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (AssignmentID) REFERENCES Assignment(AssignmentID)
);

